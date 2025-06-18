namespace Crm.Documentation
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.CompilerServices;

	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Menu;

	using PermissionGroup = DocumentationPlugin.PermissionGroup;

	public class DocumentationMenuRegistrar : IMenuRegistrar<MaterialMainMenu>
	{
		[MethodImpl(MethodImplOptions.NoInlining)]
		public virtual void Initialize(MenuProvider<MaterialMainMenu> menuProvider)
		{
			menuProvider.Register(null, "Help", iconClass: "zmdi zmdi-help", priority: - 110);
			menuProvider.Register("Help", "Application", url: "~/Crm.Documentation/Documentation/Index", priority: Int32.MinValue + 15, htmlAttributes: new Dictionary<string, object> { { "target", "_blank" }, { "name", "Application" } });
			menuProvider.AddPermission("Help", "Application", PermissionGroup.ApplicationHelp, PermissionName.View);
			menuProvider.Register("Help", "Developer", url: "~/Crm.Documentation/Documentation/DeveloperIndex", priority: Int32.MinValue + 10, htmlAttributes: new Dictionary<string, object> { { "target", "_blank" }, { "name", "Developer" } });
			menuProvider.AddPermission("Help", "Developer", PermissionGroup.DeveloperHelp, PermissionName.View);
			menuProvider.Register("Help", "API Explorer", url: "~/api/swagger", priority: Int32.MinValue + 5, htmlAttributes: new Dictionary<string, object> { { "target", "_blank" }, { "name", "API Explorer" } });
			menuProvider.AddPermission("Help", "API Explorer", DocumentationPlugin.PermissionGroup.DeveloperHelp, PermissionName.View);
		}
	}
}
