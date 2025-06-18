namespace Crm.Project.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ProjectContactRelationshipController : Controller
	{
		[RenderAction("ProjectDetailsMaterialTabHeader", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult ProjectDetailsMaterialRelationshipTabHeader() => PartialView();
		[RenderAction("ProjectDetailsMaterialTab", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult ProjectDetailsMaterialRelationshipTab() => PartialView();

		[RequiredPermission(ProjectPlugin.PermissionName.EditContactRelationship, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult EditTemplate() => PartialView("../Relationship/EditTemplate");

		[RenderAction("CompanyDetailsRelationshipTypeExtension", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult CompanyDetailsRelationshipTypeExtension() => PartialView();

		[RenderAction("CompanyDetailsRelationshipTypeActionExtension", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult CompanyDetailsRelationshipTypeActionExtension() => PartialView("MaterialRelationshipAction");

		[RenderAction("PersonDetailsRelationshipTypeExtension", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult PersonDetailsRelationshipTypeExtension() => PartialView();

		[RenderAction("PersonDetailsRelationshipTypeActionExtension", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult PersonDetailsRelationshipTypeActionExtension() => PartialView("MaterialRelationshipAction");

		[RenderAction("MaterialProjectItemExtensions", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]

		public virtual ActionResult MaterialProjectItemExtensions() => PartialView("MaterialItemExtensions");
		[RenderAction("MaterialCompanyItemExtensions", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialCompanyItemExtensions() => PartialView("MaterialItemExtensions");

		[RenderAction("MaterialPersonItemExtensions", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialPersonItemExtensions() => PartialView("MaterialItemExtensions");

		[RenderAction("ProjectItemTemplateActions", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult ProjectItemTemplateActions() => PartialView("MaterialItemTemplateActions");

		[RenderAction("CompanyItemTemplateActions", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult CompanyItemTemplateActions() => PartialView("MaterialItemTemplateActions");

		[RenderAction("PersonItemTemplateActions", Priority = 50)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult PersonItemTemplateActions() => PartialView("MaterialItemTemplateActions");
	}
}
