namespace Crm.Project.SearchCriteria
{
	using System;

	using Crm.Model.Lookups;
	using Crm.Project.Model.Lookups;
	using Crm.SearchCriteria;

	public class ProjectSearchCriteria : TimeSpanSearchCriteria
	{
		public string Name { get; set; }
		public string ProjectStatusKey { get; set; }
		public ProjectStatus ProjectStatus { get; set; }
		public Guid? ParentId { get; set; }
		public string ResponsibleUser { get; set; }
		public string CategoryKey { get; set; }
		public ProjectCategory Category { get; set; }
		public string CurrencyKey { get; set; }
		public Currency Currency { get; set; }
		public int? ProjectRating { get; set; }
		public string SortBy { get; set; }
		public string SortOrder { get; set; }
		public Guid? CampaignKey { get; set; }
		public bool? IsActive { get; set; }
	}
}
