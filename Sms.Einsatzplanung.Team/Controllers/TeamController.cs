namespace Sms.Einsatzplanung.Team.Controllers
{
	using Crm;
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Modularization;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class TeamController : Controller
	{
		[RenderAction("MaterialHeadResource", Priority = 980)]
		public virtual ActionResult HeadResource() => Content(Url.JsResource("Sms.Einsatzplanung.Team", "smsEinsatzplanungTeamMaterialTs"));

		[RenderAction("UserGroupEdit", Priority = 100)]
		public virtual ActionResult UserGroupEdit() => PartialView();

		[RequiredPermission(MainPlugin.PermissionName.AssignUserGroup, Group = PermissionGroup.UserAdmin)]
		[RenderAction("UserGroupRightActionExtensions")]
		public virtual ActionResult UserGroupRightActionExtensions() => PartialView();
	}
}
