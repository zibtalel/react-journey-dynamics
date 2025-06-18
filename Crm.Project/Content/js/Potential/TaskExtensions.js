/// <reference path="../../../../Content/js/ViewModels/TaskListIndexViewModel.js" />
;
(function() {
	var getContactLink = window.Helper.Task.getContactLink;
	window.Helper.Task.getContactLink = function(task) {
		if (task.ContactType() === "Potential" && window.AuthorizationManager.isAuthorizedForAction("Potential", "Read")) {
			return "#/Crm.Project/Potential/DetailsTemplate/" + task.ContactId();
		}
		return getContactLink.apply(this, arguments);
	};
})();