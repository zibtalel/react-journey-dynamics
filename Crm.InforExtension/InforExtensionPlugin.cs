namespace Crm.InforExtension
{
	using Crm.Library.Configuration;
	using Crm.Library.Modularization;

	[Plugin(Requires = "Crm.ErpExtension")]
	public class InforExtensionPlugin : Plugin
	{
		public static string PluginName = typeof(InforExtensionPlugin).Namespace;
		public static class Settings
		{
			public static class Export
			{
				public static SettingDefinition<bool> ShortFieldNames => new SettingDefinition<bool>("InforExport/ShortFieldNames", PluginName);
			}

			public static class System
			{
				public static SettingDefinition<string> InforErpComVersion => new SettingDefinition<string>("InforExport/InforErpComVersion", PluginName);
				public static SettingDefinition<string> ErpConnectionString => new SettingDefinition<string>("ErpConnectionString", PluginName);
			}
		}
	}
}