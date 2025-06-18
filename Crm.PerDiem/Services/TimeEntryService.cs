namespace Crm.PerDiem.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Services.Interfaces;

	public class TimeEntryService : ITimeEntryService
	{
		private readonly IRepositoryWithTypedId<TimeEntry, Guid> timeEntryRepository;

		public virtual IEnumerable<string> GetUsedCostCenters()
		{
			return timeEntryRepository.GetAll().Select(c => c.CostCenterKey).Distinct();
		}

		public virtual IEnumerable<string> GetUsedTimeEntryTypes()
		{
			return timeEntryRepository.GetAll().ToList().Select(c => c.TimeEntryTypeKey).Distinct();
		}

		public TimeEntryService(IRepositoryWithTypedId<TimeEntry, Guid> timeEntryRepository)
		{
			this.timeEntryRepository = timeEntryRepository;
		}
	}
}
