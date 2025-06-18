namespace Crm.Project.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Crm.Events;
	using Crm.Library.BaseModel.Interfaces;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Modularization.Events;
	using Crm.Model;
	using Crm.Project.Model;
	using Crm.Project.Model.Relationships;
	using Crm.Project.Services.Interfaces;

	public class ProjectService : IProjectService, IEventHandler<CompanyDeletedEvent>, IMerger<Contact>
	{
		// Members
		private readonly IRepositoryWithTypedId<Project, Guid> projectRepository;
		private readonly IRepositoryWithTypedId<ProjectContactRelationship, Guid> projectContactRelationshipRepository;
		private readonly Func<ProjectContactRelationship> projectContactRelationshipFactory;
		
		public virtual IEnumerable<string> GetUsedProjectCategories()
		{
			return projectRepository.GetAll().Select(p => p.CategoryKey).Distinct();
		}

		public virtual IEnumerable<string> GetUsedProjectStatuses()
		{
			return projectRepository.GetAll().Select(p => p.StatusKey).Distinct();
		}

		public virtual IEnumerable<string> GetUsedLostReasonCategories()
		{
			return projectRepository.GetAll().Select(p => p.ProjectLostReasonCategoryKey).Distinct();
		}

		public virtual IEnumerable<string> GetUsedPaymentConditions()
		{
			return projectRepository.GetAll().Select(p => p.PaymentConditionKey).Distinct();
		}

		public virtual IEnumerable<string> GetUsedCurrencies()
		{
			return projectRepository.GetAll().Select(p => p.CurrencyKey).Distinct();
		}

		public virtual IEnumerable<string> GetUsedProjectContactRelationshipTypes()
		{
			return projectContactRelationshipRepository.GetAll().Select(p => p.RelationshipTypeKey).Distinct();
		}

		public virtual void Handle(CompanyDeletedEvent e)
		{
			// ToDo: Projects are not deleted because trigger T_CONTACT_PARENT sets ParentKey to NULL
			var projectsToDelete = from p in projectRepository.GetAll()
				where p.Parent != null
				      && e.CompanyIds.Contains(p.Parent.Id)
				select p;
			foreach (Project project in projectsToDelete)
			{
				projectRepository.Delete(project);
			}
		}

		public virtual void Merge(Contact loser, Contact winner)
		{
			var loserProjects = projectRepository.GetAll().Where(x => x.ParentId == loser.Id);
			foreach (Project loserProject in loserProjects)
			{
				loserProject.ParentId = winner.Id;
				projectRepository.SaveOrUpdate(loserProject);
			}
			var loserCompetitorProjects = projectRepository.GetAll().Where(x => x.CompetitorId == loser.Id);
			foreach (Project loserCompetitorProject in loserCompetitorProjects)
			{
				loserCompetitorProject.CompetitorId = winner.Id;
				projectRepository.SaveOrUpdate(loserCompetitorProject);
			}
			var loserProjectContactRelationships = projectContactRelationshipRepository.GetAll().Where(x => x.ChildId == loser.Id);
			foreach (ProjectContactRelationship loserProjectContactRelationship in loserProjectContactRelationships)
			{
				if (!projectContactRelationshipRepository.GetAll().Any(x => x.ParentId == loserProjectContactRelationship.ParentId && x.ChildId == winner.Id))
				{
					var relationship = projectContactRelationshipFactory();
					relationship.ParentId = loserProjectContactRelationship.ParentId;
					relationship.Parent = loserProjectContactRelationship.Parent;
					relationship.ChildId = winner.Id;
					relationship.Child = winner;
					relationship.RelationshipTypeKey = loserProjectContactRelationship.RelationshipTypeKey;
					relationship.Information = loserProjectContactRelationship.Information;
					projectContactRelationshipRepository.SaveOrUpdate(relationship);
				}
				projectContactRelationshipRepository.Delete(loserProjectContactRelationship);
			}
		}

		// Constructor
		public ProjectService(IRepositoryWithTypedId<Project, Guid> projectRepository, IRepositoryWithTypedId<ProjectContactRelationship, Guid> projectContactRelationshipRepository, Func<ProjectContactRelationship> projectContactRelationshipFactory)
		{
			this.projectRepository = projectRepository;
			this.projectContactRelationshipRepository = projectContactRelationshipRepository;
			this.projectContactRelationshipFactory = projectContactRelationshipFactory;
		}
	}
}
