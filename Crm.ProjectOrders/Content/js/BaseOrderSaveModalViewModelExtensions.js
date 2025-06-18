$(function () {
	var baseViewModel = namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel;
	namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel = function (parentViewModel) {
		var viewModel = this;
		baseViewModel.apply(viewModel, arguments);
		viewModel.projectId = window.ko.observable(parentViewModel.project() ? parentViewModel.project().Id() : null);
		viewModel.onProjectSelect = function (project) {
			if (project) {
				viewModel.baseOrder().ExtensionValues().ProjectId(project.Id);
				viewModel.baseOrder().ContactId(project.ParentId);
			} else {
				viewModel.baseOrder().ExtensionValues().ProjectId(null);
			}
		};
		viewModel.filterByCompany = function (query, term) {
			if (term) {
				query = query.filter('it.Name.toLowerCase().contains(this.term)', { term: term });
			}
			if (!viewModel.baseOrder()) {
				return query;
			}
			var contactId = viewModel.baseOrder().ContactId();
			if (!!contactId) {
				return query.filter("it.ParentId == this.contactId", { contactId: contactId });
			}
			return query;
		}
	}
	namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel.prototype = baseViewModel.prototype;

	var baseInit = baseViewModel.prototype.init;
	namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel.prototype.init = function(id, params) {
		var viewModel = this;
		return baseInit.apply(viewModel, arguments)
			.pipe(function() {
				var oldContactId = viewModel.baseOrder().ContactId();
				viewModel.baseOrder().ContactId.subscribe(function(value) {
					if (oldContactId) {
						viewModel.projectId(null);
						viewModel.baseOrder().ExtensionValues().ProjectId(null);
					}
					oldContactId = value;
				});
			});
	}
});