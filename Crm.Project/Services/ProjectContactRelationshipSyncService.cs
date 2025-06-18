using System;
using System.Linq;

namespace Crm.Project.Services
{
	using AutoMapper;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Model;
	using Crm.Library.Rest;
	using Crm.Library.Services;
	using Crm.Model;
	using Crm.Project.Model;
	using Crm.Project.Model.Relationships;
	public class ProjectContactRelationshipSyncService : DefaultSyncService<ProjectContactRelationship, Guid>
	{
		public ProjectContactRelationshipSyncService(IRepositoryWithTypedId<ProjectContactRelationship, Guid> repository, RestTypeProvider restTypeProvider, IRestSerializer restSerializer, IMapper mapper)
			: base(repository, restTypeProvider, restSerializer, mapper)
		{
		}
		public override Type[] SyncDependencies => new[] { typeof(Company), typeof(Person), typeof(Project) };
		public override ProjectContactRelationship Save(ProjectContactRelationship entity)
		{
			return repository.SaveOrUpdate(entity);
		}
		public override IQueryable<ProjectContactRelationship> GetAll(User user)
		{
			return repository.GetAll()
					.Where(x => x.Parent.IsActive && x.Child.IsActive && (x.Child.ContactType == "Company" || x.Child.ContactType == "Person") && x.Parent.ContactType == "Project");
		}
	}
}