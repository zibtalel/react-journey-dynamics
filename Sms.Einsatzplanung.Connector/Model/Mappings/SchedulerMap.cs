namespace Sms.Einsatzplanung.Connector.Model.Mappings
{
	using System;

	using Crm.Library.BaseModel.Mappings;

	using NHibernate.Mapping.ByCode;

	public class SchedulerMap : EntityClassMapping<Scheduler>
	{
		public SchedulerMap()
		{
			Schema("SMS");
			Table("Scheduler");
			Id(x => x.Id, m =>
			{
				m.Column("SchedulerId");
				m.Generator(LMobile.Unicore.NHibernate.GuidCombGeneratorDef.Instance);
				m.UnsavedValue(Guid.Empty);
			});
			Property(x => x.VersionString, m => m.Column("Version"));
			Property(x => x.ClickOnceVersion);
			Property(x => x.Warnings);
			Property(x => x.IsReleased);

			Property(x => x.IconKey, m => m.Column("SchedulerIconKey"));
			ManyToOne(x => x.Icon, m =>
			{
				m.Column("SchedulerIconKey");
				m.Fetch(FetchKind.Select);
				m.Lazy(LazyRelation.Proxy);
				m.Cascade(Cascade.None);
				m.Insert(false);
				m.Update(false);
			});

			Property(x => x.ConfigKey, m => m.Column("SchedulerConfigKey"));
			ManyToOne(x => x.Config, m =>
			{
				m.Column("SchedulerConfigKey");
				m.Fetch(FetchKind.Select);
				m.Lazy(LazyRelation.Proxy);
				m.Cascade(Cascade.None);
				m.Insert(false);
				m.Update(false);
			});
		}
	}
}