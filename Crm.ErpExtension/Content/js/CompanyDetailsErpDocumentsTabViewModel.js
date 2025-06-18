namespace("Main.ViewModels").CompanyDetailsErpDocumentsTabViewModel = function(parentViewModel) {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentsTabViewModel.apply(this, arguments);
	viewModel.contactId(parentViewModel.company().Id());
};
namespace("Main.ViewModels").CompanyDetailsErpDocumentsTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentsTabViewModel.prototype);