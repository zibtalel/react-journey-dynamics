namespace Crm.ErpExtension.Rest.Model
{
	using System;

	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(CreditNote))]
	public class CreditNoteRest : ErpDocumentHeadRest<CreditNotePositionRest>
	{
		public string CreditNoteNo { get; set; }
		public string InvoiceNo { get; set; }
		public DateTime? CreditNoteDate { get; set; }
	}
}
