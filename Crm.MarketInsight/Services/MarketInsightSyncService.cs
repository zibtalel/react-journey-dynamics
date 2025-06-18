using System;
using System.Linq;

namespace Crm.MarketInsight.Services
{
	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.MarketInsight.Model;

	public class MarketInsightSyncService : DefaultSyncService<MarketInsight, Guid>
	{
		private readonly IVisibilityProvider visibilityProvider;
		public MarketInsightSyncService(IRepositoryWithTypedId<MarketInsight, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, IVisibilityProvider visibilityProvider)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.visibilityProvider = visibilityProvider;
		}
		public override IQueryable<MarketInsight> GetAll(User user)
		{
			return visibilityProvider.FilterByVisibility(repository.GetAll());
		}
	}
}