namespace Crm.Project
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
			CacheJs("projectsMaterialJs");
			CacheJs("projectMaterialTs");
			Cache("ContactPerson", "EditTemplate");
			Cache("Project", "CreateTemplate");
			Cache("Project", "DetailsTemplate");
			Cache("Project", "SetProjectStatus");
			Cache("ProjectContactRelationship", "EditTemplate");
			Cache("ProjectList", "FilterTemplate");
			Cache("ProjectList", "GetIcsLink");
			Cache("ProjectList", "IndexTemplate");
			Cache("Potential", "CreateTemplate");
			Cache("Potential", "DetailsTemplate");
			Cache("PotentialContactRelationship", "EditTemplate");
			Cache("PotentialList", "IndexTemplate");
			Cache("PotentialList", "FilterTemplate");
		}
	}
}
