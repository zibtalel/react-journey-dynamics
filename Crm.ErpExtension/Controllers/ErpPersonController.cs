namespace Crm.ErpExtension.Controllers
{
	using System.Text;
	using Crm.Library.Helper;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	[Authorize]
	public class ErpPersonController : Controller
	{
		private readonly IAppSettingsProvider appSettingsProvider;
		public ErpPersonController(IAppSettingsProvider appSettingsProvider)
		{
			this.appSettingsProvider = appSettingsProvider;
		}

		public virtual ActionResult ObjectLibraryLink(string inforId)
		{
			var erpSystemID = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemID);
			var contents = new StringBuilder();
			contents.AppendLine("[ILM]");
			contents.AppendLine(string.Format("SystemID={0}", erpSystemID));
			contents.AppendLine("AppID=10100002");
			contents.AppendLine(string.Format("FilterCond=([PersonNr]='{0}')", inforId));
			contents.AppendLine("FilterView=relPerson");

			return File(
				Encoding.Default.GetBytes(contents.ToString()),
				"application/infor",
				string.Format("{0}.iol", inforId));
		}
	}
}
