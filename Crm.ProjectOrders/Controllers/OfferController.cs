using Microsoft.AspNetCore.Mvc;

namespace Crm.ProjectOrders.Controllers
{
	using Crm.Library.Modularization;
	using Crm.ViewModels;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class OfferController : Controller
	{
		
		[RenderAction("OfferItemTemplateDetails")]
		public virtual ActionResult OfferItemTemplateDetails()
		{
			return PartialView("BaseOrderItemTemplateDetails");
		}

		[RenderAction("OfferLoadItemTemplateDetails")]
		public virtual ActionResult OfferLoadItemTemplateDetails()
		{
			return PartialView(new CrmModel());
		}

		[RenderAction("OfferCreateForm", "OfferSaveForm")]
		public virtual ActionResult OfferSaveForm()
		{
			return PartialView(new CrmModel());
		}

		[RenderAction("OfferSummaryGeneral", Priority = 10)]
		public virtual ActionResult OfferSummaryGeneral()
		{
			return PartialView(new CrmModel());
		}
	}
}
