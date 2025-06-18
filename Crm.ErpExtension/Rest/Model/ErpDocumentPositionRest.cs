namespace Crm.ErpExtension.Rest.Model
{
	using System;

	using Crm.Article.Rest.Model;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;

	public class ErpDocumentPositionRest : RestEntityWithExtensionValues
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
		public string ItemNo { get; set; }
		public Guid? ParentKey { get; set; }
		public decimal? Quantity { get; set; }
		public decimal? PricePerUnit { get; set; }
		public Guid? ArticleKey { get; set; }
		public string QuantityUnit { get; set; }
		public decimal? RemainingQuantity { get; set; }
		public string PositionNo { get; set; }
		[NotReceived, ExplicitExpand] public ArticleRest Article { get; set; }
	}
	public class ErpDocumentPositionRest<THeadRest> : ErpDocumentPositionRest
	where THeadRest : ErpDocumentHeadRest
	{
		[NotReceived, ExplicitExpand] public THeadRest Parent { get; set; }
	}
}
