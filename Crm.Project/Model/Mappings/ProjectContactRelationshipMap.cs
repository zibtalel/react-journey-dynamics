namespace Crm.Project.Model.Mappings
{
	using System;

	using Crm.Library.BaseModel.Mappings;
	using Crm.Project.Model.Relationships;

	using NHibernate.Mapping.ByCode;

	public class ProjectContactRelationshipMap : EntityClassMapping<ProjectContactRelationship>
	{
		public ProjectContactRelationshipMap()
		{
			Schema("CRM");
			Table("ProjectContactRelationship");

			Id(x => x.Id, m =>
			{
				m.Column("ProjectContactRelationshipId");
				m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				m.UnsavedValue(Guid.Empty);
			});

			Property(x => x.RelationshipTypeKey, m => m.Column("RelationshipType"));

			Property(x => x.ParentId, m => m.Column("ProjectKey"));
			ManyToOne(x => x.Parent, m =>
			{
				m.Column("ProjectKey");
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