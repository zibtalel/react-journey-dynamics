/// <reference path="../../../../Content/js/ViewModels/ContactDetailsTasksTabViewModel.js" />
namespace("Crm.Project.ViewModels").ProjectDetailsTasksTabViewModel = function (parentViewModel) {
	window.Main.ViewModels.ContactDetailsTasksTabViewModel.apply(this, arguments);
}
namespace("Crm.Project.ViewModels").ProjectDetailsTasksTabViewModel.prototype = Object.create(window.Main.ViewModels.ContactDetailsTasksTabViewModel.prototype);