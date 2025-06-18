namespace Crm.Configurator.Controllers
{
	using Crm.Article;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ArticleController : Controller
	{
		[RenderAction("ArticleDetailsMaterialTab", Priority = 80)]
		[RequiredPermission(PermissionName.View, Group = ArticlePlugin.PermissionGroup.Article)]
		public virtual ActionResult ConfigurationRulesTab()
		{
			return PartialView();
		}
		[RenderAction("ArticleDetailsMaterialTabHeader", Priority = 80)]
		[RequiredPermission(PermissionName.View, Group = ArticlePlugin.PermissionGroup.Article)]
		public virtual ActionResult ConfigurationRulesTabHeader()
		{
			return PartialView();
		}
	}
}
