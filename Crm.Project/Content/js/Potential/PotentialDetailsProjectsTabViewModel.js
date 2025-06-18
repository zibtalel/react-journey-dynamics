/// <reference path="ProjectListIndexViewModel.js" />
window.Helper.Database.addIndex("CrmProject_Project", ["StatusKey"]);
namespace("Crm.Project.ViewModels").PotentialDetailsProjectsTabViewModel = function (parentViewModel) {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	var joinTags = {
		Selector: "Tags",
		Operation: "orderBy(function(t) { return t.Name; })"
	};
	window.Main.ViewModels.GenericListViewModel.call(viewModel, "CrmProject_Project", ["StatusKey", "DueDate"], ["DESC", "DESC"], ["ResponsibleUserUser", joinTags]);
	var potentialId = parentViewModel.potential().Id();
	viewModel.potentialId = window.ko.observable(potentialId);
	viewModel.getFilter("PotentialId").extend({ filterOperator: "===" })(potentialId);
	viewModel.projects = viewModel.items;
	viewModel.projects.distinct("StatusKey");
	viewModel.lookups = {
		projectStatuses: { $tableName: "CrmProject_ProjectStatus" },
		currencies: { $tableName: "Main_Currency" },
		projectCategories: { $tableName: "CrmProject_ProjectCategory" }
	};
}
namespace("Crm.Project.ViewModels").PotentialDetailsProjectsTabViewModel.prototype = Object.create(window.Main.ViewModels.GenericListViewModel.prototype);
namespace("Crm.Project.ViewModels").PotentialDetailsProjectsTabViewModel.prototype.init = function (parentViewModel) {
	var viewModel = this;
	var args = arguments;
	return window.Helper.User.getCurrentUser()
		.then(function () { return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups); })
		.then(function () { return window.Main.ViewModels.GenericListViewModel.prototype.init.apply(viewModel, args); });
}