namespace Crm.Configurator.Model.Mappings
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;

	public class ConfigurationRuleMap : EntityClassMapping<ConfigurationRule>
	{
		public ConfigurationRuleMap()
		{
			Schema("CRM");
			Table("ConfigurationRule");

			Id(x => x.Id, map =>
			{
				map.Column("ConfigurationRuleId");
				map.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				map.UnsavedValue(Guid.Empty);
			});

			Property(x => x.ConfigurationBaseId);
			Property(x => x.Validation);

			Set(x => x.VariableValues, map =>
			{
				map.Schema("CRM");
				map.Table("ConfigurationRuleVariableValue");
				map.Key(km => km.Column("ConfigurationRuleId"));
				map.Fetch(CollectionFetchMode.Select);
				map.BatchSize(100);
				map.Lazy(CollectionLazy.Lazy);
				map.Cascade(Cascade.Persist);
				map.BatchSize(100);
			}, r => r.ManyToMany(m => m.Column("VariableValueId")));

			Set(x => x.AffectedVariableValues, map =>
			{
				map.Schema("CRM");
				map.Table("ConfigurationRuleAffectedVariableValue");
				map.Key(km => km.Column("ConfigurationRuleId"));
				map.Fetch(CollectionFetchMode.Select);
				map.BatchSize(100);
				map.Lazy(CollectionLazy.Lazy);
				map.Cascade(Cascade.Persist);
				map.BatchSize(100);
			}, r => r.ManyToMany(m => m.Column("VariableValueId")));
		}
	}
}