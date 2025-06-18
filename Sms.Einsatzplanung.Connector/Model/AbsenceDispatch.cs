namespace Sms.Einsatzplanung.Connector.Model;

using Crm.PerDiem.Model.Lookups;

public class AbsenceDispatch : RplDispatch
{
	public virtual string AbsenceTypeKey { get; set; }
	public virtual TimeEntryType AbsenceType => AbsenceTypeKey != null ? LookupManager.Get<TimeEntryType>(AbsenceTypeKey) : null;
}
