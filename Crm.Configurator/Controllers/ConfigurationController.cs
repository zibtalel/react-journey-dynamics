namespace Crm.Configurator.Controllers
{
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ConfigurationController : Controller
	{
		[RenderAction("MaterialHeadResource", Priority = 990)]
		public virtual ActionResult HeadResource()
		{
			return Content(Url.JsResource("Crm.Configurator", "configuratorJs") + Url.JsResource("Crm.Configurator", "configuratorMaterialTs"));
		}

		[RequiredPermission(PermissionName.Index, Group = ConfiguratorPlugin.PermissionGroup.Configurator)]
		public virtual ActionResult IndexTemplate()
		{
			return PartialView("Index");
		}

		[RenderAction("OfferDetailsTabHeader", "OrderDetailsTemplateTabHeader")]
		public virtual ActionResult ConfigurationDetailsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("OfferDetailsTab", "OrderDetailsTemplateTab")]
		public virtual ActionResult ConfigurationDetailsTab()
		{
			return PartialView();
		}

		[RenderAction("OfferSummaryBottom", Priority = -100)]
		public virtual ActionResult OfferSummaryOptionalItems()
		{
			return PartialView();
		}

		[RenderAction("OfferSummaryGeneral", Priority = -100)]
		public virtual ActionResult OfferSummaryOptions()
		{
			return PartialView();
		}

		[RenderAction("OrderSummaryGeneral", Priority = -100)]
		public virtual ActionResult OrderSummaryOptions()
		{
			return PartialView();
		}

		[RenderAction("OfferSummaryGeneral", "OrderSummaryGeneral", Priority = 0)]
		public virtual ActionResult SummaryConfigurationBase()
		{
			return PartialView();
		}

		[RenderAction("VariableValueSelectionItem")]
		public virtual ActionResult VariableValueSelectionItemAttributes()
		{
			return PartialView();
		}

		[RenderAction("VariableValueSelectionItemAction")]
		public virtual ActionResult VariableValueSelectionItemAction()
		{
			return PartialView();
		}
		public virtual ActionResult EditTemplate()
		{
			return PartialView();
		}
	}
}
