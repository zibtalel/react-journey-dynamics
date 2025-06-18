namespace Crm.ErpExtension.Controllers.ActionRoleProvider
{
	using Crm.ErpExtension.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class GenericListActionRoleProvider : RoleCollectorBase
	{
		public GenericListActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			var genericListTypes = new[]
			{
				nameof(CreditNote),
				nameof(DeliveryNote),
				nameof(Invoice),
				nameof(MasterContract),
				nameof(MasterContractPosition),
				nameof(Quote),
				nameof(QuotePosition),
				nameof(SalesOrder),
				nameof(SalesOrderPosition)
			};

			foreach (var permissionGroup in genericListTypes)
			{
				Add(permissionGroup, MainPlugin.PermissionName.ExportAsCsv, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice);
				Add(permissionGroup, MainPlugin.PermissionName.Ics, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
				Add(permissionGroup, MainPlugin.PermissionName.Rss, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			}
		}
	}
}
