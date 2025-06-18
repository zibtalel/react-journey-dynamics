namespace Crm.PerDiem.SearchCriteria
{
	using System;

	public class ExpenseSearchCriteria
	{
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public string Username { get; set; }
	}
}