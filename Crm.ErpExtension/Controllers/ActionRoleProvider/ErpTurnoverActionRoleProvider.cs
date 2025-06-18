namespace Crm.ErpExtension.Controllers.ActionRoleProvider
{
	using Crm.ErpExtension.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class ErpTurnoverActionRoleProvider : RoleCollectorBase
	{
		public ErpTurnoverActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.TurnoverTransaction, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.TurnoverTransaction, MainPlugin.PermissionGroup.Company, PermissionName.Read);
			Add(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.TurnoverTransactionDetails, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.TurnoverTransactionDetails, MainPlugin.PermissionGroup.Company, PermissionName.Read);

			Add(PermissionGroup.WebApi, nameof(ErpTurnover), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales);
		}
	}
}