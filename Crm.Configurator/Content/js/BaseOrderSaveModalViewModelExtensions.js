(function() {
	var baseViewModel = namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel;
	namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel = function (parentViewModel) {
		var viewModel = this;
		baseViewModel.apply(viewModel, arguments);
		viewModel.configurationBase = parentViewModel.configurationBase;
	}
	namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel.prototype = baseViewModel.prototype;
	var baseGetDefaultPrivateDescription = namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel.prototype.getDefaultPrivateDescription;
	namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel.prototype.getDefaultPrivateDescription = function () {
		var viewModel = this;
		var result = baseGetDefaultPrivateDescription.apply(viewModel, arguments);
		if (!!viewModel.configurationBase()) {
			result = viewModel.configurationBase().ItemNo() + " / " + result;
		}
		return result;
	}
	namespace("Crm.Order.ViewModels").BaseOrderSaveModalViewModel.prototype.save = function () {
		var viewModel = this;
		viewModel.loading(true);
		if (viewModel.errors().length > 0) {
			viewModel.loading(false);
			viewModel.errors.showAllMessages();
			return;
		}
		viewModel.baseOrder().Price(viewModel.baseOrder().Price() || viewModel.totalPrice());

		return window.NumberingService.createNewNumberBasedOnAppSettings(window.Crm.Order.Settings.Order.OrderNoIsGenerated, window.Crm.Order.Settings.Order.OrderNoIsCreateable, viewModel.baseOrder().OrderNo(), window.Crm.Order.ViewModels.OfferCreateViewModel.prototype.numberingSequence, viewModel.baseOrder().OrderType() === "Offer" ? window.database.CrmOrder_Offer : window.database.CrmOrder_Order, "OrderNo")
			.pipe(function (orderNo) {
				if (orderNo !== undefined) {
					viewModel.baseOrder().OrderNo(orderNo);
				}
			}).pipe(function () {
				window.database.saveChanges().then(function () {
					viewModel.loading(false);
					$(".modal:visible").modal("hide");
				});
			});
	};

})();