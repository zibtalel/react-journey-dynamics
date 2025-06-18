namespace("Crm.ErpExtension.ViewModels").ErpDocumentMultiListBaseViewModel = function(parentViewModel) {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	viewModel.documentViewModels = window.ko.observableArray([]);
	viewModel.documentTypes = window.ko.observableArray([]);
	viewModel.lookups = {
		currencies: { $tableName: "Main_Currency" },
		documentStatuses: { $tableName: "CrmErpExtension_ErpDocumentStatus" }
	};
};
namespace("Crm.ErpExtension.ViewModels").ErpDocumentMultiListBaseViewModel.prototype = Object.create(window.Main.ViewModels.ViewModelBase.prototype);
namespace("Crm.ErpExtension.ViewModels").ErpDocumentMultiListBaseViewModel.prototype.init = function (parentViewModel) {
	var viewModel = this;
	if (window.database.CrmErpExtension_Quote) {
		viewModel.documentTypes.push({
			table: "CrmErpExtension_Quote",
			plugin: "Crm.ErpExtension",
			model: "Quote"
		});
	}
	if (window.database.CrmErpExtension_SalesOrder) {
		viewModel.documentTypes.push({
			table: "CrmErpExtension_SalesOrder",
			plugin: "Crm.ErpExtension",
			model: "SalesOrder"
		});
	}
	if (window.database.CrmErpExtension_DeliveryNote) {
		viewModel.documentTypes.push({
			table: "CrmErpExtension_DeliveryNote",
			plugin: "Crm.ErpExtension",
			model: "DeliveryNote"
		});
	}
	if (window.database.CrmErpExtension_Invoice) {
		viewModel.documentTypes.push({
			table: "CrmErpExtension_Invoice",
			plugin: "Crm.ErpExtension",
			model: "Invoice"
		});
	}
	if (window.database.CrmErpExtension_CreditNote) {
		viewModel.documentTypes.push({
			table: "CrmErpExtension_CreditNote",
			plugin: "Crm.ErpExtension",
			model: "CreditNote"
		});
	}
	if (window.database.CrmErpExtension_MasterContract) {
		viewModel.documentTypes.push({
			table: "CrmErpExtension_MasterContract",
			plugin: "Crm.ErpExtension",
			model: "MasterContract"
		});
	}
	var d = new $.Deferred();
	window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups)
		.then(function () {
		viewModel.documentTypes().forEach(function (documentType) {
			var documentViewModel = new window.Main.ViewModels.GenericListViewModel(documentType.table, ["OrderNo", "ModifyDate"], ["DESC", "DESC"]);
			documentViewModel.currencies = viewModel.lookups.currencies;
			documentViewModel.documentStatuses = viewModel.lookups.documentStatuses;
			viewModel.initListViewModel && viewModel.initListViewModel.call(viewModel, documentViewModel, documentType);
			viewModel.documentViewModels.push(documentViewModel);
		});
		window.async.eachSeries(viewModel.documentViewModels(), function(documentViewModel, cb) {
			documentViewModel.init().then(cb).fail(cb);
		}, function(err) {
			if (err) {
				d.reject(err);
			} else {
				if (viewModel.afterInit){
					viewModel.afterInit.call(viewModel, d);
				}
				else {
					d.resolve()
				}
			}
		});
	});
	return d.promise();
};