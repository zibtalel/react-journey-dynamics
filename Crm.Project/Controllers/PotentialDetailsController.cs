namespace Crm.Project.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Modularization;
	using Crm.ViewModels;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class PotentialDetailsController : Controller
	{
		[RenderAction("PotentialDetailsMaterialTabHeader", Priority = 100)]
		public virtual ActionResult MaterialDetailsTabHeader()
		{
			return PartialView();
		}
		[RenderAction("PotentialDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialDetailsTab()
		{
			return PartialView();
		}
		[RenderAction("PotentialDetailsMaterialTabHeader", Priority = 90)]
		[RequiredPermission(MainPlugin.PermissionName.NotesTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialNotesTabHeader()
		{
			return PartialView();
		}
		[RenderAction("PotentialDetailsMaterialTab", Priority = 90)]
		[RequiredPermission(MainPlugin.PermissionName.NotesTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialNotesTab()
		{
			return PartialView();
		}
		[RenderAction("PotentialDetailsMaterialTabHeader", Priority = 80)]
		[RequiredPermission(MainPlugin.PermissionName.TasksTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialTasksTabHeader()
		{
			return PartialView();
		}
		[RenderAction("PotentialDetailsMaterialTab", Priority = 80)]
		[RequiredPermission(MainPlugin.PermissionName.TasksTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialTasksTab()
		{
			return PartialView();
		}
		[RenderAction("PotentialDetailsMaterialTabHeader", Priority = 50)]
		[RequiredPermission(ProjectPlugin.PermissionName.ProjectTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialProjectsTabHeader()
		{
			return PartialView();
		}
		[RenderAction("PotentialDetailsMaterialTab", Priority = 50)]
		[RequiredPermission(ProjectPlugin.PermissionName.ProjectTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialProjectsTab()
		{
			return PartialView(new CrmModel());
		}

		[RenderAction("PotentialDetailsMaterialTabHeader", Priority = 40)]
	
		[RequiredPermission(ProjectPlugin.PermissionName.ContactHistoryTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialContactHistoryTabHeader() {
			return PartialView();
		}
		[RenderAction("PotentialDetailsMaterialTab", Priority = 40)]
		[RequiredPermission(ProjectPlugin.PermissionName.ContactHistoryTab, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult MaterialContactHistoryTab() {
			return PartialView();
		}
	}
}
