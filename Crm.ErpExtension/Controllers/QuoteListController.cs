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

	public class QuoteListController : GenericListController<Quote>
	{
		public QuoteListController(IPluginProvider pluginProvider, IEnumerable<IRssFeedProvider<Quote>> rssFeedProviders, IEnumerable<ICsvDefinition<Quote>> csvDefinitions, IEntityConfigurationProvider<Quote> entityConfigurationProvider, IRepository<Quote> repository, IAppSettingsProvider appSettingsProvider, IResourceManager resourceManager, RestTypeProvider restTypeProvider)
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
