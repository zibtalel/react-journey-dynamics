namespace Crm.PerDiem.Model
{
	using System;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Interfaces;
	using Crm.Library.Model;

	public class PerDiemReport : EntityBase<Guid>, ISoftDelete
	{
		public virtual DateTime From { get; set; }
		public virtual bool IsSent { get; set; }
		public virtual string SendingError { get; set; }
		public virtual string StatusKey { get; set; }
		public virtual DateTime To { get; set; }
		public virtual User User { get; set; }
		public virtual string UserName { get; set; }
		public virtual string ApprovedBy { get; set; }
		public virtual DateTime? ApprovedDate { get; set; }
	}
}
