namespace("Crm.ErpExtension.ViewModels").MasterContractListIndexViewModel = function () {
	var viewModel = this;
	window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.call(this, "CrmErpExtension_MasterContract", ["OrderNo", "ModifyDate"], ["DESC", "DESC"]);
};
namespace("Crm.ErpExtension.ViewModels").MasterContractListIndexViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.prototype);
