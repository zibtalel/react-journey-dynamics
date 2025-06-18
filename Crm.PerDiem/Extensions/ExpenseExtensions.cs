namespace Crm.PerDiem.Extensions
{
	using System.Linq;

	using Crm.Library.Extensions;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.SearchCriteria;

	public static class ExpenseExtensions
	{
		public static IQueryable<Expense> Filter(this IQueryable<Expense> expenses, ExpenseSearchCriteria criteria)
		{
			if (criteria == null)
			{
				return expenses;
			}

			expenses = expenses.Where(e => e.Date >= criteria.FromDate && e.Date <= criteria.ToDate);
			
			if (criteria.Username.IsNotNullOrEmpty())
			{
				expenses = expenses.Where(e => e.ResponsibleUser == criteria.Username);
			}

			return expenses;
		}
	}
}