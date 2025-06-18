namespace Crm.Project.EventHandler
{
	using Crm.Library.Modularization.Events;
	using Crm.Project.Model;

	public class ProjectStatusChangedEvent : IEvent
	{
		public Project Project { get; protected set; }

		public ProjectStatusChangedEvent(Project project)
		{
			Project = project;
		}
	}
}