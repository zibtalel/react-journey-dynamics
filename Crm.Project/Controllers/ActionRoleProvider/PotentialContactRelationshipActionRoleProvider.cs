namespace Crm.Project.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Project.Model.Lookups;
	using Crm.Project.Model.Relationships;

	public class PotentialContactRelationshipActionRoleProvider : RoleCollectorBase
	{
		public PotentialContactRelationshipActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.RemoveContactRelationship, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.RemoveContactRelationship, ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.RelationshipsTab);
			Add(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.EditContactRelationship, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.EditContactRelationship, ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.RelationshipsTab);
			Add(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.RelationshipsTab, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.RelationshipsTab, ProjectPlugin.PermissionGroup.Potential, PermissionName.Read);

			Add(PermissionGroup.WebApi, nameof(PotentialContactRelationship), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(PotentialContactRelationshipType), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
		}
	}
}