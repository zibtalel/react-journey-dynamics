using System;

namespace Sms.Einsatzplanung.Connector.SyncService
{
	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Rest;
	using Crm.Library.Services;

	using Sms.Einsatzplanung.Connector.Model;

	public class SchedulerIconSyncService : DefaultSyncService<SchedulerIcon, Guid>
	{
		public SchedulerIconSyncService(IRepositoryWithTypedId<SchedulerIcon, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
		}
	}
}
