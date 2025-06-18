;(function () {
	var baseInit = namespace("Crm.Order.ViewModels").OfferCreateOrderModalViewModel.prototype.init;
	namespace("Crm.Order.ViewModels").OfferCreateOrderModalViewModel.prototype.init = function() {
		var viewModel = this;
		return baseInit.apply(this, arguments)
			.pipe(function() {
				viewModel.order().ExtensionValues().ConfigurationBaseId(viewModel.offer().ExtensionValues().ConfigurationBaseId());
			});
	}
})();