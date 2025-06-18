namespace Crm.Project.Rest.Model
{
	using System;

	using Crm.Article.Rest.Model;
	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.Project.Model;
	using Crm.Rest.Model;

	using AddressRest = Crm.Rest.Model.AddressRest;
	using UserRest = Crm.Rest.Model.UserRest;

	[RestTypeFor(DomainType = typeof(Potential))]
	public class PotentialRest : ContactRest
	{
		public string PotentialNo { get; set; }
		public string StatusKey { get; set; }
		public string PriorityKey { get; set; }
		public DateTime? CloseDate { get; set; }
		public DateTime? StatusDate { get; set; }
		[NotReceived, ExplicitExpand] public CompanyRest Parent { get; set; }
		[NotReceived, ExplicitExpand] public AddressRest PotentialAddress { get; set; }
		[NotReceived, ExplicitExpand] public TagRest[] Tags { get; set; }
		[NotReceived, ExplicitExpand] public UserRest ResponsibleUserUser { get; set; }
		[NotReceived, ExplicitExpand] public ProductFamilyRest ProductFamily { get; set; }
		public Guid? ProductFamilyKey { get; set; }
		[NotReceived, ExplicitExpand] public ProductFamilyRest MasterProductFamily { get; set; }
		public Guid? MasterProductFamilyKey { get; set; }
		[NotReceived, ExplicitExpand, RestrictedField] public  DateTime? LastNoteDate { get; set; }

	}
}
