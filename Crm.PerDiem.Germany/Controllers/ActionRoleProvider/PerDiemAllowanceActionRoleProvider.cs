namespace Crm.PerDiem.Germany.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.PerDiem.Germany.Model;
	using Crm.PerDiem.Germany.Model.Lookups;

	public class PerDiemAllowanceActionRoleProvider : RoleCollectorBase
	{
		public PerDiemAllowanceActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			var defaultRoleNames = new[]
			{
				MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales,
				"HeadOfService", "ServiceBackOffice", "InternalService", "FieldService"
			};

			Add(nameof(PerDiemAllowanceEntry), PermissionName.Create, defaultRoleNames);
			AddImport(nameof(PerDiemAllowanceEntry), PermissionName.Create, nameof(PerDiemAllowanceEntry), PermissionName.Edit);
			Add(nameof(PerDiemAllowanceEntry), PermissionName.Delete, defaultRoleNames);
			AddImport(nameof(PerDiemAllowanceEntry), PermissionName.Delete, nameof(PerDiemAllowanceEntry), PermissionName.Edit);
			Add(nameof(PerDiemAllowanceEntry), PermissionName.Edit, defaultRoleNames);

			Add(nameof(PerDiemAllowanceEntryAllowanceAdjustmentReference), PermissionName.Create, defaultRoleNames);
			AddImport(nameof(PerDiemAllowanceEntryAllowanceAdjustmentReference), PermissionName.Create, nameof(PerDiemAllowanceEntryAllowanceAdjustmentReference), PermissionName.Edit);
			Add(nameof(PerDiemAllowanceEntryAllowanceAdjustmentReference), PermissionName.Delete, defaultRoleNames);
			AddImport(nameof(PerDiemAllowanceEntryAllowanceAdjustmentReference), PermissionName.Delete, nameof(PerDiemAllowanceEntryAllowanceAdjustmentReference), PermissionName.Edit);
			Add(nameof(PerDiemAllowanceEntryAllowanceAdjustmentReference), PermissionName.Edit, defaultRoleNames);

			Add(PermissionGroup.WebApi, nameof(PerDiemAllowance), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", "FieldService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(PerDiemAllowanceAdjustment), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", "FieldService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(PerDiemAllowanceEntry), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", "FieldService", Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(PerDiemAllowanceEntryAllowanceAdjustmentReference), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, "HeadOfService", "ServiceBackOffice", "InternalService", "FieldService", Roles.APIUser);
		}
	}
}