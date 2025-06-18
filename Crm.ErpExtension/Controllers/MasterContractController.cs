using Microsoft.AspNetCore.Mvc;

namespace Crm.ErpExtension.Controllers
{
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class MasterContractController : Controller
	{
		public virtual ActionResult DetailsTemplate()
		{
			return PartialView();
		}
		
		[RenderAction("MasterContractDetailsMaterialTabHeader", Priority = 100)]
		public virtual ActionResult MaterialDetailsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("MasterContractDetailsMaterialTab", Priority = 100)]
		public virtual ActionResult MaterialDetailsTab()
		{
			return PartialView();
		}
	}
}
