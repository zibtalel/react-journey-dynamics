$(function () {
	var baseViewModel = namespace("Crm.Order.ViewModels").OrderLoadModalViewModel;
	namespace("Crm.Order.ViewModels").OrderLoadModalViewModel = function(parentViewModel) {
		var viewModel = this;
		baseViewModel.apply(viewModel, arguments);
	}
	namespace("Crm.Order.ViewModels").OrderLoadModalViewModel.prototype = baseViewModel.prototype;
	namespace("Crm.Order.ViewModels").OrderLoadModalViewModel.prototype.initItems = function (items) {
		return Helper.ProjectOrder.getProjects(items);
	}
});