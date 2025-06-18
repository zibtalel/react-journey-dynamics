/// <reference path="../../../../Content/js/ViewModels/TaskListIndexViewModel.js" />
;
(function() {
	var getContactLink = window.Helper.Task.getContactLink;
	window.Helper.Task.getContactLink = function(task) {
		if (task.ContactType() === "Project" && window.AuthorizationManager.isAuthorizedForAction("Project", "Read")) {
			return "#/Crm.Project/Project/DetailsTemplate/" + task.ContactId();
		}
		return getContactLink.apply(this, arguments);
	};

	var initItems = window.Main.ViewModels.TaskListIndexViewModel.prototype.initItems;
	window.Main.ViewModels.TaskListIndexViewModel.prototype.initItems = function (items) {
		return initItems.apply(this, arguments).then(function () {
			if (!window.database.CrmProject_Project) {
				return items;
			}
			var projectIds = _.uniq(items.filter(x => x.ContactType() === "Project").map(x => x.ContactId()));
			if (projectIds.length > 0){
				return window.database.CrmProject_Project.include("Parent").filter(function (it) {
						return it.Id in this.projectIds;
					}, { projectIds: projectIds })
					.toArray()
					.then(function (projects) {
						items.forEach(function (task) {
							var project = projects.filter(x => x.Id === task.ContactId())[0];
							if (project) {
								task.Company = ko.observable(project.Parent.asKoObservable());
							}
						});
						return items;
					});
			}
			return items;
		});
	};
})();