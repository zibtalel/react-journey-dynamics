using Microsoft.AspNetCore.Mvc;

namespace Crm.ErpExtension.Controllers
{
	using System;
	using System.Linq;
	using System.Text;
	using Crm.ErpExtension.Helpers;
	using Crm.ErpExtension.Model;
	using Crm.Helpers;
	using Crm.Library.Data.Domain.DataInterfaces;
	using Crm.Library.Helper;
	using Crm.Library.Model;
	using Crm.Library.Modularization;
	using Microsoft.AspNetCore.Authorization;
	using PermissionGroup = ErpPlugin.PermissionGroup;

	[Authorize]
	public class ErpDocumentController : Controller
	{
		private readonly IRepositoryWithTypedId<Quote, Guid> quoteRepository;
		private readonly IRepositoryWithTypedId<DeliveryNote, Guid> deliveryNoteRepository;
		private readonly IRepositoryWithTypedId<Invoice, Guid> invoiceRepository;
		private readonly IRepositoryWithTypedId<SalesOrder, Guid> salesOrderRepository;
		private readonly IRepositoryWithTypedId<CreditNote, Guid> creditNoteRepository;
		private readonly IAppSettingsProvider appSettingsProvider;
		private readonly IRepositoryWithTypedId<MasterContract, Guid> masterContractRepository;
		public ErpDocumentController(IRepositoryWithTypedId<Quote, Guid> quoteRepository, IRepositoryWithTypedId<DeliveryNote, Guid> deliveryNoteRepository, IRepositoryWithTypedId<Invoice, Guid> invoiceRepository, IRepositoryWithTypedId<SalesOrder, Guid> salesOrderRepository, IRepositoryWithTypedId<CreditNote, Guid> creditNoteRepository, IAppSettingsProvider appSettingsProvider, IRepositoryWithTypedId<MasterContract, Guid> masterContractRepository)
		{
			this.quoteRepository = quoteRepository;
			this.deliveryNoteRepository = deliveryNoteRepository;
			this.invoiceRepository = invoiceRepository;
			this.salesOrderRepository = salesOrderRepository;
			this.creditNoteRepository = creditNoteRepository;
			this.appSettingsProvider = appSettingsProvider;
			this.masterContractRepository = masterContractRepository;
		}

		protected virtual ObjectLinkIntegration GetIntegrationType()
		{
			var objectLinkIntegration = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ObjectLinkIntegration);
			return objectLinkIntegration;
		}

		[RenderAction("MaterialErpDocumentsTab")]
		public virtual ActionResult MaterialErpDocumentsQuote()
		{
			return PartialView();
		}

		[RenderAction("MaterialErpDocumentsTab")]
		public virtual ActionResult MaterialErpDocumentsSalesOrder()
		{
			return PartialView();
		}

		[RenderAction("MaterialErpDocumentsTab")]
		public virtual ActionResult MaterialErpDocumentsDeliveryNote()
		{
			return PartialView();
		}

		[RenderAction("MaterialErpDocumentsTab")]
		public virtual ActionResult MaterialErpDocumentsInvoice()
		{
			return PartialView();
		}

		[RenderAction("MaterialErpDocumentsTab")]
		public virtual ActionResult MaterialErpDocumentsCreditNote()
		{
			return PartialView();
		}

		[RenderAction("MaterialErpDocumentsTab")]
		public virtual ActionResult MaterialErpDocumentsMasterContract()
		{
			return PartialView();
		}

