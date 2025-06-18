using Microsoft.AspNetCore.Mvc;

namespace Crm.Project.Controllers
{

	using Crm.Library.Model;
	using Crm.Library.Modularization;
	using Crm.ViewModels;

	public class CompanyDetailsController : Controller
	{
		[RenderAction("CompanyDetailsMaterialTabHeader", Priority = 50)]
		[RequiredPermission(ProjectPlugin.PermissionName.ProjectTab, Group = MainPlugin.PermissionGroup.Company)]
		public virtual ActionResult MaterialProjectsTabHeader()
		{
			return PartialView();
		}
		[RenderAction("CompanyDetailsMaterialTab", Priority = 50)]
		[RequiredPermission(ProjectPlugin.PermissionName.ProjectTab, Group = MainPlugin.PermissionGroup.Company)]
		public virtual ActionResult MaterialProjectsTab()
		{
			return PartialView(new CrmModel());
		}
		[RenderAction("CompanyDetailsMaterialTabHeader", Priority = 50)]
		[RequiredPermission(ProjectPlugin.PermissionName.PotentialTab, Group = MainPlugin.PermissionGroup.Company)]
		public virtual ActionResult MaterialPotentialsTabHeader()
		{
			return PartialView();
		}
		[RenderAction("CompanyDetailsMaterialTab", Priority = 50)]
		[RequiredPermission(ProjectPlugin.PermissionName.PotentialTab, Group = MainPlugin.PermissionGroup.Company)]
		public virtual ActionResult MaterialPotentialsTab()
		{
			return PartialView(new CrmModel());
		}
	}
}
