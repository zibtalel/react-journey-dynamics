namespace Crm.Project.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class GenericListActionRoleProvider : RoleCollectorBase
	{
		public GenericListActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.ExportAsCsv, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.Ics, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.Rss, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
		}
	}
}