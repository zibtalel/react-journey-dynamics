namespace Crm.PerDiem.SearchCriteria
{
	using System;

	public class TimeEntrySearchCriteria
	{
		public string TimeEntryTypeKey { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string Username { get; set; }
		public bool IsExported { get; set; }
	}
}