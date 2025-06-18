namespace("Crm.ErpExtension.ViewModels").DeliveryNoteListIndexViewModel = function () {
	window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.call(this, "CrmErpExtension_DeliveryNote", ["OrderNo", "ModifyDate"], ["DESC", "DESC"]);
};
namespace("Crm.ErpExtension.ViewModels").DeliveryNoteListIndexViewModel.prototype = Object.create(window.Crm.ErpExtension.ViewModels.ErpDocumentListViewModel.prototype);
