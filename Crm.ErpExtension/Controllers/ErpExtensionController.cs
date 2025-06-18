using Microsoft.AspNetCore.Mvc;

namespace Crm.ErpExtension.Controllers
{
	using Crm.Library.Helper;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class ErpExtensionController : Controller
	{
		[RenderAction("MaterialHeadResource", Priority = 7990)]
		public virtual ActionResult HeadResource()
		{
			return Content(Url.JsResource("Crm.ErpExtension", "erpExtensionMaterialJs"));
		}
	}
}
