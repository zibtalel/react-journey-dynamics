namespace Crm.ErpExtension.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;

	public class ErpDocumentHeadRest : RestEntityWithExtensionValues
	{
		public string LegacyId { get; set; }
		public string DocumentType { get; set; }
		public string StatusKey { get; set; }
		public decimal? Total { get; set; }
		public decimal? TotalWoTaxes { get; set; }
		public decimal? VATLevel { get; set; }
		public decimal? DiscountPercentage { get; set; }
		public string CurrencyKey { get; set; }
		public string Description { get; set; }
		public string CompanyNo { get; set; }
		public string PaymentTerms { get; set; }
		public string PaymentMethod { get; set; }
		public string TermsOfDelivery { get; set; }
		public string DeliveryMethod { get; set; }
		public Guid? ContactKey { get; set; }
		[RestrictedField, NotReceived] public string ContactType { get; set; }
		public string OrderNo { get; set; }
		public string OrderType { get; set; }
		public DateTime? OrderDate { get; set; }
		public string Commission { get; set; }
		[RestrictedField, NotReceived] public string CompanyName { get; set; }
	}
	public class ErpDocumentHeadRest<TPositionRest> : ErpDocumentHeadRest
		where TPositionRest : ErpDocumentPositionRest
	{
		[ExplicitExpand, NotReceived] public TPositionRest[] Positions { get; set; }
	}
}
