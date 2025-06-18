namespace Crm.ErpExtension.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ErpProjectController : Controller
	{
		[RenderAction("ProjectDetailsMaterialTabHeader", Priority = 20)]
		[RequiredPermission(ErpPlugin.PermissionName.ErpDocumentsTab, Group = ErpPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialDocumentsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("ProjectDetailsMaterialTab", Priority = 20)]
		[RequiredPermission(ErpPlugin.PermissionName.ErpDocumentsTab, Group = ErpPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialDocumentsTab()
		{
			return PartialView();
		}
	}
}
