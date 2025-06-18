namespace Crm.MarketInsight.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;

	[Lookup("[LU].[MarketInsightStatus]", "MarketInsightStatusId")]
	public class MarketInsightStatus : EntityLookup<string>, ILookupWithColor
	{
		[LookupProperty(Shared = true)]
		public virtual string Color { get; set; }
		[LookupProperty(Shared = true)]
		public virtual bool SelectableByUser { get; set; }
		public MarketInsightStatus()
		{
			Color = "#AAAAAA";
		}

		public static readonly string UnqualifiedKey = "unqualified";
		public static readonly string QualifiedKey = "qualified";
		public static readonly string SalesKey = "sales";
		public static readonly string CustomerKey = "customer";
	}
}