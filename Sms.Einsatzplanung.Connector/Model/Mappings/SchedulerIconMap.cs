namespace Sms.Einsatzplanung.Connector.Model.Mappings
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;

	public class SchedulerIconMap : EntityClassMapping<SchedulerIcon>
	{
		public SchedulerIconMap()
		{
			Schema("SMS");
			Table("SchedulerIcon");
			Id(x => x.Id, m =>
			{
				m.Column("SchedulerIconId");
				m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				m.UnsavedValue(Guid.Empty);
			});
			Property(x => x.Icon, m => m.Length(1048576));
			this.EntitySet(x => x.Schedulers, map =>
			{
				map.Key(km => km.Column("SchedulerIconKey"));
				map.Fetch(CollectionFetchMode.Select);
				map.Lazy(CollectionLazy.Lazy);
				map.Cascade(Cascade.Persist);
				map.Inverse(true);
			}, action => action.OneToMany());
		}
	}
}