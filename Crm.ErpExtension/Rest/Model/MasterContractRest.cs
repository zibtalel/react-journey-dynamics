namespace Crm.ErpExtension.Rest.Model
{
	using System;

	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(MasterContract))]
	public class MasterContractRest : ErpDocumentHeadRest<MasterContractPositionRest>
	{
		public decimal? QuantityShipped { get; set; }
		public decimal? RemainingQuantity { get; set; }
		public DateTime? OrderConfirmationDate { get; set; }
		public DateTime? DueDate { get; set; }
		public string ItemNo { get; set; }
		public decimal? Quantity { get; set; }
	}
}
