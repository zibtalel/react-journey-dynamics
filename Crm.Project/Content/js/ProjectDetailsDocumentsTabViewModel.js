/// <reference path="../../../../Content/js/ViewModels/ContactDetailsDocumentsTabViewModel.js" />
namespace("Crm.Project.ViewModels").ProjectDetailsDocumentsTabViewModel = function(parentViewModel) {
	window.Main.ViewModels.ContactDetailsDocumentsTabViewModel.apply(this, arguments);
	this.contactId(parentViewModel.project().Id());
};
namespace("Crm.Project.ViewModels").ProjectDetailsDocumentsTabViewModel.prototype = Object.create(window.Main.ViewModels.ContactDetailsDocumentsTabViewModel.prototype);