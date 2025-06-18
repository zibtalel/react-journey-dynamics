;
(function() {
	var baseViewModel = namespace("Crm.Order.ViewModels").OrderDetailsViewModel;
	namespace("Crm.Order.ViewModels").OrderDetailsViewModel = function () {
		var viewModel = this;
		baseViewModel.apply(viewModel, arguments);
		viewModel.project = window.ko.observable(null);
	}
	namespace("Crm.Order.ViewModels").OrderDetailsViewModel.prototype = baseViewModel.prototype;

	var baseInit = baseViewModel.prototype.init;
	namespace("Crm.Order.ViewModels").OrderDetailsViewModel.prototype.init = function (id, params) {
		var viewModel = this;
		var d = new $.Deferred();
		baseInit.apply(viewModel, arguments)
			.pipe(function() {
				viewModel.order().ContactId.subscribe(function (contactId) {
					var projectId = !!viewModel.project() ? viewModel.project().Id() : null;
					if (contactId !== projectId) {
						viewModel.project(null);
						viewModel.order().ExtensionValues().ProjectId(null);
					}
				});
				return null;
			})
			.pipe(d.resolve)
			.fail(d.reject);
		return d.promise();
	}

	var baseRefresh = baseViewModel.prototype.refresh;
	namespace("Crm.Order.ViewModels").OrderDetailsViewModel.prototype.refresh = function() {
		var viewModel = this;
		return baseRefresh.apply(this, arguments).then(function() {
			var projectId = viewModel.order().ExtensionValues().ProjectId();
			if (projectId) {
				return window.database.CrmProject_Project.find(projectId).then(function(result) {
					viewModel.project(result.asKoObservable());
				});
			}
		});
	};
})();