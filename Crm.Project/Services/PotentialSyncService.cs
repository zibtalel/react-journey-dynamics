using System;
using System.Linq;

namespace Crm.Project.Services
{
	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Model.Authorization.Interfaces;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.Model;
	using Crm.Project.Model;

	using NHibernate.Linq;

	public class PotentialSyncService : DefaultSyncService<Potential, Guid>
	{
		private readonly IVisibilityProvider visibilityProvider;
		public PotentialSyncService(IRepositoryWithTypedId<Potential, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, IAuthorizationManager authorizationManager, IVisibilityProvider visibilityProvider)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.visibilityProvider = visibilityProvider;
		}

		public override Type[] SyncDependencies
		{
			get { return new[] { typeof(Company) }; }
		}

		public override IQueryable<Potential> GetAll(User user)
		{
			return visibilityProvider.FilterByVisibility(repository.GetAll());
		}
		public override IQueryable<Potential> Eager(IQueryable<Potential> entities)
		{
			entities = entities
				.Fetch(x => x.Parent);
			return entities;
		}
	}
}