namespace Crm.ErpExtension.Services
{
	using System;
	using System.Linq;

	using AutoMapper;

	using Crm.ErpExtension.Model;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;

	using NHibernate.Linq;

	public class MasterContractPositionSyncService : DefaultSyncService<MasterContractPosition, Guid>
	{
		public MasterContractPositionSyncService(IRepositoryWithTypedId<MasterContractPosition, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
		}
		public override Type[] SyncDependencies => new[] { typeof(MasterContract) };
		public override IQueryable<MasterContractPosition> GetAll(User user)
		{
			return repository.GetAll();
		}
		public override IQueryable<MasterContractPosition> Eager(IQueryable<MasterContractPosition> entities)
		{
			return entities.Fetch(x => x.Parent);
		}
	}
}
