namespace Crm.ErpExtension.Rest.Model
{
	using System;

	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(Quote))]
	public class QuoteRest : ErpDocumentHeadRest<QuotePositionRest>
	{
		public string QuoteNo { get; set; }
		public DateTime? QuoteDate { get; set; }
		public DateTime? DocumentDate11 { get; set; }
		public DateTime? DueDate { get; set; }
	}
}
