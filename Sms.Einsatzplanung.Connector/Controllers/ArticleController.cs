namespace Sms.Einsatzplanung.Connector.Controllers;

using Crm.Library.Modularization;

using Microsoft.AspNetCore.Mvc;

public class ArticleController : Controller
{
	[RenderAction("ArticleMaterialDetailsTabExtensions")]
	public virtual ActionResult ArticleMaterialDetailsTabSchedulerExtensions() => PartialView();
}
