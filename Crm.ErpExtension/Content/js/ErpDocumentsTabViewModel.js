namespace("Crm.ErpExtension.ViewModels").ErpDocumentsTabViewModel = function(parentViewModel) {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentMultiListBaseViewModel.apply(viewModel, arguments);
	viewModel.contactId = window.ko.observable(null);
	viewModel.displayedStatuses = window.ko.observableArray(["Open"]);
	viewModel.hasErpDocuments = window.ko.observable(false);
};
namespace("Crm.ErpExtension.ViewModels").ErpDocumentsTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentMultiListBaseViewModel.prototype);
namespace("Crm.ErpExtension.ViewModels").ErpDocumentsTabViewModel.prototype.afterInit = function (d) {
	var viewModel = this;
	window.async.eachSeries(viewModel.documentTypes(), function(documentType, cb) {
		if (viewModel.hasErpDocuments()) {
			cb();
			return;
		}
		window.database[documentType.table]
			.filter(function(x) { return x.ContactKey === this.contactId; }, { contactId: viewModel.contactId() })
			.count(function(count) {
				viewModel.hasErpDocuments(count > 0);
				cb();
			}).fail(cb);
	}, function(err2) {
		if (err2) {
			d.reject(err2);
		} else {
			d.resolve();
		}
	});
}
namespace("Crm.ErpExtension.ViewModels").ErpDocumentsTabViewModel.prototype.initListViewModel = function (documentViewModel, documentType) {
	var viewModel = this;
	documentViewModel.caption = window.ko.observable(window.Helper.String.getTranslatedString("Open" + documentType.model + "Caption"));
	documentViewModel.emptyStateText = window.ko.observable(window.Helper.String.getTranslatedString("Open" + documentType.model + "EmptyStateText"));
	documentViewModel.showAllLink = "#/Crm.ErpExtension/" + documentType.model + "List/IndexTemplate?filters=" + encodeURIComponent(JSON.stringify({ ContactKey: { Operator: "==", Value: viewModel.contactId() } }));
	documentViewModel.showAllText = window.ko.observable(window.Helper.String.getTranslatedString("ShowAll" + documentType.model));
	documentViewModel.pageSize(10);
	documentViewModel.getFilter("ContactKey").extend({ filterOperator: "==" })(viewModel.contactId());
	documentViewModel.getFilter("StatusKey").extend({ filterOperator: "in" })(viewModel.displayedStatuses());
}
