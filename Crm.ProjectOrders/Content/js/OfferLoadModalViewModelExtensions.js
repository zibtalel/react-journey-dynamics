$(function () {
	var baseViewModel = namespace("Crm.Order.ViewModels").OfferLoadModalViewModel;
	namespace("Crm.Order.ViewModels").OfferLoadModalViewModel = function(parentViewModel) {
		var viewModel = this;
		baseViewModel.apply(viewModel, arguments);
	}
	namespace("Crm.Order.ViewModels").OfferLoadModalViewModel.prototype = baseViewModel.prototype;
	namespace("Crm.Order.ViewModels").OfferLoadModalViewModel.prototype.initItems = function (items) {
		return Helper.ProjectOrder.getProjects(items);
	}
});
