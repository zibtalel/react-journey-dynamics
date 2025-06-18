
namespace Crm.Project.Rest.Model
{
	using System;
	using System.ComponentModel.DataAnnotations.Schema;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.Project.Model;
	using Crm.Rest.Model;

	[RestTypeFor(DomainType = typeof(DocumentEntry))]
	public class DocumentEntryRest : RestEntityWithExtensionValues
	{
		public Guid ContactKey { get; set; }
		public Guid PersonKey { get; set; }
		public DateTime SendDate { get; set; }
		public Guid DocumentKey { get; set; }
		public bool FeedbackReceived { get; set; }
		[NotReceived, ExplicitExpand] public PersonRest Person { get; set; }
		[NotReceived, ExplicitExpand] public DocumentAttributeRest Document { get; set; }
		[NotMapped] public override bool IsActive { get; set; }
	}
}
