namespace Crm.ErpExtension.Controllers.ActionRoleProvider
{
	using Crm.ErpExtension.Model.Lookups;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class ErpCompanyActionRoleProvider : RoleCollectorBase
	{
		public ErpCompanyActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.OpenCompany, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.OpenCompany, MainPlugin.PermissionGroup.Company, PermissionName.Read);
			Add(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.DocumentSummary, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.DocumentSummary, MainPlugin.PermissionGroup.Company, PermissionName.Read);
			Add(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.BackgroundInformationExtension, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(ErpPlugin.PermissionGroup.Erp, ErpPlugin.PermissionName.BackgroundInformationExtension, MainPlugin.PermissionGroup.Company, PermissionName.Read);
			Add(MainPlugin.PermissionGroup.Company, ErpPlugin.PermissionName.ErpDocumentsTab, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(MainPlugin.PermissionGroup.Company, ErpPlugin.PermissionName.ErpDocumentsTab, MainPlugin.PermissionGroup.Company, PermissionName.Read);
			Add(MainPlugin.PermissionGroup.Company, ErpPlugin.PermissionName.TurnoverTab, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			AddImport(MainPlugin.PermissionGroup.Company, ErpPlugin.PermissionName.TurnoverTab, MainPlugin.PermissionGroup.Company, PermissionName.Read);
			
			Add(PermissionGroup.WebApi, nameof(ErpPaymentMethod), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ErpDeliveryMethod), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ErpPaymentTerms), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ErpTermsOfDelivery), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ErpDeliveryProhibitedReason), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ErpPartialDeliveryProhibitedReason), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);

		}
	}
}