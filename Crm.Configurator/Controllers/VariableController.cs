namespace Crm.Configurator.Controllers
{
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class VariableController : Controller
	{
		[RenderAction("MaterialArticleEditorBody")]
		public virtual ActionResult MaterialArticleEditorBody()
		{
			return PartialView();
		}
	}
}
