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

	public class SalesOrderPositionSyncService : DefaultSyncService<SalesOrderPosition, Guid>
	{
		public SalesOrderPositionSyncService(IRepositoryWithTypedId<SalesOrderPosition, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
		}
		public override Type[] SyncDependencies => new[] { typeof(SalesOrder) };
		public override IQueryable<SalesOrderPosition> GetAll(User user)
		{
			return repository.GetAll();
		}
		public override IQueryable<SalesOrderPosition> Eager(IQueryable<SalesOrderPosition> entities)
		{
			return entities.Fetch(x => x.Parent);
		}
	}
}
