namespace Crm.ErpExtension.Rest.Model
{
	using System;

	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(Invoice))]
	public class InvoiceRest : ErpDocumentHeadRest<InvoicePositionRest>
	{
		public string InvoiceNo { get; set; }
		public DateTime? InvoiceDate { get; set; }
		public short? DunningLevel { get; set; }
		public DateTime? DueDate { get; set; }
		public decimal? OutstandingBalance { get; set; }
	}
}
