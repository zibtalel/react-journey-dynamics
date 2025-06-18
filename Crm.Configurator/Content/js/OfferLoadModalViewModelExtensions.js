;(function () {
	var baseOfferLoadModalViewModel = namespace("Crm.Order.ViewModels").OfferLoadModalViewModel;
	namespace("Crm.Order.ViewModels").OfferLoadModalViewModel = function (parentViewModel) {
		var viewModel = this;
		baseOfferLoadModalViewModel.apply(viewModel, arguments);
		if (!!parentViewModel.configurationBase()) {
			var configurationBaseId = parentViewModel.configurationBase().Id();
			viewModel.getFilter("ExtensionValues.ConfigurationBaseId").extend({ filterOperator: "===" })(configurationBaseId);
		}
	}
	namespace("Crm.Order.ViewModels").OfferLoadModalViewModel.prototype = baseOfferLoadModalViewModel.prototype;
})();