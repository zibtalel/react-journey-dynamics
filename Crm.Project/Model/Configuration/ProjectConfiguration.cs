namespace Crm.Project.Model.Configuration
{
	using System;

	using Crm.Article.Model;
	using Crm.Library.EntityConfiguration;
	using Crm.Library.Helper;
	using Crm.Model;

	public class ProjectConfiguration : EntityConfiguration<Project>
	{
		private readonly IAppSettingsProvider appSettingsProvider;
		public ProjectConfiguration(IEntityConfigurationHolder<Project> entityConfigurationHolder, IAppSettingsProvider appSettingsProvider)
			: base(entityConfigurationHolder)
		{
			this.appSettingsProvider = appSettingsProvider;
		}
		public override void Initialize()
		{
			// Sortable properties
			Property(x => x.Name, c => c.Sortable());
			Property(x => x.WeightedValue, c => c.Sortable());
			Property(x => x.ProjectNo, c => c.Sortable());

			// Filterable properties
			Property(x => x.LegacyName, c => c.Filterable(f => f.Caption("Name")));
			Property(x => x.ProjectNo, c => c.Filterable());
			Property(x => x.ParentId, m => m.Filterable(f => f.Definition(new AutoCompleterFilterDefinition<Company>("CompanyAutocomplete", new { Plugin = "Main" }, "Main_Company", "Helper.Company.getDisplayName", x => x.Id, x => x.LegacyId, x => x.Name) { ShowOnMaterialTab = false })));
			Property(x => x.ProductFamilyKey, m => m.Filterable(f => f.Definition(new AutoCompleterFilterDefinition<ProductFamily>("ProductFamilyAutocomplete", new { Plugin = "Crm.Article" }, "CrmArticle_ProductFamily", x => x.Name, x => x.Id, x => x.LegacyId, x => x.Name) { Caption = "ProductFamily" })));

			if (appSettingsProvider.GetValue(ProjectPlugin.Settings.ProjectsHaveAddresses))
			{
				NestedProperty(x => x.ProjectAddress.ZipCode, c => c.Filterable());
				NestedProperty(x => x.ProjectAddress.City, c => c.Filterable());
				NestedProperty(x => x.ProjectAddress.Latitude, c => c.Filterable(f => f.Definition(new GeoCoordinateFilterDefinition())));
				NestedProperty(x => x.ProjectAddress.Longitude, c => c.Filterable(f => f.Definition(new GeoCoordinateFilterDefinition())));
			}
			else
			{
				NestedProperty(x => x.Parent.StandardAddress.ZipCode, c => c.Filterable());
				NestedProperty(x => x.Parent.StandardAddress.City, c => c.Filterable());
				NestedProperty(x => x.Parent.StandardAddress.Latitude, c => c.Filterable(f => f.Definition(new GeoCoordinateFilterDefinition())));
				NestedProperty(x => x.Parent.StandardAddress.Longitude, c => c.Filterable(f => f.Definition(new GeoCoordinateFilterDefinition())));
			}
			Property(x => x.ResponsibleUser, c => c.Filterable(f => f.Definition(new UserFilterDefinition { WithGroups = true })));

			// Filterable and Sortable properties
			Property(x => x.Category, c => { c.Filterable(); });
			Property(x => x.CategoryKey, c => { c.Sortable(); });
			Property(x => x.DueDate, c =>
			{
				c.Filterable(f => f.Definition(new DateFilterDefinition { AllowPastDates = true, AllowFutureDates = true }));
				c.Sortable();
			});
			Property(x => x.Status, c => { c.Filterable(); });
			Property(x => x.StatusKey, c => { c.Sortable(); });
			Property(x => x.StatusDate, c =>
			{
				c.Filterable(f => f.Definition(new DateFilterDefinition { AllowPastDates = true, AllowFutureDates = false }));
				c.Sortable();
			});
			Property(x => x.CreateDate, c =>
			{
				c.Filterable(f => f.Definition(new DateFilterDefinition { AllowPastDates = true, AllowFutureDates = false }));
				c.Sortable();
			});
			Property(x => x.Rating, c =>
			{
				c.Filterable(f => f.Definition(new ScaleFilterDefinition(1, 4, 1, Operator.GreaterThan)));
				c.Sortable();
			});
			Property(x => x.Value, c =>
			{
				c.Filterable(f => f.Definition(new ScaleFilterDefinition(10000, 100000, 10000, Operator.GreaterThan)));
				c.Sortable();
			});
			Property(x => x.ProductFamilyKey, m => m.Filterable(f => f.Definition(new AutoCompleterFilterDefinition<ProductFamily>("ProductFamilyAutocomplete", new { Plugin = "Crm.Article" }, "CrmArticle_ProductFamily", x => x.Name, x => x.Id, x => x.LegacyId, x => x.Name) { Caption = "ProductFamily" })));
		}
	}

	public static class ProjectExtension
	{
		public static string GetIcsSummary(this Project x)
		{
			return String.Format("{0}: {1} ({2})", x.ParentName, x.Name, x.ResponsibleUser);
		}
	}
}
