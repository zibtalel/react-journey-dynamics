namespace Crm.Project.Controllers
{
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Crm.Project.Model;
	using Crm.ViewModels;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ProjectController : Controller
	{
		[RequiredPermission(MainPlugin.PermissionName.SetStatus, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult SetProjectStatus()
		{
			return PartialView();
		}

		[RequiredPermission(PermissionName.Create, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult CreateTemplate()
		{
			return PartialView();
		}

		[RequiredPermission(PermissionName.Read, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult DetailsTemplate()
		{
			return PartialView(new CrmModel());
		}

		[RenderAction("ProjectDetailsTopMenu")]
		public virtual ActionResult TopMenu()
		{
			return PartialView();
		}

		[RenderAction("MaterialHeadResource", Priority = 5990)]
		[RequiredPermission(nameof(Crm.Project), Group = PermissionGroup.WebApi)]
		[RequiredPermission(nameof(Potential), Group = PermissionGroup.WebApi)]
		public virtual ActionResult HeadResource()
		{
			return Content(Url.JsResource("Crm.Project", "projectsMaterialJs") + Url.JsResource("Crm.Project", "projectMaterialTs"));
		}
	}
}
