namespace Crm.Configurator.Services
{
	using System;
	using System.Linq;

	using AutoMapper;

	using Crm.Configurator.Model;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;

	public class ConfigurationBaseSyncService : DefaultSyncService<ConfigurationBase, Guid>
	{
		private readonly IVisibilityProvider visibilityProvider;
		public ConfigurationBaseSyncService(
			IRepositoryWithTypedId<ConfigurationBase, Guid> repository,
			RestTypeProvider restTypeProvider,
			IRestSerializer restSerializer,
			IMapper mapper,
			IVisibilityProvider visibilityProvider)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.visibilityProvider = visibilityProvider;
		}
		public override IQueryable<ConfigurationBase> GetAll(User user)
		{
			return visibilityProvider.FilterByVisibility(repository.GetAll());
		}
	}
}