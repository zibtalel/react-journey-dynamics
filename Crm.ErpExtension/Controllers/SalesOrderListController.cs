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

	public class SalesOrderListController : GenericListController<SalesOrder>
	{
		public SalesOrderListController(IPluginProvider pluginProvider, IEnumerable<IRssFeedProvider<SalesOrder>> rssFeedProviders, IEnumerable<ICsvDefinition<SalesOrder>> csvDefinitions, IEntityConfigurationProvider<SalesOrder> entityConfigurationProvider, IRepository<SalesOrder> repository, IAppSettingsProvider appSettingsProvider, IResourceManager resourceManager, RestTypeProvider restTypeProvider)
			: base(pluginProvider, rssFeedProviders, csvDefinitions, entityConfigurationProvider, repository, appSettingsProvider, resourceManager, restTypeProvider)
		{
		}
		protected override string GetTitle()
		{
			return "SalesOrders";
		}

		protected override string GetEmptySlate()
		{
			return repository.GetAll().Any() ? resourceManager.GetTranslation("NoErpDocumentsMatch") : resourceManager.GetTranslation("NoErpDocumentsAvailable");
		}
	}
}
