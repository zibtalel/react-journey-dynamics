namespace Crm.ProjectOrders.Controllers
{
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Crm.Order;
	using Crm.Project;
	using Crm.ViewModels;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ProjectDetailsController : Controller
	{
		private readonly IAppSettingsProvider appSettingsProvider;
		public ProjectDetailsController(IAppSettingsProvider appSettingsProvider)
		{
			this.appSettingsProvider = appSettingsProvider;
		}
		[RenderAction("ProjectDetailsMaterialTabHeader", Priority = 70)]
		[RequiredPermission(PermissionName.Read, Group = OrderPlugin.PermissionGroup.Offer)]
		[RequiredPermission(OrderPlugin.PermissionName.OfferTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialOffersTabHeader()
		{
			if (!appSettingsProvider.GetValue(OrderPlugin.Settings.System.Offers.Enabled))
			{
				return new EmptyResult();
			}

			return PartialView(new CrmModel());
		}

		[RenderAction("ProjectDetailsMaterialTab", Priority = 70)]
		[RequiredPermission(PermissionName.Read, Group = OrderPlugin.PermissionGroup.Offer)]
		[RequiredPermission(OrderPlugin.PermissionName.OfferTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialOffersTab()
		{
			if (!appSettingsProvider.GetValue(OrderPlugin.Settings.System.Offers.Enabled))
			{
				return new EmptyResult();
			}

			return PartialView(new CrmModel());
		}

		[RenderAction("ProjectDetailsMaterialTabHeader", Priority = 60)]
		[RequiredPermission(PermissionName.Read, Group = OrderPlugin.PermissionGroup.Order)]
		[RequiredPermission(OrderPlugin.PermissionName.OrderTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialOrdersTabHeader()
		{
			return PartialView(new CrmModel());
		}

		[RenderAction("ProjectDetailsMaterialTab", Priority = 60)]
		[RequiredPermission(PermissionName.Read, Group = OrderPlugin.PermissionGroup.Order)]
		[RequiredPermission(OrderPlugin.PermissionName.OrderTab, Group = ProjectPlugin.PermissionGroup.Project)]
		public virtual ActionResult MaterialOrdersTab()
		{
			return PartialView(new CrmModel());
		}
	}
}
