namespace("Crm.ErpExtension.ViewModels").QuoteDetailsViewModel = function() {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsViewModel.apply(this, ["Quote"]);
	
}
namespace("Crm.ErpExtension.ViewModels").QuoteDetailsViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsViewModel.prototype);
