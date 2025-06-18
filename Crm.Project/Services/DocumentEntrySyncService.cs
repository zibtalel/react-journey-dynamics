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
	using Crm.Project.Extensions;
	using Crm.Project.Model;

	using NHibernate.Linq;

	public class DocumentEntrySyncService : DefaultSyncService<DocumentEntry, Guid> {
		
		private readonly IAuthorizationManager authorizationManager;
		public DocumentEntrySyncService(IRepositoryWithTypedId<DocumentEntry, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper, IAuthorizationManager authorizationManager)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.authorizationManager = authorizationManager;
		}
		public override DocumentEntry Save(DocumentEntry entity) {
			repository.SaveOrUpdate(entity);
			return entity;
		}
		public override IQueryable<DocumentEntry> GetAll(User user) {
			return repository
				.GetAll()
				.FilterByContactVisibility(authorizationManager, user);
		}
		public override IQueryable<DocumentEntry> Eager(IQueryable<DocumentEntry> entities)
		{
			return entities
				.Fetch(i => i.Person);
		}
	}
}