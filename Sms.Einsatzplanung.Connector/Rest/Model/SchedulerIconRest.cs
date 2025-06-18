namespace Sms.Einsatzplanung.Connector.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;

	using Sms.Einsatzplanung.Connector.Model;

	[RestTypeFor(DomainType = typeof(SchedulerIcon))]
	public class SchedulerIconRest : RestEntity
	{
		public Guid Id { get; set; }
		public byte[] Icon { get; set; }
		[ExplicitExpand]
		[NotReceived]
		public SchedulerRest[] Schedulers { get; set; }
	}
}
