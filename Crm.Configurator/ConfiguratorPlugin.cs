namespace Crm.Configurator
{
	using Crm.Library.Modularization;

	[Plugin(ModuleId = "FLD03090", Requires = "Crm.Order,Crm.Article")]
	public class ConfiguratorPlugin : Plugin
	{
		public static readonly string PluginName = typeof(ConfiguratorPlugin).Namespace;

		public static class PermissionGroup
		{
			public const string ConfigurationRules = "ConfigurationRules";
			public const string Configurator = "Configurator";
		}
	}
}