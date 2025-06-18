namespace Sms.Einsatzplanung.Connector.Model
{
	using System;
	using System.Collections.Generic;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Interfaces;

	public class SchedulerConfig : EntityBase<Guid>, ISoftDelete, INoAuthorisedObject
	{
		public virtual byte[] Config { get; set; }
		public virtual ICollection<Scheduler> Schedulers { get; set; }
		public virtual string Type { get; set; }
	}
}