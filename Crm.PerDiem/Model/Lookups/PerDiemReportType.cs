namespace Crm.PerDiem.Model.Lookups
{
	using Crm.Library.Globalization.Lookup;

	[Lookup("[LU].[PerDiemReportType]")]
	public class PerDiemReportType : EntityLookup<string>
	{
		public const string CustomKey = "Custom";
		public const string MonthlyKey = "Monthly";
		public const string WeeklyKey = "Weekly";
	}
}