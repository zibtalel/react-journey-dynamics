using Microsoft.AspNetCore.Mvc;

namespace Crm.ErpExtension.Controllers
{
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class CreditNoteController : Controller
	{
		public virtual ActionResult DetailsTemplate()
		{
			return PartialView();
		}
		
		[RenderAction("CreditNoteDetailsMaterialTabHeader", Priority = 100)]
		public virtual ActionResult MaterialDetailsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("CreditNoteDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialDetailsTab()
		{
			return PartialView();
		}
	}
}
