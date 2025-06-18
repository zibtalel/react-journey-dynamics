namespace Crm.PerDiem.Services.Interfaces
{
	using System.Collections.Generic;

	using Crm.Library.AutoFac;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.SearchCriteria;

	public interface ITimeEntryFilter : IDependency
	{
		List<TimeEntry> Filter(List<TimeEntry> timeEntries, TimeEntrySearchCriteria criteria);
	}
}