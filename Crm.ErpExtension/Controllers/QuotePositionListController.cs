namespace Crm.ErpExtension.Controllers
{
	using System.Collections.Generic;
	using System.Linq;

	using Crm.Controllers;
	using Crm.ErpExtension.Model;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.EntityConfiguration;
	using Crm.Library.EntityConfiguration.Interfaces;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Helper;
	using Crm.Library.Modularization.Interfaces;
	using Crm.Library.Rest;
	using Crm.Library.Services.Interfaces;

	public class QuotePositionListController : GenericListController<QuotePosition>
	{
		public QuotePositionListController(IPluginProvider pluginProvider, IEnumerable<IRssFeedProvider<QuotePosition>> rssFeedProviders, IEnumerable<ICsvDefinition<QuotePosition>> csvDefinitions, IEntityConfigurationProvider<QuotePosition> entityConfigurationProvider, IRepository<QuotePosition> repository, IAppSettingsProvider appSettingsProvider, IResourceManager resourceManager, RestTypeProvider restTypeProvider)
			: base(pluginProvider, rssFeedProviders, csvDefinitions, entityConfigurationProvider, repository, appSettingsProvider, resourceManager, restTypeProvider)
		{
		}
		protected override string GetTitle()
		{
			return "Quotations";
		}

		protected override string GetEmptySlate()
		{
			return repository.GetAll().Any() ? resourceManager.GetTranslation("NoErpDocumentsMatch") : resourceManager.GetTranslation("NoErpDocumentsAvailable");
		}
	}
}
