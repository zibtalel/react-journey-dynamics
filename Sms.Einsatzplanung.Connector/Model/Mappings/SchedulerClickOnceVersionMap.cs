namespace Sms.Einsatzplanung.Connector.Model.Mappings
{
	using NHibernate.Mapping.ByCode.Conformist;
	using NHibernate.Type;

	public class SchedulerClickOnceVersionMap : ClassMapping<SchedulerClickOnceVersion>
	{
		public SchedulerClickOnceVersionMap()
		{
			Schema("SMS");
			Table("SchedulerClickOnceVersion");
			Id("Id", m =>
			{
				m.Column("Id");
				m.Type(new BooleanType());
			});
			Property(x => x.Version);
			Property(x => x.CreateDate);
			Property(x => x.ModifyDate);
			Property(x => x.CreateUser);
			Property(x => x.ModifyUser);
		}
	}
}
