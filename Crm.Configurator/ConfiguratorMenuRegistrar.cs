namespace Crm.Configurator
{
	using System;

	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Menu;

	public class ConfiguratorMenuRegistrar : IMenuRegistrar<MaterialMainMenu>
	{
		public virtual void Initialize(MenuProvider<MaterialMainMenu> menuProvider)
		{
			menuProvider.Register(null, "Sales", iconClass: "zmdi zmdi-money-box", priority: Int32.MaxValue - 100);
			menuProvider.Register("Sales", "Configurator", url: "~/Crm.Configurator/Configuration/IndexTemplate", iconClass: "zmdi zmdi-widgets", priority: 200);
			menuProvider.AddPermission("Sales", "Configurator", ConfiguratorPlugin.PermissionGroup.Configurator, PermissionName.View);
		}
	}
}
