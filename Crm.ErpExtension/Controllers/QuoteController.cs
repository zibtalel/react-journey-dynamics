using Microsoft.AspNetCore.Mvc;

namespace Crm.ErpExtension.Controllers
{
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class QuoteController : Controller
	{
		public virtual ActionResult DetailsTemplate()
		{
			return PartialView();
		}
		
		[RenderAction("QuoteDetailsMaterialTabHeader", Priority = 100)]
		public virtual ActionResult MaterialDetailsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("QuoteDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialDetailsTab()
		{
			return PartialView();
		}
	}
}
