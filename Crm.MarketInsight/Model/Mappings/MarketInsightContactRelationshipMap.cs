using System;

namespace Crm.MarketInsight.Model.Mappings
{
	using Crm.Library.BaseModel.Mappings;
	using Crm.MarketInsight.Model.Relationships;

	using NHibernate.Mapping.ByCode;

	public class MarketInsightContactRelationshipMap : EntityClassMapping<MarketInsightContactRelationship>
	{
		public MarketInsightContactRelationshipMap()
		{
			Schema("CRM");
			Table("MarketInsightContactRelationship");
			Id(
				x => x.Id,
				m =>
				{
					m.Column("MarketInsightContactRelationshipId");
					m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
					m.UnsavedValue(Guid.Empty);
				});

			Property(x => x.RelationshipTypeKey, m => m.Column("RelationshipType"));

			Property(x => x.ParentId, m => m.Column("MarketInsightKey"));
			ManyToOne(
				x => x.Parent,
				m =>
				{
					m.Column("MarketInsightKey");
					m.Insert(false);
					m.Update(false);
					m.Lazy(LazyRelation.Proxy);
				});
			Property(x => x.ChildId, m => m.Column("ContactKey"));
			ManyToOne(
				x => x.Child,
				m =>
				{
					m.Column("ContactKey");
					m.Insert(false);
					m.Update(false);
					m.Lazy(LazyRelation.Proxy);
				});
			Property(x => x.Information);
		}
	}
}
