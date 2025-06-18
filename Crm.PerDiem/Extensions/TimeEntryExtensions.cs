namespace Crm.PerDiem.Extensions
{
	using System.Linq;

	using Crm.Library.Extensions;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.SearchCriteria;

	public static class TimeEntryExtensions
	{
		public static IQueryable<TimeEntry> Filter(this IQueryable<TimeEntry> timeEntries, TimeEntrySearchCriteria criteria)
		{
			if (criteria == null)
			{
				return timeEntries;
			}

			if (criteria.Username.IsNotNullOrEmpty())
			{
				timeEntries = timeEntries.Where(t => t.ResponsibleUser == criteria.Username);
			}

			if (criteria.TimeEntryTypeKey.IsNotNullOrEmpty())
			{
				timeEntries = timeEntries.Where(t => t.TimeEntryTypeKey == criteria.TimeEntryTypeKey);
			}

			timeEntries = timeEntries.Where(t => t.Date >= criteria.FromDate && t.Date <= criteria.ToDate);

			return timeEntries;
		}
	}
}