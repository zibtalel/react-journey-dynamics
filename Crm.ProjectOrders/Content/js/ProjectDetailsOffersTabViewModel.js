namespace("Crm.Project.ViewModels").ProjectDetailsOffersTabViewModel = function(parentViewModel) {
	var viewModel = this;
	viewModel.project = parentViewModel.project;
	viewModel.loading = window.ko.observable(true);
	window.Crm.Order.ViewModels.OfferListIndexViewModel.call(viewModel, "CrmOrder_Offer", "Id", "DESC");
	var projectId = parentViewModel.project().Id();
	viewModel.getFilter("ExtensionValues.ProjectId").extend({ filterOperator: "===" })(projectId);
}
namespace("Crm.Project.ViewModels").ProjectDetailsOffersTabViewModel.prototype = Object.create(window.Crm.Order.ViewModels.OfferListIndexViewModel.prototype);
namespace("Crm.Project.ViewModels").ProjectDetailsOffersTabViewModel.prototype.setAsPreferredOffer = function(offer) {
	var viewModel = this;
	if (viewModel.project().ExtensionValues().PreferredOfferId(offer.Id())) {
		return;
	}
	viewModel.loading(true);
	window.database.attachOrGet(viewModel.project().innerInstance);
	viewModel.project().ExtensionValues().PreferredOfferId(offer.Id());
	viewModel.project().Value(offer.Price());
	viewModel.project().CurrencyKey(offer.CurrencyKey());
	window.database.saveChanges().then(function() {
		viewModel.loading(false);
	});
}