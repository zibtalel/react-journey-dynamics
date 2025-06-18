namespace Crm.Configurator
{

	using Crm.Library.Modularization.Interfaces;
	using Crm.Library.Offline;
	using Crm.Library.Offline.Interfaces;

	public class CacheManifestRegistrar : CacheManifestRegistrar<MaterialCacheManifest>
	{
		public CacheManifestRegistrar(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
		}
		protected override void Initialize()
		{
			CacheJs("configuratorJs");
			CacheJs("configuratorMaterialTs");
			Cache("Configuration", "IndexTemplate");
		}
	}
}