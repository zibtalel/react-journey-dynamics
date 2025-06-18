namespace Crm.PerDiem.Germany.Services
{
	using System;
	using System.Linq;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.PerDiem.Extensions;
	using Crm.PerDiem.Germany.Model;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.SearchCriteria;
	using Crm.PerDiem.Services.Interfaces;

	public class ExpenseFilter : IExpenseFilter
	{
		private readonly IRepositoryWithTypedId<PerDiemAllowanceEntry, Guid> perDiemAllowanceEntryRepository;

		public ExpenseFilter(IRepositoryWithTypedId<PerDiemAllowanceEntry, Guid> perDiemAllowanceEntryRepository)
		{
			this.perDiemAllowanceEntryRepository = perDiemAllowanceEntryRepository;
		}

		public virtual IQueryable<Expense> Filter(ExpenseSearchCriteria criteria)
		{
			return perDiemAllowanceEntryRepository.GetAll().Filter(criteria);
		}
	}
}
