namespace Crm.PerDiem.Model.Lookups
{
	using System.Collections.Generic;

	using Crm.Library.Globalization.Lookup;

	[Lookup("[SMS].[TimeEntryType]")]
	public class TimeEntryType : EntityLookup<string>, ILookupWithColor
	{
		[LookupProperty(Shared = true)]
		public virtual bool DecreasesCapacity { get; set; }
		[LookupProperty(Shared = true)]
		public virtual bool ShowInMobileClient { get; set; }
		[LookupProperty(Shared = true)]
		public virtual bool ShowInScheduler { get; set; }
		[LookupProperty(Shared = true)]
		public virtual string Color { get; set; }
		[LookupProperty(Shared = true)]
		public virtual int? DefaultDurationInMinutes { get; set; }
		[LookupProperty(Shared = true)]
		public virtual List<string> ValidCostCenters { get; set; }

		public TimeEntryType()
		{
			Color = "#AAAAAA";
		}
	}
}