using System;

namespace Sms.Einsatzplanung.Connector.SyncService
{
	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Rest;
	using Crm.Library.Services;

	using Sms.Einsatzplanung.Connector.Model;

	public class SchedulerConfigSyncService : DefaultSyncService<SchedulerConfig, Guid>
	{
		public SchedulerConfigSyncService(IRepositoryWithTypedId<SchedulerConfig, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
		}
	}
}
