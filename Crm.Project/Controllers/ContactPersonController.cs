namespace Crm.Project.Controllers
{
	using Crm.Library.Model;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ContactPersonController : Controller
	{
		[RequiredPermission(ProjectPlugin.PermissionName.EditContactPerson, Group = ProjectPlugin.PermissionGroup.Potential)]
		public virtual ActionResult EditTemplate() => PartialView();
	}
}
