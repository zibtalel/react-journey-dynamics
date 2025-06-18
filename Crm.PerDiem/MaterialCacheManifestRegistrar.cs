namespace Crm.PerDiem
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
			CacheJs("perDiemMaterialJs");
			CacheJs("perDiemMaterialTs");
			Cache("Expense", "EditTemplate");
			Cache("PerDiemReport", "Details");
			Cache("TimeEntry", "Close");
			Cache("TimeEntry", "EditTemplate");
			Cache("TimeEntry", "IndexTemplate");
		}
	}
}