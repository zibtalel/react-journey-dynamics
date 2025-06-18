namespace Crm.MarketInsight.EventHandlers
{
	using System;
	using System.Linq;

	using Crm.Article.Services.Interfaces;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Modularization.Events;
	using Crm.MarketInsight.Model;
	using Crm.MarketInsight.Model.Lookups;
	using Crm.Project.Model;

	public class ProjectCreatedEventHandler : IEventHandler<EntityCreatedEvent<Project>>
	{
		private readonly IRepositoryWithTypedId<MarketInsight, Guid> marketInsightRepository;
		private readonly IProductFamilyService productFamilyService;
		public ProjectCreatedEventHandler(IRepositoryWithTypedId<MarketInsight, Guid> marketInsightRepository, IProductFamilyService productFamilyService)
		{
			this.marketInsightRepository = marketInsightRepository;
			this.productFamilyService = productFamilyService;
		}

		public virtual void Handle(EntityCreatedEvent<Project> e)
		{
			if (e.Entity.ProductFamilyKey != null)
			{
				var firstProductFamily = productFamilyService.GetFirstProductFamilyInHierarchy((Guid)e.Entity.ProductFamilyKey);
				var marketInsight = marketInsightRepository.GetAll().FirstOrDefault(x => x.ParentId == e.Entity.ParentId && x.ProductFamilyKey == firstProductFamily.Id);
				if (marketInsight != null && (marketInsight.StatusKey == MarketInsightStatus.UnqualifiedKey || marketInsight.StatusKey == MarketInsightStatus.QualifiedKey))
				{
					marketInsight.StatusKey = MarketInsightStatus.SalesKey;
					marketInsightRepository.SaveOrUpdate(marketInsight);
				}
				else if (marketInsight == null)
				{
					if (firstProductFamily == null)
					{
						return;
					}
					marketInsight = new MarketInsight
					{
						ParentId = e.Entity.ParentId,
						ProductFamilyKey = firstProductFamily.Id,
						Name = firstProductFamily.Name,
						StatusKey = MarketInsightStatus.SalesKey
					};
					marketInsightRepository.SaveOrUpdate(marketInsight);
				}
			}
		}
	}

}
