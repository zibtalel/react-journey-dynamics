namespace Sms.Einsatzplanung.Connector
{
	using Crm.Library.Configuration;
	using Crm.Library.Modularization;

	[Plugin(ModuleId = "FLD00030", Requires = "Crm.Service")]
	public class EinsatzplanungConnectorPlugin : Plugin
	{
		public static readonly string PluginName = typeof(EinsatzplanungConnectorPlugin).Namespace;

		public static class PermissionName
		{
			public const string Scheduler = "Scheduler";
			public const string AbsenceDispatch = "AbsenceDispatch";
		}

		public static class PermissionGroup
		{
			public const string AbsenceDispatch = nameof(Model.AbsenceDispatch);
		}

		public static class Settings
		{
			public static class DashboardCalendar
			{
				public static SettingDefinition<bool> ShowAbsencesInCalendar => new SettingDefinition<bool>("DashboardCalendar/ShowAbsencesInCalendar", PluginName);
			}

			public static class System
			{
				public static SettingDefinition<string> SetupName => new SettingDefinition<string>("Setup/Name", PluginName);
				public static SettingDefinition<string> SetupFlavor => new SettingDefinition<string>("Setup/Flavor", PluginName);
				public static SettingDefinition<bool> OverrideRestEndPoint => new SettingDefinition<bool>("Setup/OverrideRestEndPoint", PluginName);
				public static SettingDefinition<bool> OverrideDatabaseHostAndCatalog => new SettingDefinition<bool>("Setup/OverrideDatabaseHostAndCatalog", PluginName);
				public static SettingDefinition<bool> AppendFlavorToCompanyName => new SettingDefinition<bool>("Setup/AppendFlavorToCompanyName", PluginName);
				public static SettingDefinition<bool> OverrideGoogleMapsApiKey => new SettingDefinition<bool>("Setup/OverrideGoogleMapsApiKey", PluginName);
			}

			public static class Export
			{
				public static class TimePosting
				{
					public static SettingDefinition<bool> Enabled => new SettingDefinition<bool>("Export/TimePostingActive", PluginName);
				}

				public static SettingDefinition<string> TimeZoneId => new SettingDefinition<string>("Export/TimeZoneId", PluginName);
			}
		}
	}
}
