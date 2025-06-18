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

	public class DeliveryNoteListController : GenericListController<DeliveryNote>
	{
		public DeliveryNoteListController(IPluginProvider pluginProvider, IEnumerable<IRssFeedProvider<DeliveryNote>> rssFeedProviders, IEnumerable<ICsvDefinition<DeliveryNote>> csvDefinitions, IEntityConfigurationProvider<DeliveryNote> entityConfigurationProvider, IRepository<DeliveryNote> repository, IAppSettingsProvider appSettingsProvider, IResourceManager resourceManager, RestTypeProvider restTypeProvider)
			: base(pluginProvider, rssFeedProviders, csvDefinitions, entityConfigurationProvider, repository, appSettingsProvider, resourceManager, restTypeProvider)
		{
		}
		protected override string GetTitle()
		{
			return "DeliveryNotes";
		}

		protected override string GetEmptySlate()
		{
			return repository.GetAll().Any() ? resourceManager.GetTranslation("NoErpDocumentsMatch") : resourceManager.GetTranslation("NoErpDocumentsAvailable");
		}
	}
}
