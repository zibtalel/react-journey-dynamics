using Microsoft.AspNetCore.Mvc;

namespace Crm.Project.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class DashboardController : Controller
	{
		[RenderAction("DashboardMiniChart", Priority = 2000)]
		[RequiredPermission(PermissionName.Index, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult OpenProjects()
		{
			return PartialView();
		}

		[RenderAction("DashboardMiniChart", Priority = 1500)]
		[RequiredPermission(PermissionName.Index, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult OverdueProjects()
		{
			return PartialView();
		}
	}
}
