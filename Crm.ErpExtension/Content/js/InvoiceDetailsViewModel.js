namespace("Crm.ErpExtension.ViewModels").InvoiceDetailsViewModel = function() {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsViewModel.apply(this, ["Invoice"]);
	
}
namespace("Crm.ErpExtension.ViewModels").InvoiceDetailsViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsViewModel.prototype);
