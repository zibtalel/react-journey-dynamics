namespace Crm.ErpExtension.Model
{
	using Crm.Library.BaseModel;
	using Crm.Model;

	public class CompanyExtension : EntityExtension<Company>
	{
		public string ErpPaymentTerms { get; set; }
		public string ErpPaymentMethod { get; set; }
		public string ErpPaymentTermsKey { get; set; }
		public string ErpTermsOfDelivery { get; set; }
		public string ErpDeliveryMethod { get; set; }
		public string ErpTermsOfDeliveryKey { get; set; }
		public string ErpCurrency { get; set; }
		public string VATIdentificationNumber { get; set; }
		public decimal? ErpCreditLimit { get; set; }
		public decimal? ErpOpenItemsTotal { get; set; }
		public decimal? ErpOpenItemsDue { get; set; }
		public decimal? ErpOutstandingOrderValue { get; set; }
		public decimal? ErpOutstandingInvoiceValue { get; set; }
		public decimal? ErpDiscount { get; set; }
		public bool? ErpPartialDeliveryProhibited { get; set; }
		public string ErpPartialDeliveryProhibitedReason { get; set; }
		public bool? ErpDeliveryProhibited { get; set; }
		public string ErpDeliveryProhibitedReason { get; set; }
		public int? ErpAccountGroup { get; set; }
	}
}
