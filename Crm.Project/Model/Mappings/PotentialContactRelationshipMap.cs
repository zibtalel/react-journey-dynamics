using System;

namespace Crm.Project.Model.Mappings
{
	using Crm.Library.BaseModel.Mappings;
	using Crm.Project.Model.Relationships;

	using NHibernate.Mapping.ByCode;

	public class PotentialContactRelationshipMap : EntityClassMapping<PotentialContactRelationship>
	{
		public PotentialContactRelationshipMap()
		{
			Schema("CRM");
			Table("PotentialContactRelationship");
			Id(x => x.Id, m =>
			{
				m.Column("PotentialContactRelationshipId");
				m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				m.UnsavedValue(Guid.Empty);
			});

			Property(x => x.RelationshipTypeKey, m => m.Column("RelationshipType"));

			Property(x => x.ParentId, m => m.Column("PotentialKey"));
			ManyToOne(x => x.Parent, m =>
			{
				m.Column("PotentialKey");
				m.Insert(false);
				m.Update(false);
				m.Lazy(LazyRelation.Proxy);
			});
			Property(x => x.ChildId, m => m.Column("ContactKey"));
			ManyToOne(x => x.Child, m =>
			{
				m.Column("ContactKey");
				m.Insert(false);
				m.Update(false);
				m.Lazy(LazyRelation.Proxy);
			});
			Property(x => x.Information);
			ManyToOne(x => x.ChildPerson, m =>
			{
				m.Column("ContactKey");
				m.Insert(false);
				m.Update(false);
				m.Lazy(LazyRelation.Proxy);
				m.NotFound(NotFoundMode.Ignore);
			});
			ManyToOne(x => x.ChildCompany, m =>
			{
				m.Column("ContactKey");
				m.Insert(false);
				m.Update(false);
				m.Lazy(LazyRelation.Proxy);
				m.NotFound(NotFoundMode.Ignore);
			});
		}
	}
}