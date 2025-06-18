namespace Crm.PerDiem.Services
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.PerDiem.Model;
	using Crm.PerDiem.Services.Interfaces;

	public class ExpenseService : IExpenseService
	{
		private readonly IRepositoryWithTypedId<Expense, Guid> expenseRepository;

		public virtual IEnumerable<string> GetUsedCostCenters()
		{
			return expenseRepository.GetAll().Select(c => c.CostCenterKey).Distinct();
		}

		public virtual IEnumerable<string> GetUsedCurrencies()
		{
			return expenseRepository.GetAll().Select(c => c.CurrencyKey).Distinct();
		}

		public ExpenseService(IRepositoryWithTypedId<Expense, Guid> expenseRepository)
		{
			this.expenseRepository = expenseRepository;
		}
	}
}
