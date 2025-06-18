namespace Crm.Project.Model
{
	using System;
	using System.Collections.Generic;

	using Crm.Article.Model;
	using Crm.Model;
	using Crm.Model.Interfaces;
	using Crm.Model.Lookups;
	using Crm.Project.Model.Lookups;
	using Crm.Project.Model.Relationships;

	public class Project : Contact, IWithFolder, IEntityWithTags
	{
		// Properties
		public virtual Guid? MasterProductFamilyKey { get; set; }
		public virtual ProductFamily MasterProductFamily { get; set; }
		public virtual Guid? ProductFamilyKey { get; set; }
		public virtual ProductFamily ProductFamily { get; set; }
		public virtual string ProjectNo { get; set; }
		public virtual Guid? PotentialId { get; set; }
		public virtual Potential Potential { get; set; }
		public virtual string AppMiteId { get; set; }
		public virtual string BugtrackerId { get; set; }
		public virtual Contact Competitor { get; set; }
		public virtual Guid? CompetitorId { get; set; }
		public virtual decimal? ContributionMargin { get; set; }
		public virtual DateTime? DueDate { get; set; }
		public virtual string ProjectLostReason { get; set; }
		public virtual string ProjectLostReasonCategoryKey { get; set; }
		public virtual int? Rating { get; set; }
		public virtual DateTime? StatusDate { get; set; }
		public virtual string StatusInfo { get; set; }
		public virtual int? Users { get; set; }
		public virtual decimal? Value { get; set; }
		public virtual float WeightedContributionMargin { get; protected set; }
		public virtual float WeightedValue { get; protected set; }

		public virtual Guid? FolderId { get; set; }
		public virtual string FolderName
		{
			get { return Folder != null ? Folder.Name : null; }
		}
		public virtual Folder Folder { get; set; }
		public virtual Address ProjectAddress { get; set; }

		public virtual string CategoryKey { get; set; }
		public virtual ProjectCategory Category
		{
			get { return CategoryKey != null ? LookupManager.Get<ProjectCategory>(CategoryKey) : null; }
		}
		public virtual string StatusKey { get; set; }
		public virtual ProjectStatus Status
		{
			get { return StatusKey != null ? LookupManager.Get<ProjectStatus>(StatusKey) : null; }
		}
		public virtual string CurrencyKey { get; set; }
		public virtual Currency Currency
		{
			get { return CurrencyKey != null ? LookupManager.Get<Currency>(CurrencyKey) : null; }
		}
		public virtual ProjectLostReasonCategory ProjectLostReasonCategory
		{
			get { return ProjectLostReasonCategoryKey != null ? LookupManager.Get<ProjectLostReasonCategory>(ProjectLostReasonCategoryKey) : null; }
		}
		public virtual string PaymentConditionKey { get; set; }
		public virtual PaymentCondition PaymentCondition
		{
			get { return PaymentConditionKey != null ? LookupManager.Get<PaymentCondition>(PaymentConditionKey) : null; }
		}

		public virtual ICollection<ProjectContactRelationship> ContactRelationships { get; set; }
		public override string ReferenceLink
		{
			get { return ToString(); }
		}
		public override string ToString()
		{
			return String.IsNullOrWhiteSpace(ProjectNo) ? LegacyName : String.Format("{0} - {1}", ProjectNo, Name);
		}

		public Project()
		{
			IsActive = true;
		}
	}
}
