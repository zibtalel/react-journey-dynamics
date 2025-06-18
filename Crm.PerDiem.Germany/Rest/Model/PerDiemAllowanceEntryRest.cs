namespace Crm.PerDiem.Germany.Rest.Model
{
	using System.Collections.Generic;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.PerDiem.Germany.Model;
	using Crm.PerDiem.Rest.Model;

	[RestTypeFor(DomainType = typeof(PerDiemAllowanceEntry))]
	public class PerDiemAllowanceEntryRest : ExpenseRest
	{
		public virtual bool AllDay { get; set; }

		public virtual string PerDiemAllowanceKey { get; set; }
		[ExplicitExpand, NotReceived] public virtual ICollection<PerDiemAllowanceEntryAllowanceAdjustmentReferenceRest> AdjustmentReferences { get; set; }
		public PerDiemAllowanceEntryRest()
		{
			AdjustmentReferences = new List<PerDiemAllowanceEntryAllowanceAdjustmentReferenceRest>();
		}
	}
}
