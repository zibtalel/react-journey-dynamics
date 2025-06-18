namespace Crm.ErpExtension.Model.Maps
{
	using Crm.Library.BaseModel.Interfaces;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class ErpDocumentPositionMap : SubclassMapping<ErpDocumentPosition>, IDatabaseMapping
	{
		public ErpDocumentPositionMap()
		{
			Property(x => x.ItemNo);
			Property(x => x.Quantity);
			Property(x => x.PricePerUnit);
			Property(x => x.RemainingQuantity);
			Property(x => x.QuantityUnit);
			Property(x => x.PositionNo);
			Property(x => x.ParentKey);
			Property(x => x.ArticleKey);
			ManyToOne(x => x.Article, map =>
			{
				map.Column("ArticleKey");
				map.Fetch(FetchKind.Select);
				map.Lazy(LazyRelation.Proxy);
				map.Cascade(Cascade.None);
				map.Insert(false);
				map.Update(false);
			});
		}
	}
}