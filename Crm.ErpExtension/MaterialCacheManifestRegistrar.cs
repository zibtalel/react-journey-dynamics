namespace Crm.ErpExtension
{

	using Crm.Library.Modularization.Interfaces;
	using Crm.Library.Offline;
	using Crm.Library.Offline.Interfaces;

	public class MaterialCacheManifestRegistrar : CacheManifestRegistrar<MaterialCacheManifest>
	{
		public MaterialCacheManifestRegistrar(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
		}
		protected override void Initialize()
		{
			CacheJs("erpExtensionMaterialJs");
			Cache("CreditNoteList", "FilterTemplate");
			Cache("CreditNoteList", "IndexTemplate");
			Cache("CreditNote", "DetailsTemplate");
			Cache("DeliveryNoteList", "FilterTemplate");
			Cache("DeliveryNoteList", "IndexTemplate");
			Cache("DeliveryNote", "DetailsTemplate");
			Cache("InvoiceList", "FilterTemplate");
			Cache("InvoiceList", "IndexTemplate");
			Cache("Invoice", "DetailsTemplate");
			Cache("MasterContractList", "FilterTemplate");
			Cache("MasterContractList", "IndexTemplate");
			Cache("MasterContract", "DetailsTemplate");
			Cache("QuoteList", "FilterTemplate");
			Cache("QuoteList", "IndexTemplate");
			Cache("Quote", "DetailsTemplate");
			Cache("SalesOrderList", "FilterTemplate");
			Cache("SalesOrderList", "IndexTemplate");
			Cache("SalesOrder", "DetailsTemplate");
		}
	}
}