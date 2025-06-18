namespace Crm.Project.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Crm.Controllers;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.EntityConfiguration;
	using Crm.Library.EntityConfiguration.Interfaces;
	using Crm.Library.Extensions;
	using Crm.Library.Globalization.Lookup;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.PermissionIntegration;
	using Crm.Library.Modularization;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Library.Rest;
	using Crm.Library.Services.Interfaces;
	using Crm.Model;
	using Crm.Model.Lookups;
	using Crm.Project.Model;
	using Crm.Project.Model.Lookups;

	using Microsoft.AspNetCore.Mvc;

	using NHibernate.Linq;

	public class ProjectListController : GenericListController<Project>
	{
		[RequiredPermission(PermissionName.Read, Group = ProjectPlugin.PermissionGroup.Project)]
		[RenderAction("ProjectItemTemplateActions", Priority = 100)]
		public virtual ActionResult ActionDetails()
		{
			return PartialView();
		}

		public ProjectListController(IPluginProvider pluginProvider, IEnumerable<IRssFeedProvider<Project>> rssFeedProviders, IEnumerable<ICsvDefinition<Project>> csvDefinitions, IEntityConfigurationProvider<Project> entityConfigurationProvider, IRepository<Project> repository, IAppSettingsProvider appSettingsProvider, IResourceManager resourceManager, RestTypeProvider restTypeProvider)
			: base(pluginProvider, rssFeedProviders, csvDefinitions, entityConfigurationProvider, repository, appSettingsProvider, resourceManager, restTypeProvider)
		{
		}

		[RequiredPermission(PermissionName.Index, Group = ProjectPlugin.PermissionGroup.Project)]
		public override ActionResult FilterTemplate() => base.FilterTemplate();

		[RequiredPermission(PermissionName.Index, Group = ProjectPlugin.PermissionGroup.Project)]
		public override ActionResult IndexTemplate()
		{
			return base.IndexTemplate();
		}

		protected override string GetTitle()
		{
			return "Projects";
		}

		protected override string GetEmptySlate()
		{
			return !repository.GetAll().Any()
				? resourceManager.GetTranslation("NoProjectsInfo")
				: resourceManager.GetTranslation("NoProjectsMatchingSearchCriteria");
		}

		public class ProjectCsvDefinition : CsvDefinition<Project>
		{
			private readonly IUserService userService;
			private readonly ILookupManager lookupManager;
			private readonly IAppSettingsProvider appSettingsProvider;
			public override IQueryable<Project> Eager(IQueryable<Project> query)
			{
				return query
					.Fetch(x => x.ProjectAddress)
					.Fetch(x => x.Parent)
					.ThenFetch(x => x.StandardAddress);
			}

			public ProjectCsvDefinition(IUserService userService, IResourceManager resourceManager, ILookupManager lookupManager, IAppSettingsProvider appSettingsProvider)
				: base(resourceManager)
			{
				this.userService = userService;
				this.lookupManager = lookupManager;
				this.appSettingsProvider = appSettingsProvider;
			}
			public override string GetCsv(IEnumerable<Project> items) {
				var projectCategories = lookupManager.List<ProjectCategory>();
				var projectStatuses = lookupManager.List<ProjectStatus>();
				var sourceTypes = lookupManager.List<SourceType>();
				var currencies = lookupManager.List<Currency>();
				var projectLostReasonCategories = lookupManager.List<ProjectLostReasonCategory>();
				var regions = lookupManager.List<Region>();
				var countries = lookupManager.List<Country>();
				var users = userService.GetUsers();

				Property("Id", x => x.Id);
				Property("ProjectNo", x => x.ProjectNo ?? string.Empty);
				Property("Name", x => x.Name);
				Property("CompanyNo", x => (x.Parent as Company)?.CompanyNo);
				Property("Company", x => $"{x.ParentName} - {x.Parent?.StandardAddress.ToString(string.Empty, false, regions.FirstOrDefault(c => c.Key == x.Parent.StandardAddressRegionKey) ?? Region.None, countries.FirstOrDefault(c => c.Key == x.Parent.StandardAddressCountryKey) ?? Country.None)}");
				if (appSettingsProvider.GetValue<bool>(ProjectPlugin.Settings.ProjectsHaveAddresses))
				{
					Property("ProjectAddress", x => x.ProjectAddress);
				}
				Property("CompanyId", x => x.ParentId);
				Property("ProductFamily", x => x.ProductFamily);
				Property("ResponsibleUser", x => x.ResponsibleUserObject != null ? x.ResponsibleUserObject.DisplayName : x.ResponsibleUser);
				Property("Category", x => x.CategoryKey.IsNotNullOrEmpty() ? projectCategories.FirstOrDefault(c => c.Key == x.CategoryKey)?.Value : string.Empty);
				Property("Status", x => x.StatusKey.IsNotNullOrEmpty() ? projectStatuses.FirstOrDefault(c => c.Key == x.StatusKey)?.Value : string.Empty);
				Property("DueDate", x => $"{x.DueDate:d}");
				Property("Rating", x => $"{(x.Rating.HasValue ? x.Rating * 20 : 0)}%");
				Property("Source", x => x.SourceTypeKey.IsNotNullOrEmpty() ? sourceTypes.FirstOrDefault(c => c.Key == x.SourceTypeKey)?.Value : string.Empty);
				Property("SourceKey", x => x.CampaignSource);
				Property("Value", x => $"{x.Value:N2}");
				Property("WeightedValue", x => $"{x.WeightedValue:N2}");
				Property("ContributionMargin", x => x.ContributionMargin.HasValue ? $"{x.ContributionMargin:N2}" : null);
				Property("WeightedContributionMargin", x => x.ContributionMargin.HasValue ? $"{x.WeightedContributionMargin:N2}" : null);
				Property("Currency", x => x.CurrencyKey.IsNotNullOrEmpty() ? currencies.FirstOrDefault(c => c.Key == x.CurrencyKey)?.Value : string.Empty);
				Property("BackgroundInfo", x => x.BackgroundInfo);
				Property("StatusDate", x => $"{x.StatusDate:d}");
				Property("ProjectLostReasonCategory", x => x.ProjectLostReasonCategoryKey.IsNotNullOrEmpty() ? projectLostReasonCategories.FirstOrDefault(c => c.Key == x.ProjectLostReasonCategoryKey)?.Value : string.Empty);
				Property("ProjectLostReason", x => x.ProjectLostReason);
				Property("CreateDate", x => $"{x.CreateDate:d}");
				Property("ModifyDate", x => $"{x.ModifyDate:d}");
				Property(
					"CreateUser",
					x =>
					{
						var user = users.FirstOrDefault(u => u.Id == x.CreateUser);
						return user?.DisplayName;
					});
				Property(
					"ModifyUser",
					x =>
					{
						var user = users.FirstOrDefault(u => u.Id == x.ModifyUser);
						return user?.DisplayName;
					});

				//Internal Ids
				Property("CategoryKey", x => x.CategoryKey);
				Property("StatusKey", x=> x.StatusKey);
				Property("SourceTypeKey",x => x.SourceTypeKey);
				Property("CurrencyKey", x => x.CurrencyKey);
				Property("ProjectLostReasonCategoryKey", x => x.ProjectLostReasonCategoryKey);

				return base.GetCsv(items);
			}
		}

		[RequiredPermission(PermissionName.Create, Group = ProjectPlugin.PermissionGroup.Project)]
		public override ActionResult MaterialPrimaryAction()
		{
			return PartialView();
		}
	}
}
