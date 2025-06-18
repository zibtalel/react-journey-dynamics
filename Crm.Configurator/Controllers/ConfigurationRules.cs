namespace Crm.Configurator.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ConfigurationRulesController : Controller
	{
		public virtual ActionResult EditTemplate() => PartialView();
	}
}
