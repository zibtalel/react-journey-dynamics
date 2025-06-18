namespace Crm.Project.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Project.Model;
	using Crm.Project.Model.Lookups;
	using Crm.Project.Model.Relationships;

	public class PotentialActionRoleProvider : RoleCollectorBase
	{
		public PotentialActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ProjectPlugin.PermissionGroup.Potential, PermissionName.View, MainPlugin.Roles.FieldSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			Add(ProjectPlugin.PermissionGroup.Potential, PermissionName.Index, MainPlugin.Roles.FieldSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, PermissionName.Index, PermissionGroup.WebApi, nameof(Potential));
			Add(ProjectPlugin.PermissionGroup.Potential, PermissionName.Create, MainPlugin.Roles.FieldSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, PermissionName.Create, ProjectPlugin.PermissionGroup.Potential, PermissionName.Index);
			Add(ProjectPlugin.PermissionGroup.Potential, PermissionName.Read, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.HeadOfSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, PermissionName.Read, ProjectPlugin.PermissionGroup.Potential, PermissionName.Index);
			Add(ProjectPlugin.PermissionGroup.Potential, PermissionName.Edit, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, PermissionName.Edit, ProjectPlugin.PermissionGroup.Potential, PermissionName.Read);
			AddImport(ProjectPlugin.PermissionGroup.Potential, PermissionName.Edit, ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.SetStatus);

			Add(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.SetStatus, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			Add(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.EditContactPerson, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.EditContactPerson, ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.ContactHistoryTab);
			Add(ProjectPlugin.PermissionGroup.Potential, PermissionName.ExportAsCsv, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.SalesBackOffice);
			AddImport(ProjectPlugin.PermissionGroup.Potential, PermissionName.ExportAsCsv, ProjectPlugin.PermissionGroup.Potential, PermissionName.Index);

			Add(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.TasksTab, MainPlugin.Roles.FieldSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.TasksTab, ProjectPlugin.PermissionGroup.Potential, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.ProjectTab, MainPlugin.Roles.FieldSales, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.ProjectTab, ProjectPlugin.PermissionGroup.Potential, PermissionName.Read);
			Add(MainPlugin.PermissionGroup.Company, ProjectPlugin.PermissionName.PotentialTab, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.HeadOfSales);
			AddImport(MainPlugin.PermissionGroup.Company, ProjectPlugin.PermissionName.PotentialTab, ProjectPlugin.PermissionGroup.Potential, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.NotesTab, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.NotesTab, ProjectPlugin.PermissionGroup.Potential, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.ContactHistoryTab, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, ProjectPlugin.PermissionName.ContactHistoryTab, ProjectPlugin.PermissionGroup.Potential, PermissionName.Read);

			Add(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.AssociateTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			Add(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.CreateTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.CreateTag, ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.RemoveTag);
			Add(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.RemoveTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.RemoveTag, ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.AssociateTag);
			Add(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.RenameTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.FieldSales);
			Add(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.DeleteTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales);
			AddImport(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.DeleteTag, ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.CreateTag);
			AddImport(ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.DeleteTag, ProjectPlugin.PermissionGroup.Potential, MainPlugin.PermissionName.RenameTag);

			Add(PermissionGroup.WebApi, nameof(Potential), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(PotentialStatus), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(PotentialPriority), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(PotentialContactRelationshipType), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(PotentialContactRelationship), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
		}
	}
}
