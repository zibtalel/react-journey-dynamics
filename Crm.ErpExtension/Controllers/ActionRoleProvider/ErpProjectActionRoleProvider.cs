namespace Crm.ErpExtension.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class ErpProjectActionRoleProvider : RoleCollectorBase
	{
		public ErpProjectActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ErpPlugin.PermissionGroup.Project, ErpPlugin.PermissionName.ErpDocumentsTab, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(ErpPlugin.PermissionGroup.Project, ErpPlugin.PermissionName.ErpDocumentsTab, ErpPlugin.PermissionGroup.Project, PermissionName.Read);
		}
	}
}