namespace Crm.Project.Model.Configuration
{
	using Crm.Article.Model;
	using Crm.Library.EntityConfiguration;
	using Crm.Model;

	public class PotentialConfiguration : EntityConfiguration<Potential>
	{
		public PotentialConfiguration(IEntityConfigurationHolder<Potential> entityConfigurationHolder)
			: base(entityConfigurationHolder)
		{
		}
		public override void Initialize()
		{
			// Sortable properties
			Property(x => x.Name, c => c.Sortable());
			Property(x => x.StatusKey, c => c.Sortable());
			Property(x => x.PriorityKey, c => c.Sortable(f => f.SortCaption("Priority")));
			Property(x => x.CreateDate, c => c.Sortable());

			// Filterable properties
			Property(x => x.LegacyName, c => c.Filterable(f => f.Caption("Name")));
			Property(x => x.Status, c => { c.Filterable(); });
			Property(x => x.Priority, c => { c.Filterable(); });
			Property(x => x.PotentialNo, c => c.Filterable());
			Property(x => x.ParentId, m => m.Filterable(f => f.Definition(new AutoCompleterFilterDefinition<Company>("CompanyAutocomplete", new { Plugin = "Main" }, "Main_Company", x => x.Name, x => x.Id, x => x.LegacyId, x => x.Name))));

			NestedProperty(x => x.Parent.StandardAddress.ZipCode, c => c.Filterable());
			NestedProperty(x => x.Parent.StandardAddress.City, c => c.Filterable());
			NestedProperty(x => x.Parent.StandardAddress.Latitude, c => c.Filterable(f => f.Definition(new GeoCoordinateFilterDefinition())));
			NestedProperty(x => x.Parent.StandardAddress.Longitude, c => c.Filterable(f => f.Definition(new GeoCoordinateFilterDefinition())));

			Property(x => x.ResponsibleUser, c => c.Filterable(f => f.Definition(new UserFilterDefinition { WithGroups = true })));

			// Filterable and Sortable properties
			Property(
				x => x.StatusDate,
				c =>
				{
					c.Filterable(f => f.Definition(new DateFilterDefinition { AllowPastDates = true, AllowFutureDates = false }));
					c.Sortable();
				});
			Property(
				x => x.CreateDate,
				c =>
				{
					c.Filterable(f => f.Definition(new DateFilterDefinition { AllowPastDates = true, AllowFutureDates = false }));
					c.Sortable();
				});
			Property(
				x => x.StatusDate,
				c =>
				{
					c.Filterable(f => f.Definition(new DateFilterDefinition { AllowPastDates = true, AllowFutureDates = false }));
					c.Sortable();
				});
			Property(x => x.ProductFamilyKey, m => m.Filterable(f => f.Definition(new AutoCompleterFilterDefinition<ProductFamily>("ProductFamilyAutocomplete", new { Plugin = "Crm.Article" }, "CrmArticle_ProductFamily", x => x.Name, x => x.Id, x => x.LegacyId, x => x.Name) { Caption = "ProductFamily" })));

		}
	}

	public static class PotentialExtension
	{
		public static string GetSummary(this Potential x)
		{
			return string.Format("{0}: {1} ({2})", x.ParentName, x.Name, x.ResponsibleUser);
		}
	}
}