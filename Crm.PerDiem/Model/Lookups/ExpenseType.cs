namespace Crm.PerDiem.Model.Lookups
{
	using System.Collections.Generic;

	using Crm.Library.Globalization.Lookup;

	[Lookup("[SMS].[ExpenseType]")]
	public class ExpenseType : EntityLookup<string>
	{
		[LookupProperty(Shared = true)]
		public virtual List<string> ValidCostCenters { get; set; }
	}
}