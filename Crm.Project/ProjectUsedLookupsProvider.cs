namespace Crm.Project
{
	using System;
	using System.Collections.Generic;

	using Crm.Library.Globalization.Lookup;
	using Crm.Model.Lookups;
	using Crm.Project.Model.Lookups;
	using Crm.Project.Services.Interfaces;

	public class ProjectUsedLookupsProvider : IUsedLookupsProvider
	{
		private readonly IProjectService projectService;
		public ProjectUsedLookupsProvider(IProjectService projectService)
		{
			this.projectService = projectService;
		}

		public virtual IEnumerable<object> GetUsedLookupKeys(Type lookupType)
		{
			if (lookupType == typeof(ProjectCategory))
			{
				return projectService.GetUsedProjectCategories();
			}

			if (lookupType == typeof(ProjectStatus))
			{
				return projectService.GetUsedProjectStatuses();
			}

			if (lookupType == typeof(ProjectLostReasonCategory))
			{
				return projectService.GetUsedLostReasonCategories();
			}

			if (lookupType == typeof(PaymentCondition))
			{
				return projectService.GetUsedPaymentConditions();
			}

			if (lookupType == typeof(Currency))
			{
				return projectService.GetUsedCurrencies();
			}

			if (lookupType == typeof(ProjectContactRelationshipType))
			{
				return projectService.GetUsedProjectContactRelationshipTypes();
			}

			return new List<object>();
		}
	}
}
