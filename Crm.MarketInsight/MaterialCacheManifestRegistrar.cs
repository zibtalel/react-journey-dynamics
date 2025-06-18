namespace Crm.MarketInsight
{
	using Crm.Library.Modularization.Interfaces;
	using Crm.Library.Offline;
	using Crm.Library.Offline.Interfaces;

	public class MaterialCacheManifestRegistrar : CacheManifestRegistrar<MaterialCacheManifest>
	{
		public MaterialCacheManifestRegistrar(IPluginProvider pluginProvider)
			: base(pluginProvider)
		{
		}
		protected override void Initialize()
		{
			CacheJs("marketInsightTs");
			CacheCss("marketInsightStyle");
			Cache("MarketInsight", "EditTemplate");
			Cache("MarketInsight", "DetailsTemplate");
		}
	}
}