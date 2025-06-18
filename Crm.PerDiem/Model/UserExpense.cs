namespace Crm.PerDiem.Model
{
	using System;

	using Crm.Model;

	public class UserExpense : Expense
	{
		public virtual string Description { get; set; }
		public virtual string ExpenseTitle => "Expense";
		public virtual string ExpenseTypeKey { get; set; }
		public virtual Guid? FileResourceKey { get; set; }
		public virtual FileResource FileResource { get; set; }
	}
}