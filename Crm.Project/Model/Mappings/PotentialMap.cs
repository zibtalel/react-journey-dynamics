namespace Crm.Project.Model.Mappings
{
	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class PotentialMap : SubclassMapping<Potential>
	{
		public PotentialMap()
		{
			DiscriminatorValue("Potential");

			Join(
				"Potential",
				join =>
				{
					join.Schema("CRM");
					join.Key(x => x.Column("ContactKey"));

					join.Property(x=>x.PotentialNo);
					join.Property(x => x.StatusKey);
					join.Property(x => x.CloseDate);
					join.Property(x => x.StatusDate);
					join.Property(x => x.PriorityKey);
					join.Property(x => x.MasterProductFamilyKey);
					join.Property(x => x.ProductFamilyKey);
					join.EntityBag(
						x => x.ContactRelationships,
						m =>
						{
							m.Key(
								k =>
								{
									k.Column("PotentialKey");
									k.NotNullable(true);
								});
							m.Inverse(true);
							m.Lazy(CollectionLazy.Lazy);
							m.Cascade(Cascade.Persist.Include(Cascade.DeleteOrphans));
						},
						action => action.OneToMany());
					join.ManyToOne(x => x.ProductFamily, m =>
					{
						m.Column("ProductFamilyKey");
						m.Fetch(FetchKind.Select);
						m.Insert(false);
						m.Update(false);
					});
					join.ManyToOne(x => x.MasterProductFamily, m =>
					{
						m.Column("MasterProductFamilyKey");
						m.Fetch(FetchKind.Select);
						m.Insert(false);
						m.Update(false);
					});
				});
			this.EntitySet(
				x => x.Tasks,
				map =>
				{
					map.Schema("CRM");
					map.Table("Task");
					map.Key(km => km.Column("ContactKey"));
					map.Lazy(CollectionLazy.Lazy);
					map.Inverse(true);
					map.Fetch(CollectionFetchMode.Select);
					map.Cascade(Cascade.None);
				},
				action => action.OneToMany());
		}
	}
}