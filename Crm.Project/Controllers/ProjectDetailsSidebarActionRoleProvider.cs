namespace Crm.Project.Controllers
{
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;

	public class ProjectDetailsSidebarActionRoleProvider : RoleCollectorBase
	{
		public ProjectDetailsSidebarActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarBravo, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarContactInfo, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarBackgroundInfo, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarDropbox, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, "HeadOfService", "ServiceBackOffice", "InternalService");
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarDocumentArchive, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, "HeadOfService", "ServiceBackOffice", "InternalService");
		}
	}
}