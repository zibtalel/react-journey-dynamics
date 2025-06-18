namespace Crm.Project.BackgroundServices
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Text;

	using Crm.Library.BackgroundServices;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Data.NHibernateProvider;
	using Crm.Library.Extensions;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Model;
	using Crm.Library.Services.Interfaces;
	using Crm.Model;
	using Crm.Project.Model;
	using Crm.Project.Model.Extensions;
	using Crm.Project.Model.Lookups;
	using Crm.Project.SearchCriteria;

	using log4net;

	using Microsoft.Extensions.Hosting;

	using Quartz;

	[DisallowConcurrentExecution]
	public class UpcomingProjectNotifier : JobBase
	{
		// Members
		private readonly IRepositoryWithTypedId<Message, Guid> messageRepository;
		private readonly IUserService userService;
		private readonly IRepositoryWithTypedId<Project, Guid> projectRepository;
		private readonly DateTime toDate = DateTime.Today.LastDateOfWeek();
		private readonly IResourceManager resourceManager;
		private readonly Func<Message> messageFactory;

		// Methods
		protected override void Run(IJobExecutionContext context)
		{
			var users = userService.GetActiveUsers();
			foreach (User user in users)
			{
				if (receivedShutdownSignal)
				{
					break;
				}
				var dueProjects = GetDueProjects(user.Id).ToList();

				if (!dueProjects.Any())
				{
					continue;
				}

				var translationCulture = CultureInfo.GetCultureInfo(user.DefaultLanguageKey ?? "en");

				var body = new StringBuilder();
				body.AppendLine(resourceManager.GetTranslation("UpcomingProjectEmailBody", translationCulture).WithArgs(user.FirstName, dueProjects.Count));
				foreach (Project dueProject in dueProjects.OrderBy(x => x.DueDate))
				{
					body.AppendLine(String.Format("- {0:d}: {1} ({2}) / {3} {4:N2}", dueProject.DueDate, dueProject.Name, dueProject.ParentName, dueProject.CurrencyKey, dueProject.Value.GetValueOrDefault()));
				}
				body.AppendLine("\n" + resourceManager.GetTranslation("GeneratedEmailMessage", translationCulture));

				var message = messageFactory();
				message.Recipients.Add(user.Email);
				message.Subject = resourceManager.GetTranslation("UpcomingProjectEmailSubject", translationCulture).WithArgs(toDate);
				message.Body = body.ToString();
				message.CreateUser = "Upcoming Project Notifier";
				messageRepository.SaveOrUpdate(message);
			}
		}

		protected virtual IEnumerable<Project> GetDueProjects(string username)
		{
			var projectCriteria = new ProjectSearchCriteria
			{
				ResponsibleUser = username,
				ToDate = toDate,
				ProjectStatusKey = ProjectStatus.OpenKey,
				IsActive = true
			};

			return projectRepository.GetAll().FilterProjectsBySearchCriteria(projectCriteria);
		}

		// Constructor
		public UpcomingProjectNotifier(IUserService userService, IRepositoryWithTypedId<Message, Guid> messageRepository, IRepositoryWithTypedId<Project, Guid> projectRepository, ISessionProvider sessionProvider, IResourceManager resourceManager, ILog logger, Func<Message> messageFactory, IHostApplicationLifetime hostApplicationLifetime)
			: base(sessionProvider, logger, hostApplicationLifetime)
		{
			this.userService = userService;
			this.messageRepository = messageRepository;
			this.projectRepository = projectRepository;
			this.resourceManager = resourceManager;
			this.messageFactory = messageFactory;
		}
	}
}
