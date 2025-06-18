namespace Crm.Project.Controllers
{
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

	public class PotentialListController : GenericListController<Potential>
	{
		[RequiredPermission(PermissionName.Read, Group = ProjectPlugin.PermissionGroup.Potential)]
		[RenderAction("PotentialItemTemplateActions", Priority = 100)]
		public virtual ActionResult ActionDetails()
		{
			return PartialView();
		}
		protected override string GetTitle()
		{
			return "Potentials";
		}
		protected override string GetEmptySlate()
		{
			return resourceManager.GetTranslation("NoPotentialsMatchingSearchCriteria");
		}
		[RequiredPermission(PermissionName.Index, Group = ProjectPlugin.PermissionGroup.Potential)]
		public override ActionResult FilterTemplate() => base.FilterTemplate();

		[RequiredPermission(PermissionName.View, Group = ProjectPlugin.PermissionGroup.Potential)]
		public override ActionResult IndexTemplate()
		{
			return base.IndexTemplate();
		}
		[RequiredPermission(PermissionName.Create, Group = ProjectPlugin.PermissionGroup.Potential)]
		public override ActionResult MaterialPrimaryAction()
		{
			return PartialView();
		}
		public PotentialListController(IPluginProvider pluginProvider, IEnumerable<IRssFeedProvider<Potential>> rssFeedProviders, IEnumerable<ICsvDefinition<Potential>> csvDefinitions, IEntityConfigurationProvider<Potential> entityConfigurationProvider, IRepository<Potential> repository, IAppSettingsProvider appSettingsProvider, IResourceManager resourceManager, RestTypeProvider restTypeProvider)
			: base(pluginProvider,
				rssFeedProviders,
				csvDefinitions,
				entityConfigurationProvider,
				repository,
				appSettingsProvider,
				resourceManager,
				restTypeProvider)
		{
		}
	}

	public class PotentialCsvDefinition : CsvDefinition<Potential>
	{
		private readonly IUserService userService;
		private readonly ILookupManager lookupManager;
		public PotentialCsvDefinition(IUserService userService, IResourceManager resourceManager, ILookupManager lookupManager)
			: base(resourceManager)
		{
			this.userService = userService;
			this.lookupManager = lookupManager;
		}
		public override string GetCsv(IEnumerable<Potential> items) {
			var potentialStatuses = lookupManager.List<PotentialStatus>();
			var sourceTypes = lookupManager.List<SourceType>();
			var potentialPriority = lookupManager.List<PotentialPriority>();

			Property("Id", x => x.Id);
			Property("PotentialNo", x => x.PotentialNo);
			Property("Name", x => x.Name);
			Property("CompanyId", x => x.ParentId);
			Property("CompanyNo", x => (x.Parent as Company)?.CompanyNo);
			Property("Company", x => $"{x.ParentName} - {x.Parent?.StandardAddress}");
			Property("ResponsibleUser", x => x.ResponsibleUserObject != null ? x.ResponsibleUserObject.DisplayName : x.ResponsibleUser);
			Property("Status", x => x.StatusKey.IsNotNullOrEmpty() ? potentialStatuses.FirstOrDefault(c => c.Key == x.StatusKey)?.Value : string.Empty);
			Property("Rating", x => x.PriorityKey.IsNotNullOrEmpty() ? potentialPriority.FirstOrDefault(c => c.Key == x.PriorityKey)?.Value : string.Empty);
			Property("Source", x => x.SourceTypeKey.IsNotNullOrEmpty() ? sourceTypes.FirstOrDefault(c => c.Key == x.SourceTypeKey)?.Value : string.Empty);
			Property("SourceKey", x => x.CampaignSource);
			Property("BackgroundInfo", x => x.BackgroundInfo);
			Property("StatusDate", x => $"{x.StatusDate:d}");
			Property("CloseDate", x => $"{x.CloseDate:d}");
			Property("CreateDate", x => $"{x.CreateDate:d}");
			Property("ModifyDate", x => $"{x.ModifyDate:d}");
			Property(
				"CreateUser",
				x =>
				{
					var user = userService.GetUser(x.CreateUser);
					return user?.DisplayName;
				});
			Property(
				"ModifyUser",
				x =>
				{
					var user = userService.GetUser(x.ModifyUser);
					return user?.DisplayName;
				});

			//Internal Ids
			Property("StatusKey", x => x.StatusKey);
			Property("PriorityKey", x => x.PriorityKey);
			Property("SourceTypeKey", x => x.SourceTypeKey);

			return base.GetCsv(items);
		}
	}
}
