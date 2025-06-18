namespace Crm.ErpExtension.Controllers
{
	using System.Text;
	using Crm.Helpers;
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ErpCompanyController : Controller
	{
		private readonly IAppSettingsProvider appSettingsProvider;
		public ErpCompanyController(IAppSettingsProvider appSettingsProvider)
		{
			this.appSettingsProvider = appSettingsProvider;
		}

		[RenderAction("CompanyDetailsTopMenu", Priority = 10)]
		[RequiredPermission(ErpPlugin.PermissionName.TurnoverTab, Group = MainPlugin.PermissionGroup.Company)]
		public virtual ActionResult CompanyDetailsTopMenu()
		{
			return PartialView();
		}

		[RenderAction("CompanyDetailsMaterialTabHeader", Priority = 20)]
		[RequiredPermission(ErpPlugin.PermissionName.ErpDocumentsTab, Group = MainPlugin.PermissionGroup.Company)]
		public virtual ActionResult MaterialDocumentsTabHeader()
		{
			return PartialView();
		}

		[RenderAction("CompanyDetailsMaterialTab", Priority = 20)]
		[RequiredPermission(ErpPlugin.PermissionName.ErpDocumentsTab, Group = MainPlugin.PermissionGroup.Company)]
		public virtual ActionResult MaterialDocumentsTab()
		{
			return PartialView();
		}

		[RenderAction("CompanyDetailsMaterialTabHeader", Priority = 10)]
		[RequiredPermission(ErpPlugin.PermissionName.TurnoverTab, Group = MainPlugin.PermissionGroup.Company)]
		public virtual ActionResult MaterialTurnoverTabHeader()
		{
			return PartialView();
		}
		
		[RenderAction("CompanyDetailsMaterialTab", Priority = 10)]
		[RequiredPermission(ErpPlugin.PermissionName.TurnoverTab, Group = MainPlugin.PermissionGroup.Company)]
		public virtual ActionResult MaterialTurnoverTab()
		{
			return PartialView();
		}

		public virtual ActionResult ObjectLibraryLink(string inforId)
		{
			var contents = new StringBuilder();
			contents.AppendLine("[ILM]");
			contents.AppendLine($"SystemID={appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemID)}");
			contents.AppendLine("AppID=10100001");
			contents.AppendLine($"FilterCond=([FirmaNr]='{inforId}')");
			contents.AppendLine("FilterView=relFirma");

			return File(
				Encoding.Default.GetBytes(contents.ToString()),
				"application/infor",
				$"{inforId}.iol");
		}

		public virtual ActionResult ObjectD3Link(string inforId)
		{
			return D3IntegrationHelper.OpenD3Document(Response, "IKDSB", "1", inforId);
		}

		[RenderAction("CompanyMaterialDetailsTabExtensions")]
		public virtual ActionResult ErpBackgroundExtensions()
		{
			return PartialView();
		}
	}
}
