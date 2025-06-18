namespace("Crm.ErpExtension.ViewModels").CreditNoteListIndexViewModel = function() {
	window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.call(this, "CrmErpExtension_CreditNote", ["OrderNo", "ModifyDate"], ["DESC", "DESC"]);
};
namespace("Crm.ErpExtension.ViewModels").CreditNoteListIndexViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.prototype);
