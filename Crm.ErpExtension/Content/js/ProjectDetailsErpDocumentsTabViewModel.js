namespace("Crm.Project.ViewModels").ProjectDetailsErpDocumentsTabViewModel = function(parentViewModel) {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentsTabViewModel.apply(this, arguments);
	viewModel.contactId(parentViewModel.project().Id());
};
namespace("Crm.Project.ViewModels").ProjectDetailsErpDocumentsTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentsTabViewModel.prototype);