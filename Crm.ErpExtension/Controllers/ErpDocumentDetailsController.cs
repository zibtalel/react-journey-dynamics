using Microsoft.AspNetCore.Mvc;

namespace Crm.ErpExtension.Controllers
{
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;

	[Authorize]
	public class ErpDocumentDetailsController : Controller
	{
		[RenderAction("ErpDocumentMaterialDetailsTabGeneralInfoExtension", Priority = 50)]
		public virtual ActionResult ErpDocumentMaterialDetailsTabGeneralInfo()
		{
			return PartialView();
		}
		
		[RenderAction("ErpDocumentDetailsMaterialTabHeader", Priority = 50)]
		public virtual ActionResult MaterialOrderTabHeader()
		{
			return PartialView();
		}

		[RenderAction("ErpDocumentDetailsMaterialTab", Priority = 50)]
		public virtual ActionResult MaterialOrderTab()
		{
			return PartialView();
		}
		
		[RenderAction("ErpDocumentDetailsMaterialTabHeader", Priority = 60)]
		public virtual ActionResult MaterialPositionsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("ErpDocumentDetailsMaterialTab", Priority = 60)]
		public virtual ActionResult MaterialPositionsTab()
		{
			return PartialView();
		}
	}
}
