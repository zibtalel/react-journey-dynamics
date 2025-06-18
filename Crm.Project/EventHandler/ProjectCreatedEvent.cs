namespace Crm.Project.EventHandler
{
	using Crm.Library.Modularization.Events;
	using Crm.Project.Model;

	public class ProjectCreatedEvent : IEvent
	{
		public Project Project { get; protected set; }

		public ProjectCreatedEvent(Project project)
		{
			Project = project;
		}
	}
}