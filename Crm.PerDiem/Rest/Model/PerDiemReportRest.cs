namespace Crm.PerDiem.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.PerDiem.Model;
	using Crm.Rest.Model;

	[RestTypeFor(DomainType = typeof(PerDiemReport))]
	public class PerDiemReportRest : RestEntityWithExtensionValues
	{
		public DateTime From { get; set; }
		public string StatusKey { get; set; }
		public DateTime To { get; set; }
		[ExplicitExpand]
		[NotReceived]
		public UserRest User { get; set; }
		public string UserName { get; set; }
		public string ApprovedBy { get; set; }
		public DateTime? ApprovedDate { get; set; }
	}
}
