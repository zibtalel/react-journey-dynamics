using Microsoft.AspNetCore.Mvc;

namespace Crm.PerDiem.Controllers
{
	using Crm.Library.Helper;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class PerDiemController : Controller
	{
		[AllowAnonymous]
		[RenderAction("MaterialHeadResource", Priority = 2100)]
		public virtual ActionResult MaterialHeadResource() => Content(Url.JsResource("Crm.PerDiem", "perDiemMaterialJs") + Url.JsResource("Crm.PerDiem", "perDiemMaterialTs"));

		[RenderAction("LookupPropertyEdit")]
		public virtual ActionResult LookupPropertyEditValidCostCenters() => PartialView("LookupPropertyEditValidCostCenters");
	}
}
