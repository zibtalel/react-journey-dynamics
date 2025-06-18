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

	public class MasterContractSyncService : DefaultSyncService<MasterContract, Guid>
	{
		private readonly ISyncService<Company> companySyncService;
		public MasterContractSyncService(IRepositoryWithTypedId<MasterContract, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, ISyncService<Company> companySyncService)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.companySyncService = companySyncService;
		}
		public override IQueryable<MasterContract> GetAll(User user, IDictionary<string, int?> groups)
		{
			var companies = companySyncService.GetAll(user, groups);
			return repository.GetAll()
				.Where(x => x.ContactKey == null || companies.Any(y => y.Id == x.ContactKey));
		}
		public override IQueryable<MasterContract> Eager(IQueryable<MasterContract> entities)
		{
			return entities.Fetch(x => x.Contact);
		}
	}
}
