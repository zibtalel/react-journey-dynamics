namespace Sms.Einsatzplanung.Connector.Rest.Model;

using System;

using Crm.Library.Rest;

using Sms.Einsatzplanung.Connector.Model;

[RestTypeFor(DomainType = typeof(AbsenceDispatch))]
public class AbsenceDispatchRest : RestEntityWithExtensionValues
{
	public DateTime Start { get; set; }
	public DateTime Stop { get; set; }
	public bool Fix { get; set; }
	public string Person { get; set; }
	public int Version { get; set; }

	public string AbsenceTypeKey { get; set; }
}
