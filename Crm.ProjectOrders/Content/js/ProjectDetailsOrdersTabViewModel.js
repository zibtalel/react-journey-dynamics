namespace("Crm.Project.ViewModels").ProjectDetailsOrdersTabViewModel = function(parentViewModel) {
	var viewModel = this;
	viewModel.project = parentViewModel.project;
	viewModel.loading = window.ko.observable(true);
	window.Crm.Order.ViewModels.OrderListIndexViewModel.call(viewModel, "CrmOrder_Order", "Id", "DESC");
	var projectId = parentViewModel.project().Id();
	viewModel.getFilter("ExtensionValues.ProjectId").extend({ filterOperator: "===" })(projectId);
}
namespace("Crm.Project.ViewModels").ProjectDetailsOrdersTabViewModel.prototype = Object.create(window.Crm.Order.ViewModels.OrderListIndexViewModel.prototype);