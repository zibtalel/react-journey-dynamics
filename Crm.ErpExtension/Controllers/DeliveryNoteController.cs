using Microsoft.AspNetCore.Mvc;

namespace Crm.ErpExtension.Controllers
{
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class DeliveryNoteController : Controller
	{
		public virtual ActionResult DetailsTemplate()
		{
			return PartialView();
		}
		
		[RenderAction("DeliveryNoteDetailsMaterialTabHeader", Priority = 100)]
		public virtual ActionResult MaterialDetailsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("DeliveryNoteDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialDetailsTab()
		{
			return PartialView();
		}
	}
}
