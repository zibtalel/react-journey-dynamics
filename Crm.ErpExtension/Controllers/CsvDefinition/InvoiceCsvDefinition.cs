namespace Crm.ErpExtension.Controllers.CsvDefinition
{
	using System.Collections.Generic;
	using System.Linq;

	using Crm.ErpExtension.Model;
	using Crm.ErpExtension.Model.Lookups;
	using Crm.Library.EntityConfiguration;
	using Crm.Library.Globalization.Lookup;
	using Crm.Library.Globalization.Resource;

	public class InvoiceCsvDefinition : CsvDefinition<Invoice>
	{
		private readonly ILookupManager lookupManager;
		public InvoiceCsvDefinition(ILookupManager lookupManager, IResourceManager resourceManager)
			: base(resourceManager)
		{
			this.lookupManager = lookupManager;
		}
		public override string GetCsv(IEnumerable<Invoice> items) {
			var erpDocumentStatuses = lookupManager.List<ErpDocumentStatus>();

			Property("Id", x => x.Id);
			Property("CompanyNo", x => x.CompanyNo);
			Property("CompanyName", x => x.CompanyName);
			Property("OrderNo", x => x.OrderNo);
			Property("InvoiceNo", x => x.InvoiceNo);
			Property("InvoiceDate", x => x.InvoiceDate);
			Property("Reference", x => x.Commission);
			Property("Total", x => x.Total);
			Property("Status", x => x.StatusKey != null ? erpDocumentStatuses.FirstOrDefault(d => d.Key == x.StatusKey)?.Value : string.Empty);

			//Internal ids
			Property("StatusKey", x => x.StatusKey);

			return base.GetCsv(items);
		}
	}
}
