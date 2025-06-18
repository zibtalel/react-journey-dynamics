namespace("Crm.ErpExtension.ViewModels").CreditNoteDetailsViewModel = function() {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsViewModel.apply(this, ["CreditNote"]);
	viewModel.InvoiceId = ko.observable();
	
}
namespace("Crm.ErpExtension.ViewModels").CreditNoteDetailsViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsViewModel.prototype);
namespace("Crm.ErpExtension.ViewModels").CreditNoteDetailsViewModel.prototype.init = function(id) {
	var viewModel = this;
	return window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsViewModel.prototype.init.apply(viewModel, arguments)
		.then(function () {
			if (viewModel.ErpDocument().InvoiceNo()) {
				return window.database.CrmErpExtension_Invoice
					.single("it.InvoiceNo == '" + viewModel.ErpDocument().InvoiceNo() + "'")
					.then(function (invoice) {
						viewModel.InvoiceId(invoice.Id);
					});
			}
		});
}
