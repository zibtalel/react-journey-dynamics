namespace("Crm.Project.ViewModels").PotentialDetailsContactHistoryTabViewModel = function (parentViewModel) {
	var viewModel = this;
	viewModel.companyId = window.ko.observable(null);
	viewModel.contactId = ko.observable(null);
	window.Main.ViewModels.GenericListViewModel.call(viewModel, "CrmProject_DocumentEntry", ["Person.Id"], ["ASC"], ["Person", "Person.Parent"]);
	var potentialId = parentViewModel.potential().Id();
	var parentpotentialId = parentViewModel.potential().ParentId();
	viewModel.contactId(parentViewModel.potential().Id());
	viewModel.companyId(parentpotentialId);
	viewModel.getFilter("ContactKey").extend({ filterOperator: "===" })(potentialId);
}
namespace("Crm.Project.ViewModels").PotentialDetailsContactHistoryTabViewModel.prototype = Object.create(window.Main.ViewModels.GenericListViewModel.prototype);

namespace("Crm.Project.ViewModels").PotentialDetailsContactHistoryTabViewModel.prototype.initItems = function (items) {
	const queries = [];
	items.map(item => {
		queries.push(
			{
				queryable: window.database.Main_DocumentAttribute
					.include("FileResource")
					.filter("it.Id == Id",
						{ Id: ko.unwrap(item.DocumentKey) }),
				method: "toArray",
				handler: function (documentAttribute) {
					if (documentAttribute) {
						item.Document(documentAttribute[0].asKoObservable())
					}
					return items;
				}
			})
	})
	return Helper.Batch.Execute(queries).then(function () {
		return items;
	})
}
namespace("Crm.Project.ViewModels").PotentialDetailsContactHistoryTabViewModel.prototype.deleteEntry = function (entry) {
	var viewModel = this;
	window.Helper.Confirm.confirmDelete().done(function () {
		viewModel.loading(true);
		var entity = window.Helper.Database.getDatabaseEntity(entry);
		window.database.remove(entity);
		return window.database.saveChanges();
	})
		.then(function () {
			viewModel.loading(false);
		});
}
namespace("Crm.Project.ViewModels").PotentialDetailsContactHistoryTabViewModel.prototype.toggleFeedbackReceived = function (entry) {
	var viewModel = this;
	viewModel.loading(true);
	window.database.attachOrGet.bind(window.database)
	window.database.attachOrGet(entry.innerInstance);
	entry.FeedbackReceived(!entry.FeedbackReceived());
	window.database.saveChanges().then(function () {
		viewModel.loading(false);
	});
}
