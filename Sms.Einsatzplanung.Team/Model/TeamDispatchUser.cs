using System;

namespace Sms.Einsatzplanung.Team.Model
{
	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Interfaces;
	using Crm.Library.Model;
	using Crm.Service.Model;

	public class TeamDispatchUser : EntityBase<Guid>, ISoftDelete
	{
		public virtual Guid DispatchId { get; set; }
		public virtual ServiceOrderDispatch Dispatch { get; set; }
		public virtual string Username { get; set; }
		public virtual User User { get; set; }
		public virtual bool IsNonTeamMember { get; set; }
	}
}
