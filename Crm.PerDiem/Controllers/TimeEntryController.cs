namespace Crm.PerDiem.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class TimeEntryController : Controller
	{
		[RequiredPermission(PermissionName.View, Group = PerDiemPlugin.PermissionGroup.TimeEntry)]
		public virtual ActionResult IndexTemplate()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryIndexTopMenu")]
		public virtual ActionResult IndexTopMenuClose()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryIndexSidebar")]
		public virtual ActionResult Sidebar()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryIndexPrimaryAction", Priority = 90)]
		public virtual ActionResult PrimaryActionAddExpense()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryIndexPrimaryAction", Priority = 100)]
		public virtual ActionResult PrimaryActionAddTimeEntry()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryTemplateActions", Priority = 100)]
		public virtual ActionResult TemplateActionEdit()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryTemplateActions", Priority = 50)]
		public virtual ActionResult TemplateActionDelete()
		{
			return PartialView();
		}

		[RequiredPermission(PermissionName.Edit, Group = PerDiemPlugin.PermissionGroup.TimeEntry)]
		public virtual ActionResult EditTemplate()
		{
			return PartialView();
		}

		[RequiredPermission(PermissionName.Edit, Group = PerDiemPlugin.PermissionGroup.TimeEntry)]
		public virtual ActionResult Close()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryReportCloseModal", Priority = 200)]
		public virtual ActionResult CloseModalCustom()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryReportCloseModal", Priority = 200)]
		public virtual ActionResult CloseModalMonthly()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryReportCloseModal", Priority = 200)]
		public virtual ActionResult CloseModalWeekly()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryReportCloseModal", Priority = 100)]
		public virtual ActionResult CloseModalEntries()
		{
			return PartialView();
		}

		[RenderAction("TimeEntryDayTab", Priority = 90)]
		public virtual ActionResult DayTabExpenses()
		{
			return PartialView("TimeEntryDayTabExpenses");
		}
		[RenderAction("TimeEntryDayTab", Priority = 100)]
		public virtual ActionResult DayTabTimeEntries()
		{
			return PartialView("TimeEntryDayTabTimeEntries");
		}
		[RenderAction("AccountInfoTab", Priority = 30)]
		[RenderAction("UserDetailsTabExtensions", Priority = 30)]
		public virtual ActionResult UserDetailsTabExtension() => PartialView();
	}
}
