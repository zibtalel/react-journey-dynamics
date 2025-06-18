namespace Sms.Einsatzplanung.Connector.Rest.Model
{
	using System;

	using Crm.Library.Rest;

	using Sms.Einsatzplanung.Connector.Model;

	[RestTypeFor(DomainType = typeof(SchedulerBinary))]
	public class  SchedulerBinaryRest : RestEntity
	{
		public string Content { get; set; }
		public string ContentType { get; set; }
		public int Id { get; set; }
		public string Filename { get; set; }
		public DateTime LastWriteTimeUtc { get; set; }

		public int Length { get; set; }
		public string Version { get; set; }
	}
}
