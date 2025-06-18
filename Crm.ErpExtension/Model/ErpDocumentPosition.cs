namespace Crm.ErpExtension.Model
{
	using System;

	using Crm.Article.Model;

	public abstract class ErpDocumentPosition : ErpDocument
	{
		public virtual string ItemNo { get; set; }
		public virtual decimal? Quantity { get; set; }
		public virtual decimal? PricePerUnit { get; set; }
		public virtual Guid? ArticleKey { get; set; }
		public virtual Article Article { get; set; }
		public virtual string QuantityUnit { get; set; }
		public virtual decimal? RemainingQuantity { get; set; }
		public virtual string PositionNo { get; set; }
		public virtual Guid? ParentKey { get; set; }
	}
	public abstract class ErpDocumentPosition<THead> : ErpDocumentPosition
		where THead : ErpDocumentHead
	{
		public virtual THead Parent { get; set; }
	}
}