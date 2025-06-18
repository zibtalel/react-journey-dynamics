namespace("Crm.ErpExtension.ViewModels").SalesOrderDetailsViewModel = function() {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsViewModel.apply(this, ["SalesOrder"]);
	
}
namespace("Crm.ErpExtension.ViewModels").SalesOrderDetailsViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentDetailsViewModel.prototype);
