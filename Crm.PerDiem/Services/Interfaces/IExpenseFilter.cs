namespace Crm.PerDiem.Services.Interfaces
{
	using System.Linq;

	using Crm.Library.AutoFac;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.SearchCriteria;

	public interface IExpenseFilter : IDependency
	{
		IQueryable<Expense> Filter(ExpenseSearchCriteria criteria);
	}
}