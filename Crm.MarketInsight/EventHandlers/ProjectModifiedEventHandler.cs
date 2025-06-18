using System;
using System.Linq;

namespace Crm.MarketInsight.EventHandlers
{
	using Crm.Article.Services.Interfaces;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Modularization.Events;
	using Crm.MarketInsight.Model;
	using Crm.MarketInsight.Model.Lookups;
	using Crm.Project.Model;
	using Crm.Project.Model.Lookups;

	public class ProjectModifiedEventHandler : IEventHandler<EntityModifiedEvent<Project>>
	{
		private readonly IRepositoryWithTypedId<MarketInsight, Guid> marketInsightRepository;
		private readonly IRepositoryWithTypedId<Project, Guid> projectRepository;
		private readonly IRepositoryWithTypedId<Potential, Guid> potentialRepository;
		private readonly IProductFamilyService productFamilyService;
		public ProjectModifiedEventHandler(IRepositoryWithTypedId<MarketInsight, Guid> marketInsightRepository, IRepositoryWithTypedId<Project, Guid> projectRepository, IRepositoryWithTypedId<Potential, Guid> potentialRepository, IProductFamilyService productFamilyService)
		{
			this.marketInsightRepository = marketInsightRepository;
			this.projectRepository = projectRepository;
			this.potentialRepository = potentialRepository;
			this.productFamilyService = productFamilyService;
		}
		public virtual void Handle(EntityModifiedEvent<Project> e)
		{
			if (e.Entity.ProductFamilyKey == null)
			{
				return;
			}
			var firstProductFamily = productFamilyService.GetFirstProductFamilyInHierarchy((Guid)e.Entity.ProductFamilyKey);
			var marketInsight = marketInsightRepository.GetAll().FirstOrDefault(x => x.ProductFamilyKey == firstProductFamily.Id && x.ParentId == e.Entity.ParentId);
			if (e.EntityBeforeChange.MasterProductFamilyKey != e.Entity.MasterProductFamilyKey)
			{
				var oldMarketInsight = marketInsightRepository.GetAll().FirstOrDefault(x => x.ProductFamilyKey == e.EntityBeforeChange.MasterProductFamilyKey && x.ParentId == e.Entity.ParentId);
				var hasPotentials = potentialRepository.GetAll().Where(x => x.ParentId == e.Entity.ParentId && x.MasterProductFamilyKey == e.EntityBeforeChange.MasterProductFamilyKey).Count();
				var hasProjects = projectRepository.GetAll().Where(x => x.ParentId == e.Entity.ParentId && x.MasterProductFamilyKey == e.EntityBeforeChange.MasterProductFamilyKey).Count();

				if ((hasPotentials == 0 && hasProjects == 0) && (oldMarketInsight != null && oldMarketInsight.StatusKey != MarketInsightStatus.UnqualifiedKey))
				{
					marketInsightRepository.Delete(oldMarketInsight);
				}
			}
			else if (marketInsight == null)
			{
				marketInsight = new MarketInsight
				{
					ParentId = e.Entity.ParentId,
					ProductFamilyKey = firstProductFamily.Id,
					Name = firstProductFamily.Name,
					StatusKey = e.Entity.StatusKey == ProjectStatus.WonKey ? MarketInsightStatus.CustomerKey : MarketInsightStatus.SalesKey
				};
				marketInsightRepository.SaveOrUpdate(marketInsight);
			}
			else if (marketInsight.StatusKey != MarketInsightStatus.CustomerKey && e.Entity.StatusKey == ProjectStatus.WonKey)
			{
				marketInsight.StatusKey = MarketInsightStatus.CustomerKey;
				marketInsightRepository.SaveOrUpdate(marketInsight);
			}
		}
	}
}
