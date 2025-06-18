namespace Crm.ErpExtension.Model
{
	using System;

	public class Quote : ErpDocumentHead<QuotePosition>
	{
		public virtual string QuoteNo { get; set; }
		public virtual DateTime? QuoteDate { get; set; }
		public virtual DateTime? DocumentDate11 { get; set; }
		public virtual DateTime? DueDate { get; set; }
	}
}