(function() {
	var baseOrderDetailsCalculationTabViewModel = window.Crm.Order.ViewModels.BaseOrderDetailsCalculationTabViewModel;
	window.Crm.Order.ViewModels.BaseOrderDetailsCalculationTabViewModel = function (parentViewModel) {
		var viewModel = this;
		baseOrderDetailsCalculationTabViewModel.apply(viewModel, arguments);
		if (parentViewModel.baseOrder().OrderEntryType() !== "Configuration") {
		  return;
		}
		viewModel.defaultVariableValues = window.ko.observableArray([]);
		viewModel.calculationParameters()[0].Name = window.Helper.String.getTranslatedString("StandardConfiguration");
		viewModel.calculationParameters.push({
		  Amount: window.ko.observable(null),
		  AmountSales: window.ko.observable(null),
		  Name: window.Helper.String.getTranslatedString("RemovedStandardOptions"),
		  Calculation: function (purchasePrice, salesPrice) {
		    viewModel.defaultVariableValues().forEach(function (defaultVariableValue) {
		      var wasRemoved = window.ko.utils.arrayFirst(viewModel.parentViewModel.orderItems(), function (orderItem) { return orderItem.ArticleId() === defaultVariableValue.ChildId; }) === null;
		      if (wasRemoved) {
		        purchasePrice -= defaultVariableValue.ExtensionValues.PurchasePrice || defaultVariableValue.Child.PurchasePrice;
		        salesPrice -= defaultVariableValue.ExtensionValues.SalesPrice || defaultVariableValue.Child.Price;
		      }
		    });
		    return {
		      Amount: purchasePrice,
		      AmountSales: salesPrice
		    };
		  },
		  Details: window.ko.pureComputed(function () {
		    var details = [];
		    viewModel.defaultVariableValues().forEach(function (defaultVariableValue) {
		      var wasRemoved = window.ko.utils.arrayFirst(viewModel.parentViewModel.orderItems(), function (orderItem) { return orderItem.ArticleId() === defaultVariableValue.ChildId; }) === null;
		      if (wasRemoved) {
		        details.push({
		          Name: window.ko.unwrap(defaultVariableValue.Child.ItemNo) + " - " + window.ko.unwrap(defaultVariableValue.Child.Description),
		          Amount: -defaultVariableValue.ExtensionValues.PurchasePrice || -defaultVariableValue.Child.PurchasePrice,
		          AmountSales: -defaultVariableValue.ExtensionValues.SalesPrice || -defaultVariableValue.Child.Price
		        });
		      }
		    });
		    return details;
		  }),
		  ShowDiff: true
		});
		viewModel.calculationParameters.push({
		  Amount: window.ko.observable(null),
		  AmountSales: window.ko.observable(null),
		  Name: window.Helper.String.getTranslatedString("Subtotal"),
		  Calculation: function (purchasePrice, salesPrice) {
		    return {
		      Amount: purchasePrice,
		      AmountSales: salesPrice
		    };
		  },
		  Details: window.ko.observableArray([]),
		  ShowDiff: false
		});
		viewModel.calculationParameters.push({
		  Amount: window.ko.observable(null),
		  AmountSales: window.ko.observable(null),
		  Name: window.Helper.String.getTranslatedString("AdditionalOptions"),
		  Calculation: function (purchasePrice, salesPrice) {
		    viewModel.parentViewModel.orderItems().forEach(function (orderItem) {
		      var wasAdded = window.ko.utils.arrayFirst(viewModel.defaultVariableValues(), function (variableValue) { return variableValue.ChildId === orderItem.ArticleId(); }) === null;
		      if (wasAdded && !orderItem.IsAlternative() && !orderItem.IsOption()) {
		        purchasePrice += orderItem.PurchasePrice();
		        salesPrice += orderItem.Price();
		      }
		    });
		    return {
		      Amount: purchasePrice,
		      AmountSales: salesPrice
		    };
		  },
		  Details: window.ko.pureComputed(function () {
		    var details = [];
		    viewModel.parentViewModel.orderItems().forEach(function (orderItem) {
		      var wasAdded = window.ko.utils.arrayFirst(viewModel.defaultVariableValues(), function (variableValue) { return variableValue.ChildId === orderItem.ArticleId(); }) === null;
		      if (wasAdded && !orderItem.IsAlternative() && !orderItem.IsOption()) {
		        details.push({
		          Name: window.ko.unwrap(orderItem.ArticleNo) + " - " + window.ko.unwrap(orderItem.ArticleDescription),
		          Amount: orderItem.PurchasePrice,
		          AmountSales: orderItem.Price
		        });
		      }
		    });
		    return details;
		  }),
		  ShowDiff: true
		});
	}
	window.Crm.Order.ViewModels.BaseOrderDetailsCalculationTabViewModel.prototype = baseOrderDetailsCalculationTabViewModel.prototype;

	var baseOrderDetailsCalculationTabViewModelInit = window.Crm.Order.ViewModels.BaseOrderDetailsCalculationTabViewModel.prototype.init;
	window.Crm.Order.ViewModels.BaseOrderDetailsCalculationTabViewModel.prototype.init = function(parentViewModel) {
	  var viewModel = this;
	  var args = arguments;
		if (parentViewModel.baseOrder().OrderEntryType() !== "Configuration") {
			return baseOrderDetailsCalculationTabViewModelInit.apply(viewModel, args);
		}
		viewModel.basePrice(parentViewModel.configurationBase().PurchasePrice() || parentViewModel.configurationBase().Price() || 0);
		viewModel.baseSalesPrice(parentViewModel.configurationBase().Price() || 0);
		return viewModel.parentViewModel.getDefaultVariableValues()
			.pipe(function(defaultVariableValues) {
				viewModel.defaultVariableValues(defaultVariableValues);
				return baseOrderDetailsCalculationTabViewModelInit.apply(viewModel, args);
			});
	}
})();