namespace Crm.Configurator.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using AutoMapper;

	using Crm.Configurator.Model;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;

	public class VariableSyncService : DefaultSyncService<Variable, Guid>
	{
		private readonly IVisibilityProvider visibilityProvider;
		private readonly ISyncService<ConfigurationBase> configurationBaseSyncService;
		public VariableSyncService(
			IRepositoryWithTypedId<Variable, Guid> repository,
			RestTypeProvider restTypeProvider,
			IRestSerializer restSerializer,
			IMapper mapper,
			IVisibilityProvider visibilityProvider,
			ISyncService<ConfigurationBase> configurationBaseSyncService)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.visibilityProvider = visibilityProvider;
			this.configurationBaseSyncService = configurationBaseSyncService;
		}
		public override IQueryable<Variable> GetAll(User user, IDictionary<string, int?> groups)
		{
			var configurationBases = configurationBaseSyncService.GetAll(user, groups);
			var query = repository.GetAll()
				.Where(x => configurationBases.Any(y => y.Id == x.ParentId));
			return visibilityProvider.FilterByVisibility(query);
		}
	}
}
