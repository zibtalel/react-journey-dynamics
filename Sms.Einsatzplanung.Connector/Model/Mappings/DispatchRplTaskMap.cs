namespace Sms.Einsatzplanung.Connector.Model.Mappings
{
	using NHibernate.Mapping.ByCode.Conformist;

	public class DispatchRplTaskMap : SubclassMapping<RplServiceOrderDispatch>
	{
		public DispatchRplTaskMap()
		{
			DiscriminatorValue("ServiceOrderDispatch");
		}
	}
}