﻿namespace Sms.Einsatzplanung.Team
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
			CacheJs("smsEinsatzplanungTeamMaterialTs");
		}
	}
}
