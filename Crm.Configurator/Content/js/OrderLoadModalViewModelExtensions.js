;(function () {
	var baseOrderLoadModalViewModel = namespace("Crm.Order.ViewModels").OrderLoadModalViewModel;
	namespace("Crm.Order.ViewModels").OrderLoadModalViewModel = function (parentViewModel) {
		var viewModel = this;
		baseOrderLoadModalViewModel.apply(viewModel, arguments);
		if (!!parentViewModel.configurationBase()) {
			var configurationBaseId = parentViewModel.configurationBase().Id();
			viewModel.getFilter("ExtensionValues.ConfigurationBaseId").extend({ filterOperator: "===" })(configurationBaseId);
		}
	}
	namespace("Crm.Order.ViewModels").OrderLoadModalViewModel.prototype = baseOrderLoadModalViewModel.prototype;
})();