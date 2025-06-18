namespace Crm.Project.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Modularization;
	using Crm.Project.Model;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ProjectDetailsController : Controller
	{
		[RenderAction("MaterialProjectSidebarExtensions", Priority = 50)]
		public virtual ActionResult MaterialDetailsDropboxBlock()
		{
			return PartialView("ContactDetailsDropboxBlock", typeof(Project));
		}
		
		[RenderAction("ProjectDetailsMaterialTabHeader", Priority = 100)]
		public virtual ActionResult MaterialDetailsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("ProjectDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialDetailsTab()
		{
			return PartialView();
		}
		
		[RenderAction("ProjectDetailsMaterialTabHeader", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.DocumentsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialDocumentsTabHeader()
		{
			return PartialView("ContactDetails/MaterialDocumentsTabHeader");
		}

		[RenderAction("ProjectDetailsMaterialTab", Priority = 40)]
		[RequiredPermission(MainPlugin.PermissionName.DocumentsTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialDocumentsTab()
		{
			return PartialView("ContactDetails/MaterialDocumentsTab");
		}

		[RenderAction("ProjectDetailsMaterialTabHeader", Priority = 90)]
		[RequiredPermission(MainPlugin.PermissionName.NotesTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialNotesTabHeader()
	{
			return PartialView();
		}
		[RenderAction("ProjectDetailsMaterialTab", Priority = 90)]
		[RequiredPermission(MainPlugin.PermissionName.NotesTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialNotesTab()
		{
			return PartialView();
		}

		[RenderAction("ProjectDetailsMaterialTabHeader", Priority = 80)]
		[RequiredPermission(MainPlugin.PermissionName.TasksTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialTasksTabHeader()
		{
			return PartialView();
		}
		[RenderAction("ProjectDetailsMaterialTab", Priority = 80)]
		[RequiredPermission(MainPlugin.PermissionName.TasksTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialTasksTab()
		{
			return PartialView();
		}
	}
}
