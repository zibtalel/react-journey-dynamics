namespace("Crm.ErpExtension.ViewModels").ErpDocumentDetailsOrderTabViewModel = function(parentVm) {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentMultiListBaseViewModel.apply(viewModel, arguments);
	viewModel.OrderNo = ko.observable(parentVm.ErpDocument().OrderNo())
}
namespace("Crm.ErpExtension.ViewModels").ErpDocumentDetailsOrderTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentMultiListBaseViewModel.prototype);

namespace("Crm.ErpExtension.ViewModels").ErpDocumentDetailsOrderTabViewModel.prototype.initListViewModel = function(documentViewModel, documentType) {
	var viewModel = this;
	documentViewModel.caption = window.ko.observable(window.Helper.String.getTranslatedString(documentType.model + "s"));
	documentViewModel.pageSize(10);
	if (viewModel.OrderNo() !== null && viewModel.OrderNo() !== ""){
		documentViewModel.getFilter("OrderNo").extend({ filterOperator: "==" })(viewModel.OrderNo());
	}
	else {
		documentViewModel.getFilter("OrderNo").extend({ filterOperator: "===" })(0);
	}
}

namespace("Crm.ErpExtension.ViewModels").InvoiceDetailsOrderTabViewModel = window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel
namespace("Crm.ErpExtension.ViewModels").InvoiceDetailsOrderTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel.prototype);

namespace("Crm.ErpExtension.ViewModels").CreditNoteDetailsOrderTabViewModel = window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel
namespace("Crm.ErpExtension.ViewModels").CreditNoteDetailsOrderTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel.prototype);

namespace("Crm.ErpExtension.ViewModels").SalesOrderDetailsOrderTabViewModel = window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel
namespace("Crm.ErpExtension.ViewModels").SalesOrderDetailsOrderTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel.prototype);

namespace("Crm.ErpExtension.ViewModels").QuoteDetailsOrderTabViewModel = window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel
namespace("Crm.ErpExtension.ViewModels").QuoteDetailsOrderTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel.prototype);

namespace("Crm.ErpExtension.ViewModels").MasterContractDetailsOrderTabViewModel = window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel
namespace("Crm.ErpExtension.ViewModels").MasterContractDetailsOrderTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel.prototype);

namespace("Crm.ErpExtension.ViewModels").DeliveryNoteDetailsOrderTabViewModel = window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel
namespace("Crm.ErpExtension.ViewModels").DeliveryNoteDetailsOrderTabViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsOrderTabViewModel.prototype);