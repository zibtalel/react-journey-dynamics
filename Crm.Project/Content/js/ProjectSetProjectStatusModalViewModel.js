namespace("Crm.Project.ViewModels").ProjectSetProjectStatusModalViewModel = function(parentViewModel) {
	var viewModel = this;
	viewModel.competitorCompanyTypeKeys = ko.observableArray([]);
	viewModel.loading = window.ko.observable(true);
	viewModel.project = parentViewModel.project;
	viewModel.projectLostReasonCategories = window.ko.observableArray([]);
	viewModel.statusKey = window.ko.observable(null);
};
namespace("Crm.Project.ViewModels").ProjectSetProjectStatusModalViewModel.prototype.init = function(statusKey) {
	var viewModel = this;
	viewModel.statusKey(statusKey.toString());
	return window.Helper.User.getCurrentUser()
		.pipe(function(user) {
			return window.database.CrmProject_ProjectLostReasonCategory
				.filter(function(x) { return x.Language == this.languageKey }, { languageKey: user.DefaultLanguageKey })
				.orderBy(function(x) { return x.SortOrder; })
				.toArray(viewModel.projectLostReasonCategories);
		}).pipe(function() {
			viewModel.projectLostReasonCategories.unshift({
				Key: window.ko.observable(null),
				Value: window.ko.observable(window.Helper.String.getTranslatedString("PleaseSelect"))
			});
			return window.Helper.Lookup.queryLookup("Main_CompanyType", null, "it.ExtensionValues.Competitor === true", {}).toArray();
		}).pipe(function(companyTypes){
			viewModel.competitorCompanyTypeKeys(companyTypes.map(function(x) { return x.Key; }));
		});
};
namespace("Crm.Project.ViewModels").ProjectSetProjectStatusModalViewModel.prototype.cancel = function() {
	var viewModel = this;
	viewModel.loading(true);
	viewModel.project().innerInstance.refresh().then(function() {
		viewModel.loading(false);
	});
	return true;
};
namespace("Crm.Project.ViewModels").ProjectSetProjectStatusModalViewModel.prototype.companyFilter = namespace("Crm.Project.ViewModels").ProjectDetailsViewModel.prototype.competitorFilter;
namespace("Crm.Project.ViewModels").ProjectSetProjectStatusModalViewModel.prototype.save = function() {
	var viewModel = this;
	viewModel.loading(true);
	window.database.attachOrGet(viewModel.project().innerInstance);
	viewModel.project().StatusDate(new Date());
	viewModel.project().StatusKey(viewModel.statusKey());
	if (viewModel.statusKey() !== "102"){
		viewModel.project().CompetitorId(null);
		viewModel.project().ProjectLostReason(null);
		viewModel.project().ProjectLostReasonCategoryKey(null);
	}
	window.database.saveChanges().then(function() {
		$(".modal:visible").modal("hide");
		viewModel.loading(false);
	});
	return true;
};