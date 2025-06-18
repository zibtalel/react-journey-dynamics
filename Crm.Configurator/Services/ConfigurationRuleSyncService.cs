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

	public class ConfigurationRuleSyncService : DefaultSyncService<ConfigurationRule, Guid>
	{
		private readonly ISyncService<ConfigurationBase> configurationBaseSyncService;
		public ConfigurationRuleSyncService(IRepositoryWithTypedId<ConfigurationRule, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, ISyncService<ConfigurationBase> configurationBaseSyncService)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.configurationBaseSyncService = configurationBaseSyncService;
		}

		public override IQueryable<ConfigurationRule> GetAll(User user, IDictionary<string, int?> groups)
		{
			var configurationBases = configurationBaseSyncService.GetAll(user, groups);
			return repository.GetAll()
				.Where(x => configurationBases.Any(y => y.Id == x.ConfigurationBaseId));
		}
		public override IQueryable<ConfigurationRule> Eager(IQueryable<ConfigurationRule> entities)
		{
			return entities;
		}
	}
}
