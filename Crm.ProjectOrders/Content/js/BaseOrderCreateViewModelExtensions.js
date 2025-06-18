;
(function() {
	var baseViewModel = namespace("Crm.Order.ViewModels").BaseOrderCreateViewModel;
	namespace("Crm.Order.ViewModels").BaseOrderCreateViewModel = function () {
		var viewModel = this;
		baseViewModel.apply(viewModel, arguments);
		viewModel.onProjectSelect = function (project) {
			if (project) {
				viewModel.baseOrder().ContactId(project.ParentId);
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
	namespace("Crm.Order.ViewModels").BaseOrderCreateViewModel.prototype = baseViewModel.prototype;

	const baseInit = baseViewModel.prototype.init;
	namespace("Crm.Order.ViewModels").BaseOrderCreateViewModel.prototype.init = async function (id, params) {
		const viewModel = this;
		await baseInit.apply(viewModel, arguments);
		let oldContactId = viewModel.baseOrder().ContactId();
		viewModel.baseOrder().ExtensionValues().ProjectId(params.projectId);
		viewModel.baseOrder().ContactId.subscribe(function(value) {
			if (oldContactId) {
				viewModel.baseOrder().ExtensionValues().ProjectId(null);
			}
			oldContactId = value;
		});
	}
})();