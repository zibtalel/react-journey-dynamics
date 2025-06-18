namespace Crm.MarketInsight.Controllers
{
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class MarketInsightDetailsController : Controller
	{
		public virtual ActionResult FilterTemplate()
		{
			return PartialView();
		}
		[RenderAction("MarketInsightDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialDetailsTab()
		{
			return PartialView();
		}
		[RenderAction("MarketInsightDetailsMaterialTabHeader", Priority = 60)]
		public virtual ActionResult MaterialDetailsTabHeader()
		{
			return PartialView();
		}
		[RenderAction("MarketInsightDetailsMaterialTabHeader", Priority = 60)]
		public virtual ActionResult MaterialProjectsTabHeader()
		{
			return PartialView();
		}
		[RenderAction("MarketInsightDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialProjectsTab()
		{
			return PartialView();
		}
		[RenderAction("MarketInsightDetailsMaterialTabHeader", Priority = 60)]
		public virtual ActionResult MaterialPotentialsTabHeader()
		{
			return PartialView();
		}
		[RenderAction("MarketInsightDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialPotentialsTab()
		{
			return PartialView();
		}
	}
}
