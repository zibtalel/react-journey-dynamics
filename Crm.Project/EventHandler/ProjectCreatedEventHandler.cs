namespace Crm.Project.EventHandler
{
	using Crm.Library.Modularization.Events;
	using Crm.Project.Model;

	public class ProjectCreatedHandler : IEventHandler<EntityCreatedEvent<Project>>
	{
		private readonly IEventAggregator eventAggregator;
		public ProjectCreatedHandler(IEventAggregator eventAggregator)
		{
			this.eventAggregator = eventAggregator;
		}
		public virtual void Handle(EntityCreatedEvent<Project> e) => eventAggregator.Publish(new ProjectCreatedEvent(e.Entity));
	}
}
