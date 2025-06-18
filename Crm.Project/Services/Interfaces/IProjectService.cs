namespace Crm.Project.Services.Interfaces
{
	using System.Collections.Generic;

	using Crm.Library.AutoFac;

	public interface IProjectService : ITransientDependency
	{
		IEnumerable<string> GetUsedProjectCategories();
		IEnumerable<string> GetUsedProjectStatuses();
		IEnumerable<string> GetUsedLostReasonCategories();
		IEnumerable<string> GetUsedCurrencies();
		IEnumerable<string> GetUsedPaymentConditions();
		IEnumerable<string> GetUsedProjectContactRelationshipTypes();
	}
}
