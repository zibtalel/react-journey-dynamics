namespace Crm.ErpExtension.Controllers.ActionRoleProvider
{
	using Crm.ErpExtension.Model;
	using Crm.ErpExtension.Model.Lookups;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class ErpDocumentActionRoleProvider : RoleCollectorBase
	{
		public ErpDocumentActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			var defaultRoleNames = new[] { MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService" };
			Add(ErpPlugin.PermissionGroup.CreditNoteList, PermissionName.View, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.DeliveryNoteList, PermissionName.View, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.InvoiceList, PermissionName.View, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.MasterContractList, PermissionName.View, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.QuoteList, PermissionName.View, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.SalesOrderList, PermissionName.View, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.Integration, PermissionName.View, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, "HeadOfService", "ServiceBackOffice");

			Add(ErpPlugin.PermissionGroup.CreditNoteList, PermissionName.Index, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.DeliveryNoteList, PermissionName.Index, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.InvoiceList, PermissionName.Index, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.MasterContractList, PermissionName.Index, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.QuoteList, PermissionName.Index, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.SalesOrderList, PermissionName.Index, defaultRoleNames);
			Add(ErpPlugin.PermissionGroup.Integration, PermissionName.Index, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, "HeadOfService", "ServiceBackOffice");

			Add(PermissionGroup.WebApi, nameof(CreditNote), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(CreditNotePosition), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(DeliveryNote), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(DeliveryNotePosition), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ErpDocumentStatus), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ErpTurnover), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(Invoice), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(InvoicePosition), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(MasterContract), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(MasterContractPosition), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(Quote), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(QuotePosition), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(SalesOrder), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(SalesOrderPosition), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", Roles.APIUser);
		}
	}
}
