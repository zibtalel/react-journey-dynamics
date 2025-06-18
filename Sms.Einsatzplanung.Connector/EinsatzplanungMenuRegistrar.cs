namespace Sms.Einsatzplanung.Connector
{
	using System;

	using Crm.Library.Model.Authorization;
	using Crm.Library.Modularization.Menu;

	using J2N.Collections.Generic;

	using Sms.Einsatzplanung.Connector.Controllers;
	using Sms.Einsatzplanung.Connector.Model;

	public class EinsatzplanungMenuRegistrar : IMenuRegistrar<MaterialMainMenu>, IMenuRegistrar<MaterialUserProfileMenu>
	{
		public virtual void Initialize(MenuProvider<MaterialMainMenu> menuProvider)
		{
			menuProvider.Register("Administration", "SchedulerAdministration", url: $"~/Sms.Einsatzplanung.Connector/SchedulerMaterial/Admin", priority: 50);
			menuProvider.AddPermission("Administration", "SchedulerAdministration", PermissionGroup.WebApi, nameof(Scheduler));
		}
		public virtual void Initialize(MenuProvider<MaterialUserProfileMenu> menuProvider)
		{ 
			menuProvider.Register(null, "SchedulerLoginTitle", url: $"~/{EinsatzplanungConnectorPlugin.PluginName}/{SchedulerController.Name}/{nameof(SchedulerController.DownloadReleasedApplicationManifest)}", iconClass: "zmdi zmdi-desktop-windows", priority: Int32.MaxValue - 40, htmlAttributes: new Dictionary<string, object> { { "target", "_blank" } });
			menuProvider.AddPermission(null, "SchedulerLoginTitle", PermissionGroup.Login, EinsatzplanungConnectorPlugin.PermissionName.Scheduler);
		}
	}
}
