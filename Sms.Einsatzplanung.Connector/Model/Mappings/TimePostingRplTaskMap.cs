namespace Sms.Einsatzplanung.Connector.Model.Mappings
{
	using NHibernate.Mapping.ByCode.Conformist;

	public class TimePostingRplTaskMap : SubclassMapping<RplTimePosting>
	{
		public TimePostingRplTaskMap()
		{
			DiscriminatorValue("TimePosting");
		}
	}
}