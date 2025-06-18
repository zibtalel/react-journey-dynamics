namespace Crm.Project.Services
{
	using System;
	using System.Linq;

	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Library.Services.Interfaces;
	using Crm.Model;
	using Crm.Project.Model;

	using NHibernate.Linq;

	public class ProjectSyncService : DefaultSyncService<Project, Guid>
	{
		private readonly IVisibilityProvider visibilityProvider;
		public ProjectSyncService(
			IRepositoryWithTypedId<Project, Guid> repository,
			RestTypeProvider restTypeProvider,
			IRestSerializer restSerializer,
			IMapper mapper,
			IVisibilityProvider visibilityProvider)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
			this.visibilityProvider = visibilityProvider;
		}

		public override Type[] SyncDependencies => new[] { typeof(Company) };

		public override IQueryable<Project> GetAll(User user)
		{
			return visibilityProvider.FilterByVisibility(repository.GetAll());
		}

		public override IQueryable<Project> Eager(IQueryable<Project> entities)
		{
			entities = entities
				.Fetch(x => x.Folder)
				.Fetch(x => x.Parent);
			return entities;
		}
	}
}