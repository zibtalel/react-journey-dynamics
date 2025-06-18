namespace Crm.Project.Model.Mappings
{
	using Crm.Library.BaseModel.Interfaces;
	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class ProjectMap : SubclassMapping<Project>, IDatabaseMapping
	{
		public ProjectMap()
		{
			DiscriminatorValue("Project");

			ManyToOne(
				x => x.ProjectAddress,
				map =>
				{
					map.Formula("(SELECT TOP 1 a.AddressId FROM CRM.Address AS a WHERE a.CompanyKey = ContactId AND a.IsCompanyStandardAddress = 1)");
					map.Cascade(Cascade.All);
					map.Fetch(FetchKind.Select);
					map.Lazy(LazyRelation.Proxy);
					map.Insert(false);
					map.Update(false);
				});

			Join(
				"Project",
				join =>
				{
					join.Schema("CRM");
					join.Key(x => x.Column("ContactKey"));

					join.Property(x => x.ProjectNo);
					join.Property(x => x.PotentialId, m => m.Column("PotentialKey"));
					join.Property(x => x.CategoryKey);
					join.Property(x => x.StatusKey);
					join.Property(x => x.CurrencyKey);
					join.Property(x => x.DueDate);
					join.Property(x => x.Value);
					join.Property(x => x.Users);
					join.Property(x => x.StatusInfo);
					join.Property(x => x.AppMiteId);
					join.Property(x => x.BugtrackerId);
					join.Property(x => x.StatusDate);
					join.Property(x => x.ProjectLostReasonCategoryKey, m => m.Column("LostReasonCategoryKey"));
					join.Property(x => x.ProjectLostReason, m => m.Column("LostReasonText"));
					join.Property(x => x.Rating);
					join.Property(x => x.ContributionMargin);
					join.Property(x => x.CompetitorId);
					join.Property(x => x.MasterProductFamilyKey);
					join.Property(x => x.ProductFamilyKey);
					join.ManyToOne(
						x => x.Competitor,
						map =>
						{
							map.Column("CompetitorId");
							map.Fetch(FetchKind.Select);
							map.Lazy(LazyRelation.Proxy);
							map.Cascade(Cascade.None);
							map.Insert(false);
							map.Update(false);
						});

					join.Property(
						x => x.WeightedValue,
						m =>
						{
							m.Insert(false);
							m.Update(false);
						});
					join.Property(
						x => x.WeightedContributionMargin,
						m =>
						{
							m.Insert(false);
							m.Update(false);
						});
					join.Property(x => x.FolderId, m => m.Column("FolderKey"));
					join.ManyToOne(
						x => x.Folder,
						map =>
						{
							map.Column("FolderKey");
							map.Fetch(FetchKind.Select);
							map.Lazy(LazyRelation.Proxy);
							map.Cascade(Cascade.None);
							map.Insert(false);
							map.Update(false);
						});
					join.Property(x => x.PaymentConditionKey, m => m.Column("PaymentCondition"));
					join.EntityBag(
						x => x.ContactRelationships,
						m =>
						{
							m.Key(
								k =>
								{
									k.Column("ProjectKey");
									k.NotNullable(true);
								});
							m.Inverse(true);
							m.Lazy(CollectionLazy.Lazy);
							m.Cascade(Cascade.Persist.Include(Cascade.DeleteOrphans));
						},
						action => action.OneToMany());
					join.ManyToOne(
						x => x.Potential,
						m =>
						{
							m.Column("PotentialKey");
							m.Fetch(FetchKind.Select);
							m.Lazy(LazyRelation.Proxy);
							m.Cascade(Cascade.None);
							m.Insert(false);
							m.Update(false);
						});
					join.ManyToOne(
						x => x.ProductFamily,
						m =>
						{
							m.Column("ProductFamilyKey");
							m.Fetch(FetchKind.Select);
							m.Insert(false);
							m.Update(false);
						});
					join.ManyToOne(
						x => x.MasterProductFamily,
						m =>
						{
							m.Column("MasterProductFamilyKey");
							m.Fetch(FetchKind.Select);
							m.Insert(false);
							m.Update(false);
						});
				});
		}
	}
}