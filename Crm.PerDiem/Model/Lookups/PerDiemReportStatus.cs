namespace Crm.PerDiem.Model.Lookups
{
	using System.Collections.Generic;
	using System.Linq;

	using Crm.Library.Extensions;
	using Crm.Library.Globalization.Lookup;

	[Lookup("[LU].[PerDiemReportStatus]")]
	[NotEditable]
	public class PerDiemReportStatus : EntityLookup<string>, ILookupWithSettableStatuses
	{
		[LookupProperty(Shared = true)]
		public virtual bool ShowInMobileClient { get; set; }

		public static string OpenKey = "Open";
		public static string ClosedKey = "Closed";
		public static string RequestCloseKey = "RequestClose";

		[LookupProperty(Shared = true)]
		public virtual string SettableStatuses { get; set; }
	}

	public static class PerDiemReportStatusExtensions
	{
		public static List<string> GetSettableStatusKeys(this PerDiemReportStatus status)
		{
			return status.SettableStatuses.IsNullOrWhiteSpace()
				? new List<string>()
				: status.SettableStatuses.Split(',').ToList();
		}

		public static bool IsOpen(this PerDiemReportStatus status)
		{
			return IsOpen(status?.Key);
		}
		public static bool IsOpen(this string statusKey)
		{
			return statusKey != null && statusKey == PerDiemReportStatus.OpenKey;
		}
		public static bool IsRequestClose(this PerDiemReportStatus status)
		{
			return IsRequestClose(status?.Key);
		}
		public static bool IsRequestClose(this string statusKey)
		{
			return statusKey != null && statusKey == PerDiemReportStatus.RequestCloseKey;
		}
		public static bool IsClosed(this PerDiemReportStatus status)
		{
			return IsClosed(status?.Key);
		}
		public static bool IsClosed(this string statusKey)
		{
			return statusKey != null && statusKey == PerDiemReportStatus.ClosedKey;
		}
		public static bool HasNotClosed(this PerDiemReportStatus status)
		{
			return HasNotClosed(status?.Key);
		}
		public static bool HasNotClosed(this string statusKey)
		{
			return statusKey != null && (statusKey == PerDiemReportStatus.OpenKey || statusKey == PerDiemReportStatus.RequestCloseKey);
		}
	}
}