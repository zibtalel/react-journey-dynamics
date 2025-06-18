using Microsoft.AspNetCore.Mvc;

namespace Crm.PerDiem.Controllers
{
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	[Authorize]
	public class ExpenseController : Controller
	{
		[RequiredPermission(PermissionName.Edit, Group = PerDiemPlugin.PermissionGroup.Expense)]
		public virtual ActionResult EditTemplate()
		{
			return PartialView();
		}

		[RenderAction("ExpenseTemplateActions", Priority = 100)]
		public virtual ActionResult TemplateActionEdit()
		{
			return PartialView();
		}

		[RenderAction("ExpenseTemplateActions", Priority = 50)]
		public virtual ActionResult TemplateActionDelete()
		{
			return PartialView();
		}

		[RenderAction("ExpenseTemplateTableColumns")]
		public virtual ActionResult TemplateTableColumns()
		{
			return PartialView();
		}
	}
}
