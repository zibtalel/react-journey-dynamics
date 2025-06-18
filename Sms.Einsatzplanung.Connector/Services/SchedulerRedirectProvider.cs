using Microsoft.AspNetCore.Mvc;

namespace Sms.Einsatzplanung.Connector.Services
{
	using System.Linq;
	using Crm.Library.MobileViewEngine;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.Interfaces;
	using Crm.Services;
	using Crm.Services.Interfaces;

	using Sms.Einsatzplanung.Connector.Controllers;

	public class SchedulerRedirectProvider : IRedirectProvider
	{
		private static readonly string DownloadAction = $"~/{EinsatzplanungConnectorPlugin.PluginName}/{SchedulerController.Name}/{nameof(SchedulerController.DownloadReleasedApplicationManifest)}";
		private readonly IAuthorizationManager authorizationManager;
		private readonly ISchedulerService schedulerService;
		public SchedulerRedirectProvider(IAuthorizationManager authorizationManager, ISchedulerService schedulerService)
		{
			this.authorizationManager = authorizationManager;
			this.schedulerService = schedulerService;
		}
		protected virtual bool IsAllowed(User user, IBrowserCapabilities browserCapabilities)
		{
			if (browserCapabilities.IsMobileDevice == false
				&& authorizationManager.IsAuthorizedForAction(user, PermissionGroup.Login, EinsatzplanungConnectorPlugin.PermissionName.Scheduler)
				&& schedulerService.GetSchedulers().SingleOrDefault(x => x.IsReleased) != null)
			{
				return true;
			}
			return false;
		}
		public virtual RedirectProviderResult Index(User user, IBrowserCapabilities browserCapabilities)
		{
			if (IsAllowed(user, browserCapabilities))
			{
				return new RedirectProviderResult
				{
					Name = "Scheduler",
					Plugin = EinsatzplanungConnectorPlugin.PluginName,
					Controller = SchedulerController.Name,
					Action = nameof(SchedulerController.DownloadReleasedApplicationManifest),
					ActionResult = x => new RedirectResult(DownloadAction),
					Icon = "desktop-windows"
				};
			}
			return null;
		}
		public virtual ActionResult RedirectAfterLogin(User user, IBrowserCapabilities browserCapabilities, string returnUrl)
		{
			if (IsAllowed(user, browserCapabilities))
			{
				return new RedirectResult(DownloadAction);
			}
			return null;
		}

		public virtual string AvailableClients(User user)
		{
			if (authorizationManager.IsAuthorizedForAction(user, PermissionGroup.Login, EinsatzplanungConnectorPlugin.PermissionName.Scheduler))
			{
				return EinsatzplanungConnectorPlugin.PermissionName.Scheduler;
			}
			return null;
		}
	}
}
