namespace Crm.PerDiem.BackgroundServices
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Security.Principal;
	using System.Text;
	using System.Threading;

	using Crm.Library.BackgroundServices;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Data.NHibernateProvider;
	using Crm.Library.Extensions.IIdentity;
	using Crm.Library.Services.Interfaces;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Model.Lookups;
	using Crm.PerDiem.Services.Interfaces;

	using log4net;

	using Microsoft.Extensions.Hosting;

	using Quartz;

	[DisallowConcurrentExecution]
	public class PerDiemReportSenderAgent : JobBase, IDocumentGeneratorService
	{
		private readonly ILog logger;
		private readonly IPerDiemReportService perDiemReportService;
		private readonly IRepositoryWithTypedId<PerDiemReport, Guid> perDiemReportRepository;
		private readonly IUserService userService;
		private readonly IClientSideGlobalizationService clientSideGlobalizationService;
		private readonly IRepositoryWithTypedId<PerDiemEntry, Guid> perDiemEntryRepository;

		public PerDiemReportSenderAgent(
			ISessionProvider sessionProvider,
			ILog logger,
			IPerDiemReportService perDiemReportService,
			IRepositoryWithTypedId<PerDiemReport, Guid> perDiemReportRepository,
			IUserService userService,
			IHostApplicationLifetime hostApplicationLifetime,
			IClientSideGlobalizationService clientSideGlobalizationService,
			IRepositoryWithTypedId<PerDiemEntry, Guid> perDiemEntryRepository)
			: base(
				sessionProvider,
				logger,
				hostApplicationLifetime)
		{
			this.logger = logger;
			this.perDiemReportService = perDiemReportService;
			this.perDiemReportRepository = perDiemReportRepository;
			this.userService = userService;
			this.clientSideGlobalizationService = clientSideGlobalizationService;
			this.perDiemEntryRepository = perDiemEntryRepository;
		}

		protected override void Run(IJobExecutionContext context)
		{
			var perDiemReportIds = GetPendingDocuments().Cast<PerDiemReport>().ToList().Select(x => x.Id);

			var logMessage = new StringBuilder();
			foreach (var perDiemReportId in perDiemReportIds)
			{
				try
				{
					BeginRequest();
					var perDiemReport = perDiemReportRepository.Get(perDiemReportId);
					var isEmpty = !perDiemEntryRepository.GetAll().Any(e => e.PerDiemReportId == perDiemReport.Id);
					if (isEmpty)
					{
						logMessage.AppendLine("PerDiemReport(" + perDiemReport.From + "-" + perDiemReport.To + ") for " + perDiemReport.User.DisplayName + " closed, but was empty, therefore no notification email sent.");
						perDiemReport.IsSent = true;
						continue;
					}
					var user = userService.GetUser(perDiemReport.CreateUser);
					if (user != null)
					{
						Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(user.GetIdentityString()), new string[0]);
						Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(clientSideGlobalizationService.GetCurrentLanguageCultureNameOrDefault());
						Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(clientSideGlobalizationService.GetCurrentCultureNameOrDefault());
					}
					else
					{
						logger.Warn($"could not set user '{perDiemReport.CreateUser}' for localization");
					}
					if (perDiemReportService.SendReportAsPdf(perDiemReport))
					{
						perDiemReport.IsSent = true;
					}
					else
					{
						var recipientEmails = perDiemReportService.GetDefaultReportRecipientEmails();
						var msg = new StringBuilder($"Could not send report {perDiemReport.Id}. ");
						if (!recipientEmails.Any())
						{
							msg.Append("No recipients found. Please contact your system administrator.");
						}
						else
						{
							msg.Append($"{recipientEmails.Length} recipients are present, reason probably wrong status.");
						}

						logger.Warn(msg.ToString());
						perDiemReport.SendingError = msg.ToString();
					}
					perDiemReportRepository.SaveOrUpdate(perDiemReport);
					if (logMessage.ToString() != String.Empty)
					{
						Logger.Info(logMessage);
					}
				}

				catch (Exception exception)
				{
					EndRequest();
					BeginRequest();
					var perDiemReport = perDiemReportRepository.Get(perDiemReportId);
					logger.Error($"failed generating per diem report {perDiemReport.Id}", exception);
					perDiemReport.IsSent = false;
					perDiemReport.SendingError = exception.ToString();
					perDiemReportRepository.SaveOrUpdate(perDiemReport);
				}
				finally
				{
					EndRequest();
				}
			}
		}
		public virtual IQueryable GetFailedDocuments()
		{
			return perDiemReportRepository.GetAll().Where(x => x.StatusKey == PerDiemReportStatus.ClosedKey && !x.IsSent && x.SendingError != null);
		}
		public virtual IQueryable GetPendingDocuments()
		{
			return perDiemReportRepository.GetAll().Where(x => x.StatusKey == PerDiemReportStatus.ClosedKey && !x.IsSent && x.SendingError == null);
		}
		public virtual void Retry(Guid id)
		{
			var perDiemReport = perDiemReportRepository.Get(id);
			perDiemReport.SendingError = null;
			perDiemReportRepository.SaveOrUpdate(perDiemReport);
		}
	}
}
