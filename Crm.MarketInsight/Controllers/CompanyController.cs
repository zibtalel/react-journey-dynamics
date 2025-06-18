namespace Crm.MarketInsight.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class CompanyController : Controller
	{
		[RequiredPermission(MarketInsightPlugin.PermissionName.MarketInsightTab, Group = MainPlugin.PermissionGroup.Company)]
		[RenderAction("CompanyDetailsMaterialTabHeader", Priority = 10)]
		public virtual ActionResult MarketInsightsDetailsTabHeader()
		{
			return PartialView();
		}

		[RequiredPermission(MarketInsightPlugin.PermissionName.MarketInsightTab, Group = MainPlugin.PermissionGroup.Company)]
		[RenderAction("CompanyDetailsMaterialTab", Priority = 10)]
		public virtual ActionResult MarketInsightsDetailsTab()
		{
			return PartialView();
		}

	}
}
