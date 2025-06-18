namespace Crm.MarketInsight
{
	using Crm.Library.Modularization;

	[Plugin(Requires = "Crm.Project")]
	public class MarketInsightPlugin : Plugin
	{
		public static readonly string PluginName = typeof(MarketInsightPlugin).Namespace;
		public static class PermissionGroup
		{
			public const string MarketInsight = "MarketInsight";
		}
		public static class PermissionName
		{
			public const string MarketInsightTab = "MarketInsightTab";
			public const string EditContactRelationship = "EditContactRelationship";
			public const string RemoveContactRelationship = "RemoveContactRelationship";
			public const string SetStatus = "SetStatus";
		}

	}
}
