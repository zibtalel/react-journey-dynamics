namespace Crm.PerDiem.Services
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Net.Mail;
	using System.Net.Mime;
	using System.Text;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Extensions;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Helper;
	using Crm.Library.Model.Site;
	using Crm.Library.Services.Interfaces;
	using Crm.Library.ViewModels;
	using Crm.Model;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Model.Lookups;
	using Crm.PerDiem.Services.Interfaces;
	using Crm.Services;
	using Crm.Services.Interfaces;

	public class PerDiemReportService : IPerDiemReportService
	{
		private readonly IRenderViewToStringService renderViewToStringService;
		private readonly IPdfService pdfService;
		private readonly IAppSettingsProvider appSettingsProvider;
		private readonly IUserService userService;
		private readonly IEnumerable<IPerDiemReportProvider> perDiemReportProviders;
		private readonly IResourceManager resourceManager;
		private readonly Func<Message> messageFactory;
		private readonly IRepositoryWithTypedId<Message, Guid> messageRepository;
		private readonly IFileService fileService;
		private readonly IRepositoryWithTypedId<PerDiemReport, Guid> perDiemReportRepository;
		private readonly Site site;

		public PerDiemReportService(IRenderViewToStringService renderViewToStringService, IPdfService pdfService, IAppSettingsProvider appSettingsProvider, IUserService userService, IEnumerable<IPerDiemReportProvider> perDiemReportProviders, IResourceManager resourceManager, IRepositoryWithTypedId<Message, Guid> messageRepository, Func<Message> messageFactory, IFileService fileService, IRepositoryWithTypedId<PerDiemReport, Guid> perDiemReportRepository, Site site)
		{
			this.renderViewToStringService = renderViewToStringService;
			this.pdfService = pdfService;
			this.appSettingsProvider = appSettingsProvider;
			this.userService = userService;
			this.perDiemReportProviders = perDiemReportProviders;
			this.resourceManager = resourceManager;
			this.messageFactory = messageFactory;
			this.messageRepository = messageRepository;
			this.fileService = fileService;
			this.perDiemReportRepository = perDiemReportRepository;
			this.site = site;
		}

		public virtual byte[] CreateReportAsPdf(PerDiemReport perDiemReport)
		{
			var model = new HtmlTemplateViewModel { Id = perDiemReport.Id, ViewModel = "Crm.PerDiem.ViewModels.PerDiemReportDetailsModalViewModel" };
			var reportHtml = renderViewToStringService.RenderViewToString("Crm.PerDiem", "PerDiemReport", "Report", model);
			var reportPdf = pdfService.Html2Pdf(reportHtml);
			return reportPdf;
		}

		public virtual string[] GetDefaultReportRecipientEmails()
		{
			var perDiemReportRecipients = appSettingsProvider.GetValue(PerDiemPlugin.Settings.Email.PerDiemReportRecipients).ToList();
			perDiemReportRecipients.AddItemsNotContained(userService.CurrentUser.Email.AsEnumerable(), StringComparison.InvariantCultureIgnoreCase);
			return perDiemReportRecipients.OrderBy(e => e).ToArray();
		}

		public virtual string GetReportName(PerDiemReport perDiemReport)
		{
			var userDisplayName = userService.GetDisplayName(perDiemReport.UserName);
			var fromLocalTime = perDiemReport.From.ToLocalTime().Date;
			var toLocalTime = perDiemReport.To.ToLocalTime().Date;
			if (fromLocalTime == fromLocalTime.FirstDateOfWeek().Date && toLocalTime == toLocalTime.LastDateOfWeek().Date && fromLocalTime.GetCalendarWeek() == toLocalTime.GetCalendarWeek())
			{
				return resourceManager.GetTranslation("PerDiemReportFromUserCalendarWeek").WithArgs(userDisplayName, fromLocalTime.GetCalendarWeek());
			}

			if (fromLocalTime == fromLocalTime.FirstDateOfMonth().Date && toLocalTime == toLocalTime.LastDateOfMonth().Date)
			{
				return resourceManager.GetTranslation("PerDiemReportFromUserMonth").WithArgs(userDisplayName, fromLocalTime.ToString("MMMM", CultureInfo.InvariantCulture));
			}

			return resourceManager.GetTranslation("PerDiemReportFromUserCustomPeriod").WithArgs(userDisplayName, fromLocalTime.ToShortDateString(), toLocalTime.ToShortDateString());
		}

		public virtual bool SendReportAsPdf(PerDiemReport perDiemReport)
		{
			var recipientEmails = GetDefaultReportRecipientEmails();
			return recipientEmails.Any() && SendReportAsPdf(perDiemReport, recipientEmails);
		}

		public virtual bool SendReportAsPdf(PerDiemReport perDiemReport, IEnumerable<string> recipientEmails)
		{
			if (perDiemReport.StatusKey != PerDiemReportStatus.ClosedKey)
			{
				return false;
			}
			
			var reportName = GetReportName(perDiemReport);
			var reportAsPdf = CreateReportAsPdf(perDiemReport);
			var attachments = new List<FileResource>();
			var reportAsFileResource = fileService.CreateAndSaveFileResource(reportAsPdf, MediaTypeNames.Application.Pdf, reportName.AppendIfMissing(".pdf"));
			attachments.AddRange(perDiemReportProviders.SelectMany(x => x.GetFileResources(perDiemReport)));

			var message = messageFactory();
			message.IsBodyHtml = true;
			var bodyMsg = new StringBuilder();
			bodyMsg.AppendLine($"<h3>{resourceManager.GetTranslation("PerDiemReportTitle")}</h3>");
			var reportLink = site.GetExtension<DomainExtension>().HostUri.ToString().AppendIfMissing("/") + "File/File/" + reportAsFileResource.Id;
			bodyMsg.AppendLine($"<div><a href={reportLink}>{reportName}</a></div><br>");
			bodyMsg.AppendLine($"<h3>{resourceManager.GetTranslation("Files")}</h3>");
			foreach (var file in attachments)
			{
				var fileLink = site.GetExtension<DomainExtension>().HostUri.ToString().AppendIfMissing("/") + "File/File/" + file.Id;
				bodyMsg.AppendLine($"<div><a href={fileLink}>{file.Filename}</a></div>");
			}
			message.Body = bodyMsg.ToString();
			if (userService.CurrentUser != null)
			{
				message.From = new MailAddress(userService.CurrentUser.Email, userService.CurrentUser.DisplayName).ToString();
			}
			if (appSettingsProvider.GetValue(PerDiemPlugin.Settings.Email.SendPerDiemReportToResponsibleUser))
			{
				var createUser = userService.GetUser(perDiemReport.CreateUser);
				if (createUser != null && createUser.Email.IsNotNullOrEmpty())
				{
					recipientEmails.ToList().AddItemsNotContained(createUser.Email.AsEnumerable());
				}
			}
			message.Recipients = recipientEmails.ToList();
			message.Subject = reportName;
			message.AttachmentIds.Add(reportAsFileResource.Id);
			messageRepository.SaveOrUpdate(message);

			return true;
		}
	}
}
