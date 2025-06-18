namespace Crm.Project.EventHandler
{
	using System;
	using System.Collections.Generic;

	using Crm.Library.Modularization.Events;

	public class ProjectDeletedEvent : IEvent
	{
		// Ids of the deleted projects
		public IList<Guid> ProjectIds { get; protected set; }

		public ProjectDeletedEvent(params Guid[] projectIds)
			: this(new List<Guid>(projectIds))
		{
		}

		public ProjectDeletedEvent(IList<Guid> projectIds)
		{
			ProjectIds = projectIds;
		}
	}
}