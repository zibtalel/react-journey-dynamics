using Crm.ErpExtension.Services.Interfaces;
using Crm.Library.Globalization.Lookup;
using Crm.Model.Lookups;
using System;
using System.Collections.Generic;

namespace Crm.ErpExtension
{
	public class ErpExtensionUsedLookupsProvider : IUsedLookupsProvider
	{
		private readonly IErpDocumentService erpDocumentService;
		public ErpExtensionUsedLookupsProvider(IErpDocumentService erpDocumentService)
		{
			this.erpDocumentService = erpDocumentService;
		}

		public virtual IEnumerable<object> GetUsedLookupKeys(Type lookupType)
		{
			if (lookupType == typeof(Currency))
			{
				return erpDocumentService.GetUsedCurrencies();
			}

			return new List<object>();
		}
	}
}
