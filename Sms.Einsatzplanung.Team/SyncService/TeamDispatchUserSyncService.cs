using System;
using System.Linq;

namespace Sms.Einsatzplanung.Team.SyncService
{
	using System.Collections.Generic;

	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.Service.Model;

	using NHibernate.Linq;

	using Sms.Einsatzplanung.Team.Model;

	public class TeamDispatchUserSyncService : DefaultSyncService<TeamDispatchUser, Guid>
	{
		private readonly ISyncService<ServiceOrderHead> serviceOrderHeadSyncService;
		public TeamDispatchUserSyncService(IRepositoryWithTypedId<TeamDispatchUser, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, ISyncService<ServiceOrderHead> serviceOrderHeadSyncService)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.serviceOrderHeadSyncService = serviceOrderHeadSyncService;
		}
		public override TeamDispatchUser Save(TeamDispatchUser entity)
		{
			throw new NotImplementedException();
		}
		public override IQueryable<TeamDispatchUser> GetAll(User user, IDictionary<string, int?> groups)
		{
			var serviceOrders = serviceOrderHeadSyncService.GetAll(user, groups);
			return repository.GetAll()
				.Where(x => serviceOrders.Any(y => y.Id == x.Dispatch.OrderId));
		}
		public override IQueryable<TeamDispatchUser> Eager(IQueryable<TeamDispatchUser> entities)
		{
			return entities
				.Fetch(x => x.Dispatch)
				.ThenFetch(x => x.OrderHead)
				.Fetch(x => x.User);
		}
		public override void Remove(TeamDispatchUser entity)
		{
			throw new NotImplementedException();
		}
	}
}
