namespace Crm.Project.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.Project.Model.Relationships;
	using Crm.Rest.Model;

	[RestTypeFor(DomainType = typeof(ProjectContactRelationship))]
	public class ProjectContactRelationshipRest : RestEntityWithExtensionValues
	{
		public Guid ParentId { get; set; }
		public Guid ChildId { get; set; }
		public string Information { get; set; }
		public string RelationshipTypeKey { get; set; }
		[ExplicitExpand, NotReceived] public ProjectRest Project { get; set; }
		[ExplicitExpand, NotReceived] public PersonRest ChildPerson { get; set; }
		[ExplicitExpand, NotReceived] public CompanyRest ChildCompany { get; set; }
	}
}
