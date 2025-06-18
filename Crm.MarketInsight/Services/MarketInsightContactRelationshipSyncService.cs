
namespace Crm.MarketInsight.Services
{
	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.MarketInsight.Model;
	using Crm.MarketInsight.Model.Relationships;
	using Crm.Model;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class MarketInsightContactRelationshipSyncService : DefaultSyncService<MarketInsightContactRelationship, Guid>
	{
		private readonly ISyncService<MarketInsight> marketInsightSyncService;
		public MarketInsightContactRelationshipSyncService(IRepositoryWithTypedId<MarketInsightContactRelationship, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, ISyncService<MarketInsight> marketInsightSyncService)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.marketInsightSyncService = marketInsightSyncService;
		}
		public override Type[] SyncDependencies => new[] { typeof(Company), typeof(Person), typeof(MarketInsight) };
		public override MarketInsightContactRelationship Save(MarketInsightContactRelationship entity)
		{
			return repository.SaveOrUpdate(entity);
		}
		public override IQueryable<MarketInsightContactRelationship> GetAll(User user, IDictionary<string, int?> groups)
		{
			var marketInsight = marketInsightSyncService.GetAll(user, groups);
			return repository.GetAll()
				.Where(x => marketInsight.Any(y => y.Id == x.ParentId) && (x.Child.ContactType == "Company" || x.Child.ContactType == "Person") && x.Parent.ContactType == "MarketInsight");
		}
	}
}
