namespace Crm.Documentation.Controller.ActionRoleProvider
{
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class DocumentationActionRoleProvider : RoleCollectorBase
	{
		public DocumentationActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			var defaultRoleNames = new[] { MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", "FieldService", "ServicePlanner" };
			Add(DocumentationPlugin.PermissionGroup.ApplicationHelp, PermissionName.View, defaultRoleNames);
			Add(DocumentationPlugin.PermissionGroup.DeveloperHelp, PermissionName.View);
		}
	}
}