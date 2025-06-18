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
	using Crm.Services;
	using NHibernate.Linq;

	public class CreditNoteSyncService : DefaultSyncService<CreditNote, Guid>
	{
		private readonly ISyncService<Company> companySyncService;
		public CreditNoteSyncService(IRepositoryWithTypedId<CreditNote, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, ISyncService<Company> companySyncService)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.companySyncService = companySyncService;
		}
		public override IQueryable<CreditNote> GetAll(User user, IDictionary<string, int?> groups)
		{
			var companies = companySyncService.GetAll(user, groups);
			return repository.GetAll()
				.Where(x => x.ContactKey == null || companies.Any(y => y.Id == x.ContactKey));
		}
		public override IQueryable<CreditNote> Eager(IQueryable<CreditNote> entities)
		{
			return entities.Fetch(x => x.Contact);
		}
	}
}
