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

	public class CreditNotePositionSyncService : DefaultSyncService<CreditNotePosition, Guid>
	{
		public CreditNotePositionSyncService(IRepositoryWithTypedId<CreditNotePosition, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
		}
		public override Type[] SyncDependencies => new[] { typeof(CreditNote) };
		public override IQueryable<CreditNotePosition> GetAll(User user)
		{
			return repository.GetAll();
		}
		public override IQueryable<CreditNotePosition> Eager(IQueryable<CreditNotePosition> entities)
		{
			return entities.Fetch(x => x.Parent);
		}
	}
}
