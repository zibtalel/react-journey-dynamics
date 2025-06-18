namespace Crm.MarketInsight.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;

	[Lookup("[LU].[MarketInsightReference]", "ReferenceOptionId")]
	public class MarketInsightReference : EntityLookup<string>
	{
		public static readonly string SuitableKey = "suitable";
		public static readonly string ConfirmedKey = "confirmed";
	}
}