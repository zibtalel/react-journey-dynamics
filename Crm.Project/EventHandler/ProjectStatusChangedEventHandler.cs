namespace Crm.Project.EventHandler
{
	using Crm.Library.Modularization.Events;
	using Crm.Project.Model;
	using Crm.Project.Model.Lookups;

	public class ProjectStatusChangedEventHandler : IEventHandler<EntityModifiedEvent<Project>>
	{
		private readonly IEventAggregator eventAggregator;
		public ProjectStatusChangedEventHandler(IEventAggregator eventAggregator)
		{
			this.eventAggregator = eventAggregator;
		}
		public virtual void Handle(EntityModifiedEvent<Project> e)
		{
			if (e.Entity.StatusKey != e.EntityBeforeChange.StatusKey)
			{
				if (e.Entity.StatusKey == ProjectStatus.LostKey)
				{
					eventAggregator.Publish(new ProjectLostEvent(e.Entity));
				}
				else
				{
					eventAggregator.Publish(new ProjectStatusChangedEvent(e.Entity));
				}
			}
		}
	}
}
