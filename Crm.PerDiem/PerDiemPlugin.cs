namespace Crm.PerDiem
{
	using Crm.Library.Configuration;
	using Crm.Library.Modularization;

	[Plugin(ModuleId = "FLD03020", Requires = "Main")]
	public class PerDiemPlugin : Plugin
	{
		public static string PluginName = typeof(PerDiemPlugin).Namespace;

		public static class PermissionGroup
		{
			public const string TimeEntry = "TimeEntry";
			public const string Expense = "Expense";
			public const string PerDiemReport = "PerDiemReport";
		}

		public static class PermissionName
		{
			public const string SeeAllUsersExpenses = "SeeAllUsersExpenses";
			public const string SeeAllUsersPerDiemReports = "SeeAllUsersPerDiemReports";
			public const string CreateAllUsersPerDiemReports = "CreateAllUsersPerDiemReports";
			public const string SeeAllUsersTimeEntries = "SeeAllUsersTimeEntries";
			public const string SelectNonMobileLookupValues = "SelectNonMobileLookupValues";
			public const string RequestCloseReport = "RequestCloseReport";
			public const string CloseReport  = "CloseReport";
		}

		public static class Settings
		{
			public static class Email
			{
				public static SettingDefinition<string[]> PerDiemReportRecipients => new SettingDefinition<string[]>("Email/PerDiemReportRecipients", PluginName);
				public static SettingDefinition<bool> SendPerDiemReportToResponsibleUser => new SettingDefinition<bool>("Email/SendPerDiemReportToResponsibleUser", PluginName);
			}

			public static class Expense
			{

				public static SettingDefinition<int> ClosedHistorySyncPeriod => new SettingDefinition<int>("Expense/ClosedExpensesHistorySyncPeriod", PluginName);
				public static SettingDefinition<int> MaxDaysAgo => new SettingDefinition<int>("Expense/MaxDaysAgo", PluginName);
			}

			public static class PerDiemReport
			{
				public static SettingDefinition<int> ShowClosedReportsSince => new SettingDefinition<int>("PerDiemReport/ShowClosedReportsSince", PluginName);
				public static SettingDefinition<double> ReportHeaderMargin => new SettingDefinition<double>("PerDiemReport/ReportHeaderMargin", PluginName);
				public static SettingDefinition<double> ReportHeaderSpacing => new SettingDefinition<double>("PerDiemReport/ReportHeaderSpacing", PluginName);
				public static SettingDefinition<double> ReportFooterMargin => new SettingDefinition<double>("PerDiemReport/ReportFooterMargin", PluginName);
				public static SettingDefinition<double> ReportFooterSpacing => new SettingDefinition<double>("PerDiemReport/ReportFooterSpacing", PluginName);
			}

			public static class TimeEntry
			{
				public static SettingDefinition<int> ClosedHistorySyncPeriod => new SettingDefinition<int>("TimeEntry/ClosedTimeEntriesHistorySyncPeriod", PluginName);
				public static SettingDefinition<int> MinutesInterval => new SettingDefinition<int>("TimeEntry/MinutesInterval", PluginName);
				public static SettingDefinition<string> DefaultStart => new SettingDefinition<string>("TimeEntry/DefaultStart", PluginName);
				public static SettingDefinition<int> MaxDaysAgo => new SettingDefinition<int>("TimeEntry/MaxDaysAgo", PluginName);
				public static SettingDefinition<bool> AllowOverlap => new SettingDefinition<bool>("TimeEntry/AllowOverlap", PluginName);
				public static SettingDefinition<int> DefaultWorkingHoursPerDay => new SettingDefinition<int>("TimeEntry/DefaultWorkingHoursPerDay", PluginName);
			}
		}
	}
}
