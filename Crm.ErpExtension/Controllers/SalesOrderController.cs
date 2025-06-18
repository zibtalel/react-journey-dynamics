using Microsoft.AspNetCore.Mvc;

namespace Crm.ErpExtension.Controllers
{
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class SalesOrderController : Controller
	{
		public virtual ActionResult DetailsTemplate()
		{
			return PartialView();
		}
		
		[RenderAction("SalesOrderDetailsMaterialTabHeader", Priority = 100)]
		public virtual ActionResult MaterialDetailsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("SalesOrderDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialDetailsTab()
		{
			return PartialView();
		}
	}
}
