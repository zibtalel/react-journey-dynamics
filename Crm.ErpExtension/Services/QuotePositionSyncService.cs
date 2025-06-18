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

	public class QuotePositionSyncService : DefaultSyncService<QuotePosition, Guid>
	{
		public QuotePositionSyncService(IRepositoryWithTypedId<QuotePosition, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
		}
		public override Type[] SyncDependencies => new[] { typeof(Quote) };
		public override IQueryable<QuotePosition> GetAll(User user)
		{
			return repository.GetAll();
		}
		public override IQueryable<QuotePosition> Eager(IQueryable<QuotePosition> entities)
		{
			return entities.Fetch(x => x.Parent);
		}
	}
}
