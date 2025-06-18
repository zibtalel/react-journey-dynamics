namespace Crm.MarketInsight.Controllers
{
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class MarketInsightController : Controller
	{
		[RenderAction("MaterialHeadResource", Priority = 1990)]
		[RequiredPermission(nameof(MarketInsight), Group = PermissionGroup.WebApi)]
		public virtual ActionResult HeadResource()
		{
			return Content(Url.JsResource("Crm.MarketInsight", "marketInsightTs"));
		}
		[RenderAction("MaterialTitleResource", Priority = 4880)]
		public virtual ActionResult MarketInsightStyle() => Content(Url.CssResource("Crm.MarketInsight", "marketInsightStyle"));

		[RequiredPermission(PermissionName.Edit, Group = MarketInsightPlugin.PermissionGroup.MarketInsight)]
		public virtual ActionResult EditTemplate()
		{
			return PartialView();
		}
		[RequiredPermission(PermissionName.Read, Group = MarketInsightPlugin.PermissionGroup.MarketInsight)]
		public virtual ActionResult DetailsTemplate()
		{
			return PartialView();
		}
	}
}
