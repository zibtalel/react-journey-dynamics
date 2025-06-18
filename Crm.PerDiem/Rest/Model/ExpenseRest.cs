namespace Crm.PerDiem.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.Rest.Model;

	public abstract class ExpenseRest : RestEntityWithExtensionValues
	{
		public Guid? PerDiemReportId { get; set; }
		public DateTime Date { get; set; }
		public decimal? Amount { get; set; }
		public string ResponsibleUser { get; set; }
		public bool IsClosed { get; set; }
		public string CurrencyKey { get; set; }
		public string CostCenterKey { get; set; }
		[ExplicitExpand, NotReceived] public virtual PerDiemReportRest PerDiemReport { get; set; }
		[ExplicitExpand, NotReceived] public virtual UserRest ResponsibleUserObject { get; set; }
	}
}
