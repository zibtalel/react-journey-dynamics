namespace Sms.Einsatzplanung.Connector.Controllers
{
	using Crm.Library.Helper;
	using Crm.Library.Modularization;

	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;


	[Authorize]
	public class UserController : Controller
	{
		[RenderAction("AccountInfoTab", Priority = 50)]
		[RenderAction("UserDetailsTabExtensions", Priority = 50)]
		public virtual ActionResult HomeAddress() => PartialView();

		[RenderAction("MaterialHeadResource", Priority = 1600)]
		[AllowAnonymous]
		public virtual ActionResult MaterialHeadResource() => Content(Url.JsResource(EinsatzplanungConnectorPlugin.PluginName, "schedulerMaterialJs"));

		[RenderAction("AccountInfoView", Priority = 30)]
		[RenderAction("UserDetailsGeneralView", Priority = -10)]
		public virtual ActionResult PublicHolidayRegionView() => PartialView();

		[RenderAction("AccountInfoEdit", Priority = 30)]
		[RenderAction("UserDetailsGeneralEdit", Priority = -10)]
		public virtual ActionResult PublicHolidayRegionEdit() => PartialView();
	}
}
