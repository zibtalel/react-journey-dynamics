namespace Crm.Project.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class PotentialContactRelationshipController : Controller
	{
		[RenderAction("PotentialDetailsMaterialTabHeader", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult PotentialDetailsMaterialRelationshipTabHeader() => PartialView();

		[RenderAction("PotentialDetailsMaterialTab", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult PotentialDetailsMaterialRelationshipTab() => PartialView();

		[RequiredPermission(ProjectPlugin.PermissionName.EditContactRelationship, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult EditTemplate() => PartialView("../Relationship/EditTemplate");

		[RenderAction("CompanyDetailsRelationshipTypeExtension", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult CompanyDetailsRelationshipTypeExtension() => PartialView();

		[RenderAction("CompanyDetailsRelationshipTypeActionExtension", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult CompanyDetailsRelationshipTypeActionExtension() => PartialView("MaterialRelationshipAction");

		[RenderAction("PersonDetailsRelationshipTypeExtension", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult PersonDetailsRelationshipTypeExtension() => PartialView();

		[RenderAction("PersonDetailsRelationshipTypeActionExtension", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult PersonDetailsRelationshipTypeActionExtension() => PartialView("MaterialRelationshipAction");

		[RenderAction("MaterialPotentialItemExtensions", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialPotentialItemExtensions() => PartialView("MaterialItemExtensions");

		[RenderAction("MaterialCompanyItemExtensions", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialCompanyItemExtensions() => PartialView("MaterialItemExtensions");

		[RenderAction("MaterialPersonItemExtensions", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialPersonItemExtensions() => PartialView("MaterialItemExtensions");

		[RenderAction("PotentialItemTemplateActions", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult PotentialItemTemplateActions() => PartialView("MaterialItemTemplateActions");

		[RenderAction("CompanyItemTemplateActions", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult CompanyItemTemplateActions() => PartialView("MaterialItemTemplateActions");

		[RenderAction("PersonItemTemplateActions", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.RelationshipsTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult PersonItemTemplateActions() => PartialView("MaterialItemTemplateActions");
	}
}
