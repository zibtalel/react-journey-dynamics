namespace Crm.PerDiem.Services.Interfaces
{
	using System.Collections.Generic;

	using Crm.Library.AutoFac;

	public interface ITimeEntryService : IDependency
	{
		IEnumerable<string> GetUsedCostCenters();
		IEnumerable<string> GetUsedTimeEntryTypes();
	}
}
