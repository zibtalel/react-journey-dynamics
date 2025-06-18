using System;
using System.Collections.Generic;

namespace Crm.Project.Model
{
	using Crm.Article.Model;
	using Crm.Model;
	using Crm.Model.Interfaces;
	using Crm.Project.Model.Lookups;
	using Crm.Project.Model.Relationships;

	public class Potential : Contact, IEntityWithTags
	{
		public virtual Guid? MasterProductFamilyKey { get; set; }
		public virtual ProductFamily MasterProductFamily { get; set; }
		public virtual Guid? ProductFamilyKey { get; set; }
		public virtual ProductFamily ProductFamily { get; set; }
		public virtual string PotentialNo { get; set; }
		public virtual string StatusKey { get; set; }
		public virtual PotentialStatus Status
		{
			get { return StatusKey != null ? LookupManager.Get<PotentialStatus>(StatusKey) : null; }
		}
		public virtual Guid? CampaignKey { get; set; }
		public virtual DateTime? CloseDate { get; set; }
		public virtual DateTime? StatusDate { get; set; }
		public virtual ICollection<PotentialContactRelationship> ContactRelationships { get; set; }
		public virtual ICollection<Task> Tasks { get; set; }
		public virtual string PriorityKey { get; set; }
		public virtual PotentialPriority Priority
		{
			get { return PriorityKey != null ? LookupManager.Get<PotentialPriority>(PriorityKey) : null; }
		}
		public Potential()
		{
			IsActive = true;
			Tasks = new List<Task>();
			ContactRelationships = new List<PotentialContactRelationship>();
		}
	}
}