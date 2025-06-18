namespace Crm.PerDiem.Rest.Model
{
	using System;

	using Crm.Library.Api.Attributes;
	using Crm.Library.Rest;
	using Crm.PerDiem.Model;
	using Crm.Rest.Model;

	[RestTypeFor(DomainType = typeof(UserExpense))]
	public class UserExpenseRest : ExpenseRest
	{
		public string Description { get; set; }
		public Guid? FileResourceKey { get; set; }
		public string ExpenseTypeKey { get; set; }
		[ExplicitExpand, NotReceived] public virtual FileResourceRest FileResource { get; set; }
	}
}
