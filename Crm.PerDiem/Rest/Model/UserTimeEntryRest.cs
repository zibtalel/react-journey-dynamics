namespace Crm.PerDiem.Rest.Model
{
	using Crm.Library.Rest;
	using Crm.PerDiem.Model;

	[RestTypeFor(DomainType = typeof(UserTimeEntry))]
	public class UserTimeEntryRest : TimeEntryRest
	{
		public string TimeEntryTypeKey { get; set; }
		public string ResponsibleUser { get; set; }
		public string CostCenterKey { get; set; }
	}
}
