namespace Crm.ErpExtension.Model
{
	using System;

	public class CreditNote : ErpDocumentHead<CreditNotePosition>
	{
		public virtual string CreditNoteNo { get; set; }
		public virtual DateTime? CreditNoteDate { get; set; }
		public virtual string InvoiceNo { get; set; }
	}
}