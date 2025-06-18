namespace Crm.Project.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Crm.Project.Model;
	using Crm.ViewModels;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	using PermissionGroup = ProjectPlugin.PermissionGroup;

	[Authorize]
	public class PotentialController : Controller
	{
		[RequiredPermission(PermissionName.Create, Group = PermissionGroup.Potential)]
		public virtual ActionResult CreateTemplate()
		{
			return PartialView();
		}
	
		[RequiredPermission(PermissionName.Read, Group = PermissionGroup.Potential)]
		public virtual ActionResult DetailsTemplate()
		{
			return PartialView(new CrmModel());
		}

		[RenderAction("MaterialPotentialSidebarExtensions", Priority = 50)]
		public virtual ActionResult DropboxBlock()
		{
			return PartialView("ContactDetailsDropboxBlock", typeof(Potential));
		}

		[RenderAction("PotentialDetailsTopMenu")]
		public virtual ActionResult TopMenu()
		{
			return PartialView();
		}
	}
}
