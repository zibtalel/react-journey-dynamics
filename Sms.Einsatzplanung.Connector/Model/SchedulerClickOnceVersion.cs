namespace Sms.Einsatzplanung.Connector.Model
{
	using System;

	using Crm.Library.BaseModel.Interfaces;

	public class SchedulerClickOnceVersion : IAuditable
	{
		public virtual int Version { get; set; }
		public virtual DateTime CreateDate { get; set; }
		public virtual DateTime ModifyDate { get; set; }
		public virtual string CreateUser { get; set; }
		public virtual string ModifyUser { get; set; }
	}
}