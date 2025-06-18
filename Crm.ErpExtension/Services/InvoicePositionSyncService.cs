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

	public class InvoicePositionSyncService : DefaultSyncService<InvoicePosition, Guid>
	{
		public InvoicePositionSyncService(IRepositoryWithTypedId<InvoicePosition, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
		}
		public override Type[] SyncDependencies => new[] { typeof(Invoice) };
		public override IQueryable<InvoicePosition> GetAll(User user)
		{
			return repository.GetAll();
		}
		public override IQueryable<InvoicePosition> Eager(IQueryable<InvoicePosition> entities)
		{
			return entities.Fetch(x => x.Parent);
		}
	}
}
