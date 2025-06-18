namespace Sms.Einsatzplanung.Connector.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Modularization;
	using Crm.ViewModels;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Sms.Einsatzplanung.Connector.Model;

	[Authorize]
	public class SchedulerMaterialController : Controller
	{
		[RequiredPermission(nameof(Scheduler), Group = PermissionGroup.WebApi)]
		public virtual ActionResult Admin() => View(new CrmModel());

		[RenderAction("SchedulerManagementTabHeader", Priority = 100)]
		public virtual ActionResult SchedulerManagementTabHeader() => PartialView();
		[RenderAction("SchedulerManagementTab", Priority = 100)]
		public virtual ActionResult SchedulerManagementTab() => PartialView();
		[RenderAction("SchedulerManagementTabHeader", Priority = 95)]
		public virtual ActionResult AvailableBinariesTabHeader() => PartialView();
		[RenderAction("SchedulerManagementTab", Priority = 95)]
		public virtual ActionResult AvailableBinariesTab() => PartialView();
		[RenderAction("AvailableBinariesTabPrimaryAction", Priority = 200)]
		public virtual ActionResult MaterialPrimaryActionAddBinary() => PartialView();
		[RenderAction("AvailableBinariesTabPrimaryAction", Priority = 100)]
		[RenderAction("SchedulerManagementTabPrimaryAction", Priority = 100)]
		public virtual ActionResult MaterialPrimaryActionAddConfig() => PartialView();

		[RequiredPermission(nameof(Scheduler), Group = PermissionGroup.WebApi)]
		public virtual ActionResult AddBinaryEditor() => PartialView();
		[RequiredPermission(nameof(Scheduler), Group = PermissionGroup.WebApi)]
		public virtual ActionResult SettingsEditor() => PartialView();
	}
}