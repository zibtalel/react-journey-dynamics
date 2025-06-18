namespace Sms.Einsatzplanung.Connector.Model
{
	using System;
	using System.Collections.Generic;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Interfaces;

	public class SchedulerIcon : EntityBase<Guid>, ISoftDelete, INoAuthorisedObject
	{
		public virtual byte[] Icon { get; set; }
		public virtual ICollection<Scheduler> Schedulers { get; set; }
	}
}