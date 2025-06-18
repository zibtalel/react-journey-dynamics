namespace Crm.Project.Services
{
	using System;
	using System.Linq;

	using Crm.Controllers;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Model;
	using Crm.Project.Model;

	public class PotentialsRelatedToCompany : IRelatedContact<Company>
	{
		private readonly IRepositoryWithTypedId<Potential, Guid> potentialRepository;
		public PotentialsRelatedToCompany(IRepositoryWithTypedId<Potential, Guid> potentialRepository)
		{
			this.potentialRepository = potentialRepository;
		}
		public virtual IQueryable<Contact> RelatedContact(Contact contact)
		{
			var contacts = potentialRepository.GetAll().Where(x => x.ParentId == contact.Id).Select(y => y);

			return contacts;
		}
	}
}
