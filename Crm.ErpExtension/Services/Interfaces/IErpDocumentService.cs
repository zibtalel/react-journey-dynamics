using Crm.Library.AutoFac;
using System.Collections.Generic;

namespace Crm.ErpExtension.Services.Interfaces
{
	public interface IErpDocumentService : IDependency
	{
		IEnumerable<string> GetUsedCurrencies();
	}
}
