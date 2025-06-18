using Microsoft.AspNetCore.Mvc;

namespace Sms.Einsatzplanung.Connector.Controllers
{
	using Crm.Library.Extensions;
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Site;
	using Crm.Library.Services.Interfaces;
	using Crm.ViewModels;
	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.Services;
	using Sms.Einsatzplanung.Connector.ViewModels;
	using System;
	using System.Linq;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class SchedulerController : Controller
	{
		public static readonly string Name = nameof(SchedulerController).Replace("Controller", "");
		private readonly ISchedulerService schedulerService;
		private readonly IUserService userService;
		private readonly IAppSettingsProvider appSettingsProvider;
		private readonly Site site;
		private readonly IRuleValidationService ruleValidationService;
		public SchedulerController(ISchedulerService schedulerService, IUserService userService, IAppSettingsProvider appSettingsProvider, Site site, IRuleValidationService ruleValidationService)
		{
			this.schedulerService = schedulerService;
			this.userService = userService;
			this.appSettingsProvider = appSettingsProvider;
			this.site = site;
			this.ruleValidationService = ruleValidationService;
		}

		[RequiredRole(RoleName.Administrator)]
		public virtual ActionResult Schedulers()
		{
			var schedulers = schedulerService.GetSchedulers()
				.OrderByDescending(x => x.ClickOnceVersion)
				.ThenByDescending(x => x.VersionString)
				.ThenByDescending(x => x.ModifyDate);
			return PartialView(nameof(Schedulers), schedulers);
		}

		protected virtual string GetWarningKey(string releasedBaseVersion, Version version)
		{
			var result = string.Compare(version?.ToString(3), releasedBaseVersion, StringComparison.Ordinal);
			if (result == 0)
			{
				return "SchedulerCreatePackageSameVersionConfirmation";
			}
			if (result < 0)
			{
				return "SchedulerCreatePackageLowerVersionConfirmation";
			}
			return null;
		}

		[HttpGet]
		[RequiredRole(RoleName.Administrator)]
		public virtual ActionResult CreatePackage(string fileName)
		{
			var hostUri = site.GetExtension<DomainExtension>().HostUri;
			var pluginNamespace = typeof(EinsatzplanungConnectorPlugin).Namespace;
			var path = hostUri.PathAndQuery.AppendIfMissing("/") + $"{pluginNamespace}/{Name}/{nameof(DownloadApplicationManifest)}";
			var uriBuilder = hostUri.IsDefaultPort ? new UriBuilder(hostUri.Scheme, hostUri.Host) { Path = path } : new UriBuilder(hostUri.Scheme, hostUri.Host, hostUri.Port, path);
			schedulerService.CreatePackage(fileName, uriBuilder.ToString());
			return Schedulers();
		}

		[HttpGet]
		[RequiredRole(RoleName.Administrator)]
		public virtual ActionResult ReleasePackage(Guid id)
		{
			schedulerService.ReleasePackage(id);
			return Schedulers();
		}

		[HttpGet]
		[RequiredRole(RoleName.Administrator)]
		public virtual ActionResult DeletePackage(Guid id)
		{
			schedulerService.DeletePackage(id);
			return Schedulers();
		}

		[HttpPost]
		[RequiredRole(RoleName.Administrator)]
		public virtual ActionResult SaveSettingFiles(SchedulerIcon schedulerIcon, SchedulerConfigViewModel config)
		{
			using (config)
			{
				var ruleViolations = ruleValidationService.GetRuleViolations(schedulerIcon, config);
				if (ruleViolations.Any())
				{
					var model = new CrmModel();
					model.AddRuleViolations(ruleViolations);
					return PartialView("SettingsEditor", model);
				}

				if (schedulerIcon != null)
				{
					schedulerService.SaveIcon(schedulerIcon);
				}

				if (config != null)
				{
					schedulerService.SaveConfig(config.FileInfo, config.Type);
				}
				return Settings();
			}
		}

		protected virtual string GetConfigFileName(SchedulerConfig config) => $"SchedulerConfigTransformation_{config.ModifyDate.ToLocalTime():yy-MM-dd_hh-mm-ss}.zip";

		[RequiredRole(RoleName.Administrator)]
		public virtual ActionResult Settings()
		{
			var config = schedulerService.GetCurrentSchedulerConfig();
			var icon = schedulerService.GetCurrentSchedulerIcon();
			string iconB64 = null;
			if (icon != null)
			{
				iconB64 = Convert.ToBase64String(icon.Icon);
			}
			string configFileName = null;
			if (config != null)
			{
				configFileName = GetConfigFileName(config);
			}
			return PartialView(nameof(Settings), new SchedulerSettingsViewModel
			{
				Icon = iconB64,
				ConfigId = config?.Id,
				ConfigFileName = configFileName,
				Name = appSettingsProvider.GetValue(EinsatzplanungConnectorPlugin.Settings.System.SetupName),
				Flavor = appSettingsProvider.GetValue(EinsatzplanungConnectorPlugin.Settings.System.SetupFlavor)
			});
		}

		[HttpGet]
		[RequiredRole(RoleName.Administrator)]
		[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
		public virtual ActionResult DownloadConfig(Guid id)
		{
			var config = schedulerService.GetSchedulerConfig(id);
			if (config?.Id == id)
			{
				return new FileContentResult(config.Config, "application/zip")
				{
					FileDownloadName = GetConfigFileName(config)
				};
			}
			return new EmptyResult();
		}

		[RequiredPermission(EinsatzplanungConnectorPlugin.PermissionName.Scheduler, Group = PermissionGroup.Login)]
		[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
		public virtual ActionResult DownloadApplicationManifest(Guid id)
		{
			var currentToken = userService.CurrentUser.GeneralToken;
			var manifest = schedulerService.GetDeploymentManifest(id, currentToken);
			return new FileContentResult(manifest.Content, "application/octet-stream")
			{
				FileDownloadName = manifest.Name
			};
		}

		[RequiredPermission(EinsatzplanungConnectorPlugin.PermissionName.Scheduler, Group = PermissionGroup.Login)]
		[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
		public virtual ActionResult DownloadReleasedApplicationManifest()
		{
			var releasedScheduler = schedulerService.GetSchedulers().SingleOrDefault(x => x.IsReleased);
			if (releasedScheduler != null)
			{
				var currentToken = userService.CurrentUser.GeneralToken;
				var manifest = schedulerService.GetDeploymentManifest(releasedScheduler.Id, currentToken, nameof(DownloadReleasedApplicationManifest));
				return new FileContentResult(manifest.Content, "application/octet-stream")
				{
					FileDownloadName = manifest.Name
				};
			}
			return View("Error/Error", ErrorViewModel.NotFound);
		}

		[RequiredPermission(EinsatzplanungConnectorPlugin.PermissionName.Scheduler, Group = PermissionGroup.Login)]
		[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
		public virtual ActionResult DownloadApplicationFiles(string schedulerDirectory, string relativePath)
		{
			var file = schedulerService.GetApplicationFile(schedulerDirectory, relativePath);
			return new FileStreamResult(file.OpenRead(), "application/octet-stream");
		}
	}
}
