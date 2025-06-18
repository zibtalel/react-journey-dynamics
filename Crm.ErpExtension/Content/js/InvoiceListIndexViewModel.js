namespace("Crm.ErpExtension.ViewModels").InvoiceListIndexViewModel = function () {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.call(this, "CrmErpExtension_Invoice", ["OrderNo", "ModifyDate"], ["DESC", "DESC"]);
};
namespace("Crm.ErpExtension.ViewModels").InvoiceListIndexViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.prototype);
