namespace Sms.Einsatzplanung.Connector.Model
{
	using System;

	using Crm.Library.BaseModel;
	using Crm.Service.Model;

	public class RplDispatch : EntityBase<Guid>
	{
		public virtual Guid OrderKey { get; set; }
		public virtual DateTime Start { get; set; }
		public virtual DateTime Stop { get; set; }
		public virtual bool Fix { get; set; }
		public virtual string Person { get; set; }
		public virtual int Version { get; set; }
		public virtual ServiceOrderDispatch Dispatch { get; set; }
		public virtual bool Released { get; set; }
		public virtual bool Closed { get; set; }
		public virtual string TechnicianInformation { get; set; }
		public virtual string InternalInformation { get; set; }
	}
}