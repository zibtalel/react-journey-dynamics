namespace Crm.PerDiem.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.Rest.Model;

	public abstract class TimeEntryRest : RestEntityWithExtensionValues
	{
		public virtual DateTime Date { get; set; }
		public virtual DateTime? From { get; set; }
		public virtual DateTime? To { get; set; }
		[RestrictedField] public virtual TimeSpan? Duration { get; set; }
		public Guid? PerDiemReportId { get; set; }
		public bool IsClosed { get; set; }
		public virtual string Description { get; set; }
		[ExplicitExpand, NotReceived] public virtual PerDiemReportRest PerDiemReport { get; set; }
		[ExplicitExpand, NotReceived] public virtual UserRest ResponsibleUserObject { get; set; }
	}
}
