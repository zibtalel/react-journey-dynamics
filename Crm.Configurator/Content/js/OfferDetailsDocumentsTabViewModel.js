namespace("Crm.Order.ViewModels").OfferDetailsDocumentsTabViewModel = function(parentViewModel) {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	window.Main.ViewModels.DocumentAttributeListIndexViewModel.call(viewModel, arguments);
	var configurationBaseId = parentViewModel.configurationBase().Id();
	viewModel.getFilter("ReferenceKey").extend({ filterOperator: "===" })(configurationBaseId);
}
namespace("Crm.Order.ViewModels").OfferDetailsDocumentsTabViewModel.prototype = Object.create(window.Main.ViewModels.DocumentAttributeListIndexViewModel.prototype);