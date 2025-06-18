namespace Sms.Einsatzplanung.Connector.SyncService
{
	using System;
	using AutoMapper;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.Services;

	public class SchedulerSyncService : DefaultSyncService<Scheduler, Guid>
	{
		private readonly ISchedulerService schedulerService;
		public SchedulerSyncService(IRepositoryWithTypedId<Scheduler, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, ISchedulerService schedulerService)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.schedulerService = schedulerService;
		}
		public override void Remove(Scheduler entity)
		{
			schedulerService.DeletePackage(entity.Id);
		}
		public override Scheduler Save(Scheduler entity)
		{
			return repository.SaveOrUpdate(entity);
		}
	}
}