namespace Crm.MarketInsight.Model.Mappings
{
	using Crm.Library.BaseModel.Mappings;
	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;


	public class MarketInsightMap : SubclassMapping<MarketInsight>
	{
		public MarketInsightMap()
		{
			DiscriminatorValue("MarketInsight");
			Join(
				"MarketInsight",
				join =>
				{
					join.Schema("CRM");
					join.Key(x => x.Column("ContactKey"));
					join.Property(x => x.ProductFamilyKey);
					join.Property(x => x.ReferenceKey);
					join.Property(x => x.StatusKey);
					join.ManyToOne(
						x => x.ProductFamily,
						map =>
						{
							map.Column("ProductFamilyKey");
							map.Fetch(FetchKind.Select);
							map.Lazy(LazyRelation.NoLazy);
							map.Insert(false);
							map.Update(false);
						});
					join.EntityBag(
						x => x.ContactRelationships,
						m =>
						{
							m.Key(
								k =>
								{
									k.Column("MarketInsightKey");
									k.NotNullable(true);
								});
							m.Inverse(true);
							m.Lazy(CollectionLazy.Lazy);
							m.Cascade(Cascade.Persist.Include(Cascade.DeleteOrphans));
						},
						action => action.OneToMany());
				});
		}
	}
}