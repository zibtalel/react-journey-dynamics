namespace Crm.Project.EventHandler
{
	using Crm.Library.Modularization.Events;
	using Crm.Project.Model;

	public class ProjectLostEvent : IEvent
	{
		public Project Project { get; protected set; }

		public ProjectLostEvent(Project project)
		{
			Project = project;
		}
	}
}