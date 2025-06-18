namespace("Crm.ErpExtension.ViewModels").SalesOrderListIndexViewModel = function () {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.call(this, "CrmErpExtension_SalesOrder", ["OrderNo", "ModifyDate"], ["DESC", "DESC"]);
};
namespace("Crm.ErpExtension.ViewModels").SalesOrderListIndexViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.prototype);
