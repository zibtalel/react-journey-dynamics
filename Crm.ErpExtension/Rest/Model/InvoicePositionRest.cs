namespace Crm.ErpExtension.Rest.Model
{
	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(InvoicePosition))]
	public class InvoicePositionRest : ErpDocumentPositionRest<InvoiceRest>
	{
	}
}
