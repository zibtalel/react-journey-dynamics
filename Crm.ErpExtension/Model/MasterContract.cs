namespace Crm.ErpExtension.Model
{
	using System;

	public class MasterContract : ErpDocumentHead<MasterContractPosition>
	{
		public virtual decimal? QuantityShipped { get; set; }
		public virtual decimal? RemainingQuantity { get; set; }
		public virtual DateTime? OrderConfirmationDate { get; set; }
		public virtual DateTime? DueDate { get; set; }
		public virtual string ItemNo { get; set; }
		public virtual decimal? Quantity { get; set; }
	}
}