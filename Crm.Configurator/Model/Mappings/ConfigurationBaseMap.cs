namespace Crm.Configurator.Model.Mappings
{
	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;
	using NHibernate.Mapping.ByCode.Conformist;

	public class ConfigurationBaseMap : SubclassMapping<ConfigurationBase>
	{
		public ConfigurationBaseMap()
		{
			DiscriminatorValue("ConfigurationBase");

			this.EntitySet(x => x.ConfigurationRules, map =>
			{
				map.Key(km => km.Column("ConfigurationBaseId"));
				map.Fetch(CollectionFetchMode.Select);
				map.Lazy(CollectionLazy.Lazy);
				map.Cascade(Cascade.Persist);
				map.Inverse(true);
			}, action => action.OneToMany());

			this.EntitySet(x => x.Variables, map =>
			{
				map.Key(km => km.Column("ParentKey"));
				map.Fetch(CollectionFetchMode.Select);
				map.Lazy(CollectionLazy.Lazy);
				map.Cascade(Cascade.Persist);
				map.Inverse(true);
			}, action => action.OneToMany());
		}
	}
}