using Microsoft.AspNetCore.Mvc;

namespace Crm.ProjectOrders.Controllers
{
	using Crm.Library.Modularization;
	using Crm.ViewModels;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class OrderController : Controller
	{
		[RenderAction("OrderForm")]
		public virtual ActionResult OrderFormProjectInformation()
		{
			return PartialView();
		}

		[RenderAction("OrderItemTemplateDetails")]
		public virtual ActionResult OrderItemTemplateDetails()
		{
			return PartialView("BaseOrderItemTemplateDetails");
		}
		
		[RenderAction("OrderItemTemplateLeftColumn", "OfferItemTemplateLeftColumn")]
		public virtual ActionResult ItemTemplateLeftColumn()
		{
			return PartialView();
		}
		[RenderAction("OrderLoadItemTemplateDetails")]
		public virtual ActionResult OrderLoadItemTemplateDetails()
		{
			return PartialView(new CrmModel());
		}

		[RenderAction("OrderCreateForm", "OrderSaveForm")]
		public virtual ActionResult OrderSaveForm()
		{
			return PartialView(new CrmModel());
		}

		[RenderAction("OrderSummaryGeneral", Priority = 10)]
		public virtual ActionResult OrderSummaryGeneral()
		{
			return PartialView(new CrmModel());
		}
	}
}
