namespace Crm.ErpExtension.Helpers
{
	using System;
	using System.Text;

	using Crm.ErpExtension.Model;

	using Microsoft.AspNetCore.Mvc;

	public static class SapIntegrationHelper
	{
		public static FileContentResult OpenSalesDocument(SapDocumentType type, string erpId, string erpSystemName, string erpSystemId)
		{
			var stringbuilder = new StringBuilder();
			stringbuilder.AppendLine("[System]");
			stringbuilder.AppendLine(String.Format("Name={0}", erpSystemName));
			stringbuilder.AppendLine(String.Format("Client={0}", erpSystemId));
			stringbuilder.AppendLine("\n[User]");
			stringbuilder.AppendLine("Name=");
			stringbuilder.AppendLine("Language=DE");
			stringbuilder.AppendLine("\n[Function]");
			switch (type)
			{
				case SapDocumentType.Quote:
					stringbuilder.AppendLine("Title=Angebot anzeigen VA23");
					stringbuilder.AppendLine(String.Format("Command= \"*VA23 VBAK-VBELN={0}\"", erpId));
					break;
				case SapDocumentType.Invoice:
					stringbuilder.AppendLine("Title=Faktura anzeigen VF03");
					stringbuilder.AppendLine(String.Format("Command= \"*VF03 VBRK-VBELN={0}\"", erpId));
					break;
				case SapDocumentType.DeliveryNote:
					stringbuilder.AppendLine("Title=Lieferung anzeigen VL03N");
					stringbuilder.AppendLine(String.Format("Command= \"*VL03N LIKP-VBELN={0}\"", erpId));
					break;
				case SapDocumentType.SalesOrder:
					stringbuilder.AppendLine("Title=Auftrag anzeigen VA03");
					stringbuilder.AppendLine(String.Format("Command= \"*VA03 VBAK-VBELN={0}\"", erpId));
					break;
			}

			return new FileContentResult(Encoding.Default.GetBytes(stringbuilder.ToString()), "application/sap")
			{
				FileDownloadName = String.Format("{0}.sap", erpId)
			};
		}
	}
}