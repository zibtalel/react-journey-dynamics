/// <reference path="..\..\..\..\Content\js\ViewModels\GenericListViewModel.js" />
/// <reference path="..\..\..\..\Content\js\ViewModels\CompanyListIndexViewModel.js" />
namespace("Crm.ErpExtension.ViewModels").ErpDocumentListViewModel = function (entityType, orderBy, orderByDirection, joins) {
	var viewModel = this;
	viewModel.entityType = entityType;
	viewModel.lookups = {
		currencies: { $tableName: "Main_Currency" },
		documentStatuses: { $tableName: "CrmErpExtension_ErpDocumentStatus" }
	};
	window.Main.ViewModels.GenericListViewModel.call(this, entityType, orderBy, orderByDirection, joins);
};
namespace("Crm.ErpExtension.ViewModels").ErpDocumentListViewModel.prototype = Object.create(window.Main.ViewModels.GenericListViewModel.prototype);
namespace("Crm.ErpExtension.ViewModels").ErpDocumentListViewModel.prototype.init = function () {
	var viewModel = this;
	return window.Main.ViewModels.GenericListViewModel.prototype.init.apply(viewModel, arguments).then(function() {
		return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups);
	}).then(function() {
		viewModel.documentStatuses = viewModel.lookups.documentStatuses;
		viewModel.currencies = viewModel.lookups.currencies;
	});
};
namespace("Crm.ErpExtension.ViewModels").ErpDocumentListViewModel.prototype.showContactLink = function(item) {
	return ko.unwrap(item.ContactType) && ko.unwrap(item.ContactKey);
};
namespace("Crm.ErpExtension.ViewModels").ErpDocumentListViewModel.prototype.getCompanyText = function(item) {
	return [ko.unwrap(item.CompanyNo), ko.unwrap(item.CompanyName)].filter(Boolean).join(" - ");
};
