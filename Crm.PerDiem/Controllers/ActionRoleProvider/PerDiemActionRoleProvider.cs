namespace Crm.PerDiem.Controllers.ActionRoleProvider
{
	using System.Linq;

	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Model.Lookups;

	public class PerDiemActionRoleProvider : RoleCollectorBase
	{
		public PerDiemActionRoleProvider(IPluginProvider pluginProvider)
			:
			base(pluginProvider)
		{
			var onlineRoleNames = new[]
			{
				MainPlugin.Roles.HeadOfSales,
				MainPlugin.Roles.SalesBackOffice, 
				MainPlugin.Roles.InternalSales,
				"HeadOfService",
				"ServiceBackOffice",
				"InternalService"
			};
			var offlineRoleNames = new[]
			{
				MainPlugin.Roles.FieldSales,
				"FieldService"
			};
			var allRoleNames = onlineRoleNames.Concat(offlineRoleNames).ToArray();

			Add(PerDiemPlugin.PermissionGroup.PerDiemReport, PerDiemPlugin.PermissionName.SeeAllUsersPerDiemReports, onlineRoleNames);
			Add(PerDiemPlugin.PermissionGroup.PerDiemReport, PerDiemPlugin.PermissionName.CreateAllUsersPerDiemReports, onlineRoleNames);
			AddImport(PerDiemPlugin.PermissionGroup.PerDiemReport, PerDiemPlugin.PermissionName.CreateAllUsersPerDiemReports, PerDiemPlugin.PermissionGroup.PerDiemReport, PerDiemPlugin.PermissionName.SeeAllUsersPerDiemReports);
			Add(PerDiemPlugin.PermissionGroup.PerDiemReport, PermissionName.View, allRoleNames);
			Add(nameof(PerDiemReportStatus), PerDiemPlugin.PermissionName.SelectNonMobileLookupValues, onlineRoleNames);
			Add(PerDiemPlugin.PermissionGroup.PerDiemReport, PerDiemPlugin.PermissionName.RequestCloseReport, allRoleNames);
			Add(PerDiemPlugin.PermissionGroup.PerDiemReport, PerDiemPlugin.PermissionName.CloseReport, onlineRoleNames);
			Add(PermissionGroup.WebApi, nameof(PerDiemReport), allRoleNames);
			Add(PermissionGroup.WebApi, nameof(PerDiemReportStatus), allRoleNames);
			Add(PermissionGroup.WebApi, nameof(PerDiemReportType), allRoleNames);
		}
	}
}