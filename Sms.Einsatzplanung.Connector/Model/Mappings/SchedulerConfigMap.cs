namespace Sms.Einsatzplanung.Connector.Model.Mappings
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;

	public class SchedulerConfigMap : EntityClassMapping<SchedulerConfig>
	{
		public SchedulerConfigMap()
		{
			Schema("SMS");
			Table("SchedulerConfig");
			Id(x => x.Id, m =>
			{
				m.Column("SchedulerConfigId");
				m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				m.UnsavedValue(Guid.Empty);
			});
			Property(x => x.Config, m => m.Length(1048576));
			this.EntitySet(x => x.Schedulers, map =>
			{
				map.Key(km => km.Column("SchedulerConfigKey"));
				map.Fetch(CollectionFetchMode.Select);
				map.Lazy(CollectionLazy.Lazy);
				map.Cascade(Cascade.Persist);
				map.Inverse(true);
			}, action => action.OneToMany());

		}
	}
}