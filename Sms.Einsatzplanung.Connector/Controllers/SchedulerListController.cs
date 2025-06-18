namespace Sms.Einsatzplanung.Connector.Controllers
{
	using System.Collections.Generic;

	using Crm.Controllers;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.EntityConfiguration;
	using Crm.Library.EntityConfiguration.Interfaces;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Library.Rest;
	using Crm.Library.Services.Interfaces;

	using Microsoft.AspNetCore.Mvc;

	using Sms.Einsatzplanung.Connector.Model;

	public class SchedulerListController : GenericListController<Scheduler>
	{
		public SchedulerListController(IPluginProvider pluginProvider, IEnumerable<IRssFeedProvider<Scheduler>> rssFeedProviders, IEnumerable<ICsvDefinition<Scheduler>> csvDefinitions, IEntityConfigurationProvider<Scheduler> entityConfigurationProvider, IRepository<Scheduler> repository, IAppSettingsProvider appSettingsProvider, IResourceManager resourceManager, RestTypeProvider restTypeProvider)
			: base(pluginProvider, rssFeedProviders, csvDefinitions, entityConfigurationProvider, repository, appSettingsProvider, resourceManager, restTypeProvider)
		{
		}
		[RequiredPermission(nameof(Scheduler), Group = PermissionGroup.WebApi)]
		public override ActionResult IndexTemplate()
		{
			return base.IndexTemplate();
		}

		[RequiredPermission(nameof(Scheduler), Group = PermissionGroup.WebApi)]
		public override ActionResult MaterialItemTemplate()
		{
			return base.MaterialItemTemplate();
		}
	}
}
