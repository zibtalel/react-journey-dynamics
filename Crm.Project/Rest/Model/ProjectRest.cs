namespace Crm.Project.Rest.Model
{
	using System;

	using Crm.Article.Rest.Model;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.Project.Model;
	using Crm.Rest.Model;

	[RestTypeFor(DomainType = typeof(Project))]
	public class ProjectRest : ContactRest
	{
		public string ProjectNo { get; set; }
		public Guid? PotentialId { get; set; }
		public string AppMiteId { get; set; }
		public string BugtrackerId { get; set; }
		public string CategoryKey { get; set; }
		public Guid? CompetitorId { get; set; }
		public string StatusKey { get; set; }
		public string StatusInfo { get; set; }
		public int? Users { get; set; }
		public decimal? Value { get; set; }
		public float WeightedValue { get; set; }
		public decimal? ContributionMargin { get; set; }
		public float WeightedContributionMargin { get; set; }
		public string CurrencyKey { get; set; }
		public DateTime? DueDate { get; set; }
		public virtual Guid? FolderId { get; set; }
		[NotReceived, RestrictedField] public virtual string FolderName { get; set; }
		public DateTime? StatusDate { get; set; }
		public string ProjectLostReasonCategoryKey { get; set; }
		public string ProjectLostReason { get; set; }
		public int? Rating { get; set; }
		[NotReceived, ExplicitExpand] public BravoRest[] Bravos { get; set; }
		[NotReceived, ExplicitExpand] public CompanyRest Competitor { get; set; }
		[NotReceived, ExplicitExpand] public CompanyRest Parent { get; set; }
		[NotReceived, ExplicitExpand] public PotentialRest Potential { get; set; }
		[NotReceived, ExplicitExpand] public AddressRest ProjectAddress { get; set; }
		[NotReceived, ExplicitExpand] public Crm.Rest.Model.TagRest[] Tags { get; set; }
		[NotReceived, ExplicitExpand] public UserRest ResponsibleUserUser { get; set; }
		[NotReceived, ExplicitExpand] public ProductFamilyRest ProductFamily { get; set; }
		public Guid? ProductFamilyKey { get; set; }
		[NotReceived, ExplicitExpand] public ProductFamilyRest MasterProductFamily { get; set; }
		public Guid? MasterProductFamilyKey { get; set; }
	}
}
