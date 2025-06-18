namespace Crm.Project.Controllers.ActionRoleProvider
{
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Project.Model;
	using Crm.Project.Model.Lookups;

	public class ProjectActionRoleProvider : RoleCollectorBase
	{
		public ProjectActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ProjectPlugin.PermissionGroup.Project, PermissionName.Create, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, PermissionName.Create, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, PermissionName.Edit, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, PermissionName.Edit, ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.AddTask);
			AddImport(ProjectPlugin.PermissionGroup.Project, PermissionName.Edit, ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SetStatus);
			Add(ProjectPlugin.PermissionGroup.Project, PermissionName.Delete, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, PermissionName.Delete, ProjectPlugin.PermissionGroup.Project, PermissionName.Edit);
			Add(ProjectPlugin.PermissionGroup.Project, PermissionName.Index, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, PermissionName.Index, PermissionGroup.WebApi, nameof(Project));
			Add(ProjectPlugin.PermissionGroup.Project, PermissionName.View, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.HeadOfSales);
			Add(ProjectPlugin.PermissionGroup.Project, PermissionName.Read, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.HeadOfSales);

			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.AssociateTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.CreateTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.CreateTag, ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.RemoveTag);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.RemoveTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.RemoveTag, ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.AssociateTag);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.RenameTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.FieldSales);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.DeleteTag, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.DeleteTag, ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.CreateTag);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.DeleteTag, ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.RenameTag);

			Add(MainPlugin.PermissionGroup.Company, ProjectPlugin.PermissionName.ProjectTab, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.HeadOfSales);
			AddImport(MainPlugin.PermissionGroup.Company, ProjectPlugin.PermissionName.ProjectTab, MainPlugin.PermissionGroup.Company, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.NotesTab, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.NotesTab, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SetStatus, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SetStatus, ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.HeaderStatus);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.TasksTab, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.TasksTab, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.DocumentsTab, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.DocumentsTab, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.ExportAsCsv, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.ExportAsCsv, ProjectPlugin.PermissionGroup.Project, PermissionName.Index);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.Ics, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.AddTask, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.FieldSales, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.AddTask, ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.TasksTab);
			Add(ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.ChangeFinalizedProjectStatus, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales);
			Add(ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.HeaderStatus, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.HeaderStatus, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.SelectResponsible, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, ProjectPlugin.PermissionName.SelectResponsible, ProjectPlugin.PermissionGroup.Project, PermissionName.Edit);

			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.PrivateHeaderInfo, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.PublicHeaderInfo, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.PublicHeaderInfo, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarBackgroundInfo, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarBackgroundInfo, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarBravo, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarBravo, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarContactInfo, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarContactInfo, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);
			Add(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarDropbox, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales);
			AddImport(ProjectPlugin.PermissionGroup.Project, MainPlugin.PermissionName.SidebarDropbox, ProjectPlugin.PermissionGroup.Project, PermissionName.Read);

			Add(PermissionGroup.WebApi, nameof(Project), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ProjectCategory), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ProjectCategoryGroups), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ProjectLostReasonCategory), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			Add(PermissionGroup.WebApi, nameof(ProjectStatus), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
		}
	}
}