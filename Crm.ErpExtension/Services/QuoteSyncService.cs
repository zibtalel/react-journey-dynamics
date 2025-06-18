namespace Crm.ErpExtension.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using AutoMapper;

	using Crm.ErpExtension.Model;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.Model;
	using NHibernate.Linq;

	public class QuoteSyncService : DefaultSyncService<Quote, Guid>
	{
		private readonly ISyncService<Company> companySyncService;
		public QuoteSyncService(IRepositoryWithTypedId<Quote, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, ISyncService<Company> companySyncService)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.companySyncService = companySyncService;
		}
		public override IQueryable<Quote> GetAll(User user, IDictionary<string, int?> groups)
		{
			var companies = companySyncService.GetAll(user, groups);
			return repository.GetAll()
				.Where(x => x.ContactKey == null || companies.Any(y => y.Id == x.ContactKey));
		}
		public override IQueryable<Quote> Eager(IQueryable<Quote> entities)
		{
			return entities.Fetch(x => x.Contact);
		}
	}
}
