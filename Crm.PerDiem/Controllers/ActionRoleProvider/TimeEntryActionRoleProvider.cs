namespace Crm.PerDiem.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Model.Lookups;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Model.Lookups;

	public class TimeEntryActionRoleProvider : RoleCollectorBase
	{
		public TimeEntryActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			var salesRoleNames = new[] { MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales };

			Add(PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Create, salesRoleNames);
			AddImport(PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Create, PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Edit);
			Add(PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Edit, salesRoleNames);
			Add(PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Delete, salesRoleNames);
			AddImport(PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Delete, PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Edit);
			Add(PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.View, salesRoleNames);
			Add(PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Index, salesRoleNames);
			Add(PerDiemPlugin.PermissionGroup.TimeEntry, PerDiemPlugin.PermissionName.SeeAllUsersTimeEntries, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales);
			AddImport(PerDiemPlugin.PermissionGroup.TimeEntry, PerDiemPlugin.PermissionName.SeeAllUsersTimeEntries, PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Index);
			Add(PerDiemPlugin.PermissionGroup.TimeEntry, MainPlugin.PermissionName.DownloadAsPdf, salesRoleNames);
			AddImport(PerDiemPlugin.PermissionGroup.TimeEntry, MainPlugin.PermissionName.DownloadAsPdf, PerDiemPlugin.PermissionGroup.TimeEntry, PermissionName.Index);
			Add(nameof(TimeEntryType), PerDiemPlugin.PermissionName.SelectNonMobileLookupValues, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales);
			Add(PermissionGroup.WebApi, nameof(UserTimeEntry), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(TimeEntryType), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(CostCenter), MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
		}
	}
}