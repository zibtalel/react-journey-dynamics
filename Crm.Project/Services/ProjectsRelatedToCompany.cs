namespace Crm.Project.Services
{
	using System;
	using System.Linq;

	using Crm.Controllers;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Model;
	using Crm.Project.Model;

	public class ProjectsRelatedToCompany : IRelatedContact<Company>
	{
		private readonly IRepositoryWithTypedId<Project, Guid> projectRepository;
		public ProjectsRelatedToCompany(IRepositoryWithTypedId<Project, Guid> projectRepository)
		{
			this.projectRepository = projectRepository;
		}
		public virtual IQueryable<Contact> RelatedContact(Contact contact)
		{
			var contacts = projectRepository.GetAll().Where(x => x.ParentId == contact.Id).Select(y => y);

			return contacts;
		}
	}
}
