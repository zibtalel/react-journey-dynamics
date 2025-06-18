namespace Crm.ErpExtension.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class ErpPersonActionRoleProvider : RoleCollectorBase
	{
		public ErpPersonActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.OpenPerson, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.OpenPerson, MainPlugin.PermissionGroup.Company, PermissionName.Read);
		}
	}
}