namespace Sms.Einsatzplanung.Connector.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;

	using Sms.Einsatzplanung.Connector.Model;

	[RestTypeFor(DomainType = typeof(SchedulerConfig))]
	public class SchedulerConfigRest : RestEntity
	{
		public Guid Id { get; set; }
		public byte[] Config { get; set; }
		[ExplicitExpand]
		[NotReceived]
		public SchedulerRest[] Schedulers { get; set; }
		[RestrictedField]
		public string Type { get; set; }
	}
}
