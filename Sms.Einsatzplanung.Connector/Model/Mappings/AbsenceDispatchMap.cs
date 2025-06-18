namespace Sms.Einsatzplanung.Connector.Model.Mappings;

using NHibernate.Mapping.ByCode.Conformist;

public class AbsenceDispatchMap : SubclassMapping<AbsenceDispatch>
{
	public AbsenceDispatchMap()
	{
		DiscriminatorValue("Absence");
		Property(x => x.AbsenceTypeKey);
	}
}
