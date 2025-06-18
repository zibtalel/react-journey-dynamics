using Crm.ErpExtension.Model;
using Crm.ErpExtension.Services.Interfaces;
using Crm.Library.Data.Domain.DataInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crm.ErpExtension.Services
{
	public class ErpDocumentService : IErpDocumentService
	{
		private readonly IRepositoryWithTypedId<ErpDocument, Guid> erpDocumentRepository;

		public ErpDocumentService(IRepositoryWithTypedId<ErpDocument, Guid> erpDocumentRepository)
		{
			this.erpDocumentRepository = erpDocumentRepository;
		}

		public virtual IEnumerable<string> GetUsedCurrencies()
		{
			return erpDocumentRepository.GetAll().Select(c => c.CurrencyKey).Distinct();
		}
	}
}