		[RequiredPermission(ErpPlugin.PermissionName.DocumentSummary, Group = PermissionGroup.Erp)]
		public virtual ActionResult OpenQuote(Guid id)
		{
			var quote = quoteRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (quote == null)
			{
				return new NotFoundResult();
			}

			var erpSystemName = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemName);
			var erpSystemId = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemID);
			switch (GetIntegrationType())
			{
				case ObjectLinkIntegration.D3Link:
					return D3IntegrationHelper.OpenSalesDocument(Response, "IANVK", "11", quote.QuoteNo);
				//move to Infor Extension
				// case ObjectLinkIntegration.InforObjectLink:
				// 	var applicationId = quote.ApplicationId ?? "10000001";
				// 	var filterCondition = quote.FilterCondition ?? "DbSatz_ANr";
				//
				// 	return SalesDocumentFile(applicationId, filterCondition, quote.QuoteNo);
				case ObjectLinkIntegration.SapLink:
					return SapIntegrationHelper.OpenSalesDocument(SapDocumentType.Quote, quote.LegacyId, erpSystemName, erpSystemId);
				default:
					return Content("No ObjectLinkIntegration provider specified in the web.config file");
			}
		}

		[RequiredPermission(ErpPlugin.PermissionName.DocumentSummary, Group = PermissionGroup.Erp)]
		public virtual ActionResult OpenSalesOrder(Guid id)
		{
			var salesOrder = salesOrderRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (salesOrder == null)
			{
				return new NotFoundResult();
			}
			var erpSystemName = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemName);
			var erpSystemId = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemID);
			switch (GetIntegrationType())
			{
				case ObjectLinkIntegration.D3Link:
					return D3IntegrationHelper.OpenSalesDocument(Response, "IABVK", "12", salesOrder.OrderNo);
				//move to Infor Extension
				// case ObjectLinkIntegration.InforObjectLink:
				// 	var applicationId = salesOrder.ApplicationId ?? "10000002";
				// 	var filterCondition = salesOrder.FilterCondition ?? "DbSatz_ANr";
				//
				// 	return SalesDocumentFile(applicationId, filterCondition, salesOrder.OrderNo);
				case ObjectLinkIntegration.SapLink:
					return SapIntegrationHelper.OpenSalesDocument(SapDocumentType.SalesOrder, salesOrder.LegacyId, erpSystemName, erpSystemId);
				default:
					return Content("No ObjectLinkIntegration provider specified in the web.config file");
			}
		}
		[RequiredPermission(ErpPlugin.PermissionName.DocumentSummary, Group = PermissionGroup.Erp)]
		public virtual ActionResult OpenInvoice(Guid id)
		{
			var invoice = invoiceRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (invoice == null)
			{
				return new NotFoundResult();
			}
			var erpSystemName = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemName);
			var erpSystemId = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemID);
			switch (GetIntegrationType())
			{
				case ObjectLinkIntegration.D3Link:
					return D3IntegrationHelper.OpenSalesDocument(Response, "IRGVK", "14", invoice.InvoiceNo);
				//move to Infor Extension
				// case ObjectLinkIntegration.InforObjectLink:
				// 	var applicationId = invoice.ApplicationId ?? "10000005";
				// 	var filterCondition = invoice.FilterCondition ?? "BelegNrRech";
				//
				// 	return SalesDocumentFile(applicationId, filterCondition, invoice.InvoiceNo);
				case ObjectLinkIntegration.SapLink:
					return SapIntegrationHelper.OpenSalesDocument(SapDocumentType.Invoice, invoice.LegacyId, erpSystemName, erpSystemId);
				default:
					return Content("No ObjectLinkIntegration provider specified in the web.config file");
			}
		}

		[RequiredPermission(ErpPlugin.PermissionName.DocumentSummary, Group = PermissionGroup.Erp)]
		public virtual ActionResult OpenCreditNote(Guid id)
		{
			var creditNote = creditNoteRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (creditNote == null)
			{
				return new NotFoundResult();
			}

			if (GetIntegrationType() == ObjectLinkIntegration.D3Link)
			{
				return D3IntegrationHelper.OpenSalesDocument(Response, "IRGVK", "14", creditNote.OrderNo);
			}
			//move to Infor Extension
			// if (GetIntegrationType() == ObjectLinkIntegration.InforObjectLink)
			// {
			// 	var applicationId = creditNote.ApplicationId ?? "10000006";
			// 	var filterCondition = creditNote.FilterCondition ?? "BelegNrRech";
			//
			// 	return SalesDocumentFile(applicationId, filterCondition, creditNote.CreditNoteNo);
			// }

			return Content("No ObjectLinkIntegration provider specified in the web.config file");
		}

		[RequiredPermission(ErpPlugin.PermissionName.DocumentSummary, Group = PermissionGroup.Erp)]
		public virtual ActionResult OpenMasterContract(Guid id)
		{
			var masterContract = masterContractRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (masterContract == null)
			{
				return new NotFoundResult();
			}
			//TODO not sure what's needed for mastercontracts
			//if (IntegrationType == ObjectLinkIntegration.D3Link)
			//{
			//	return D3IntegrationHelper.OpenSalesDocument(Response, "IRGVK", "14", inforId);
			//}
			if (GetIntegrationType() == ObjectLinkIntegration.InforObjectLink)
			{
				return SalesDocumentFile("10000003", "ANr", masterContract.LegacyId);
			}

			return Content("No ObjectLinkIntegration provider specified in the web.config file");
		}

		[RequiredPermission(ErpPlugin.PermissionName.DocumentSummary, Group = PermissionGroup.Erp)]
		public virtual ActionResult OpenDeliveryNote(Guid id)
		{
			var deliveryNote = deliveryNoteRepository.GetAll().FirstOrDefault(x => x.Id == id);
			if (deliveryNote == null)
			{
				return new NotFoundResult();
			}
			var erpSystemName = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemName);
			var erpSystemId = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemID);
			switch (GetIntegrationType())
			{
				case ObjectLinkIntegration.D3Link:
					return D3IntegrationHelper.OpenSalesDocument(Response, "ILSVK", "13", deliveryNote.DeliveryNoteNo);
				//move to Infor Extension
				// case ObjectLinkIntegration.InforObjectLink:
				// 	var applicationId = deliveryNote.ApplicationId ?? "10000004";
				// 	var filterCondition = deliveryNote.FilterCondition ?? "BelegNrLief";
				//
				// 	return SalesDocumentFile(applicationId, filterCondition, deliveryNote.DeliveryNoteNo);
				case ObjectLinkIntegration.SapLink:
					return SapIntegrationHelper.OpenSalesDocument(SapDocumentType.DeliveryNote, deliveryNote.LegacyId, erpSystemName, erpSystemId);
				default:
					return Content("No ObjectLinkIntegration provider specified in the web.config file");
			}
		}

		[RequiredPermission(ErpPlugin.PermissionName.DocumentSummary, Group = PermissionGroup.Erp)]
		protected virtual FileContentResult SalesDocumentFile(string applicationId, string fieldNameInforId, string inforId, string viewName = "viwVertrieb")
		{
			var contents = new StringBuilder();
			var erpSystemID = appSettingsProvider.GetValue(ErpPlugin.Settings.System.ErpSystemID);
			contents.AppendLine("[ILM]");
			contents.AppendLine(String.Format("SystemID={0}", erpSystemID));
			contents.AppendLine(String.Format("AppID={0}", applicationId));
			contents.AppendLine(String.Format("FilterCond=([{0}]='{1}')", fieldNameInforId, inforId));
			contents.AppendLine(String.Format("FilterView={0}", viewName));

			return new FileContentResult(Encoding.Default.GetBytes(contents.ToString()), "application/infor")
			{
				FileDownloadName = String.Format("{0}.iol", inforId)
			};
		}
	}
}
