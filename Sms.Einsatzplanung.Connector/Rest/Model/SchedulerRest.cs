namespace Sms.Einsatzplanung.Connector.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;

	using Sms.Einsatzplanung.Connector.Model;

	[RestTypeFor(DomainType = typeof(Scheduler))]
	public class SchedulerRest : RestEntity
	{
		public Guid Id { get; set; }
		public string VersionString { get; set; }
		[NotReceived]
		[RestrictedField]
		public string ManifestVersion { get; set; }
		public int ClickOnceVersion { get; set; }
		public string Warnings { get; set; }
		public bool IsReleased { get; set; }
		public Guid? IconKey { get; set; }
		[NotReceived, ExplicitExpand]
		public SchedulerIconRest Icon { get; set; }
		public Guid? ConfigKey { get; set; }
		[NotReceived, ExplicitExpand]
		public SchedulerConfigRest Config { get; set; }
	}
}
