using Microsoft.AspNetCore.Mvc;

namespace Crm.ProjectOrders.Controllers
{
	using Crm.Library.Helper;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class ProjectOrderController : Controller
	{
		[RenderAction("MaterialHeadResource", Priority = 4990)]
		public virtual ActionResult HeadResource()
		{
			return Content(Url.JsResource("Crm.ProjectOrders","projectOrdersMaterialJs"));
		}
	}
}
