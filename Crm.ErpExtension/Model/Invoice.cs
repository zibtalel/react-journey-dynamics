namespace Crm.ErpExtension.Model
{
	using System;

	public class Invoice : ErpDocumentHead<InvoicePosition>
	{
		public virtual string InvoiceNo { get; set; }
		public virtual DateTime? InvoiceDate { get; set; }
		public virtual short? DunningLevel { get; set; }
		public virtual DateTime? DueDate { get; set; }
		public virtual decimal? OutstandingBalance { get; set; }
	}
}