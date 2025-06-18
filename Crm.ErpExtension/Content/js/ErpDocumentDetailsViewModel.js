namespace("Crm.ErpExtension.ViewModels").ErpDocumentDetailsViewModel = function(docType) {
	var viewModel = this;
	viewModel.docType = docType;
	viewModel.tabs = window.ko.observable({});
	viewModel.ErpDocument = ko.observable();
	viewModel.ErpDocumentId = null;
	viewModel.currentUser = ko.observable(null);
	viewModel.currencyKey = ko.observable(null);
	viewModel.lookups = {
		ErpDocumentStatus: { $tableName: "CrmErpExtension_ErpDocumentStatus" },
		ErpPaymentMethod: { $tableName: "CrmErpExtension_ErpPaymentMethod" },
		ErpPaymentTerms: { $tableName: "CrmErpExtension_ErpPaymentTerms" },
		ErpDeliveryMethod: { $tableName: "CrmErpExtension_ErpDeliveryMethod" },
		ErpTermsOfDelivery: { $tableName: "CrmErpExtension_ErpTermsOfDelivery" },
		currencies: { $tableName: "Main_Currency" },
		quantityUnits: { $tableName: "CrmArticle_QuantityUnit" },
	};
}
namespace("Crm.ErpExtension.ViewModels").ErpDocumentDetailsViewModel.prototype = Object.create(window.Main.ViewModels.ViewModelBase.prototype);
namespace("Crm.ErpExtension.ViewModels").ErpDocumentDetailsViewModel.prototype.init = function(id) {
	var viewModel = this;
	viewModel.ErpDocumentId = id;
	return window.database["CrmErpExtension_" + viewModel.docType]
		.include("Positions")
		.include("Positions.Article")
		.find(viewModel.ErpDocumentId)
		.then(function(doc) {
			viewModel.ErpDocument(doc.asKoObservable());
			viewModel.currencyKey(viewModel.ErpDocument().CurrencyKey())
		})
		.then(function () { return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups); })
		.then(function () {
			return window.Helper.User.getCurrentUser()	
		})
		.then(function (user) {
			viewModel.currentUser(user)
		})
		.then(function() {
			if (id) {
				return viewModel.setBreadcrumbs();
			}
		})
		
}

namespace("Crm.ErpExtension.ViewModels").ErpDocumentDetailsViewModel.prototype.showContactLink = function(item) {
	return ko.unwrap(item.ContactType) && ko.unwrap(item.ContactKey);
};
namespace("Crm.ErpExtension.ViewModels").ErpDocumentDetailsViewModel.prototype.getCompanyText = function(item) {
	return [ko.unwrap(item.CompanyNo), ko.unwrap(item.CompanyName)].filter(Boolean).join(" - ");
};


namespace("Crm.ErpExtension.ViewModels").ErpDocumentDetailsViewModel.prototype.setBreadcrumbs = function () {
	var viewModel = this;
	return window.breadcrumbsViewModel.setBreadcrumbs([
		new Breadcrumb(Helper.String.getTranslatedString(viewModel.docType), "#/Crm.ErpExtension/" + viewModel.docType + "List/IndexTemplate"),
		new Breadcrumb(window.Helper.ErpDocument.getDocumentNo(viewModel.ErpDocument), window.location.hash)
	]);
};
