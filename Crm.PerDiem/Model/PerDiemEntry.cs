namespace Crm.PerDiem.Model
{
	using System;

	using Crm.Library.BaseModel;
	using Crm.Library.BaseModel.Interfaces;
	using Crm.Library.Model;

	public abstract class PerDiemEntry : EntityBase<Guid>, ISoftDelete
	{
		public virtual string CostCenterKey { get; set; }
		public virtual DateTime Date { get; set; }
		public virtual bool IsClosed { get; set; }
		public virtual Guid? PerDiemReportId { get; set; }
		public virtual PerDiemReport PerDiemReport { get; set; }
		public virtual string ResponsibleUser { get; set; }
		public virtual User ResponsibleUserObject { get; set; }
	}
}