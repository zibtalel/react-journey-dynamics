namespace Crm.PerDiem.Services.Interfaces
{
	using System.Collections.Generic;

	using Crm.Library.AutoFac;

	public interface IExpenseService : ITransientDependency
	{
		IEnumerable<string> GetUsedCostCenters();
		IEnumerable<string> GetUsedCurrencies();
	}
}
