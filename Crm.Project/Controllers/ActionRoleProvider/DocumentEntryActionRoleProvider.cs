namespace Crm.Project.Controllers.ActionRoleProvider {
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Project.Model;

	public class DocumentEntryActionRoleProvider : RoleCollectorBase {
		public DocumentEntryActionRoleProvider(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
			Add(ProjectPlugin.PermissionGroup.DocumentEntry, PermissionName.Delete, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			Add(ProjectPlugin.PermissionGroup.DocumentEntry, PermissionName.Edit, MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales);
			AddImport(ProjectPlugin.PermissionGroup.DocumentEntry, PermissionName.Delete,  ProjectPlugin.PermissionGroup.DocumentEntry, PermissionName.Edit);
			Add(PermissionGroup.WebApi, nameof(DocumentEntry), MainPlugin.Roles.SalesBackOffice, MainPlugin.Roles.HeadOfSales, MainPlugin.Roles.InternalSales, MainPlugin.Roles.FieldSales, Roles.APIUser);
			AddImport(PermissionGroup.WebApi, nameof(DocumentEntry), ProjectPlugin.PermissionGroup.DocumentEntry, PermissionName.Delete);
		}
	}
}