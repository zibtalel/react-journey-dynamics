namespace Crm.Project.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Project.Model.Lookups;
	using Crm.Project.Model.Relationships;

	public class ProjectContactRelationshipActionRoleProvider : RoleCollectorBase
	{
		public ProjectContactRelationshipActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.RemoveContactRelationship, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.RemoveContactRelationship, ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.RelationshipsTab);
			Add(ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.EditContactRelationship, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.EditContactRelationship, ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.RelationshipsTab);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.RelationshipsTab, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.RelationshipsTab, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);

			Add(PermissionGroup.WebApi, nameof(ProjectContactRelationship), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ProjectContactRelationshipType), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
		}
	}
}