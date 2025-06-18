namespace("Crm.PerDiem.ViewModels").PerDiemReportListViewModel = function() {
	var viewModel = this;
	viewModel.date = ko.observable(null);
	viewModel.selectedUser = window.ko.observable(window.Helper.User.getCurrentUserName());
	window.Main.ViewModels.GenericListViewModel.call(viewModel,
		"CrmPerDiem_PerDiemReport",
		["From"],
		["DESC"],
		["User"]);
	viewModel.date.subscribe(viewModel.filter.bind(viewModel));
	viewModel.selectedUser.subscribe(viewModel.filter.bind(viewModel));
	viewModel.pageSize(10);
	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString("View"),
		Name: window.Helper.String.getTranslatedString("Pending"),
		Key: "Pending",
		Expression: function (query) {
			return query.filter(function (x) { return x.StatusKey in this.statusKeys; },
				{ statusKeys: ["RequestClose", "Open"] });
		}
	});
	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString("View"),
		Name: window.Helper.String.getTranslatedString("Closed"),
		Key: "Closed",
		Expression: function(query) {
			return query.filter(function(x) { return x.StatusKey in this.statusKeys; }, { statusKeys: ["Closed"] });
		}
	});
	viewModel.bookmark(viewModel.bookmarks()[0]);
	viewModel.bookmarkKey = window.ko.observable(null);
	viewModel.bookmarkKey.subscribe(function(bookmarkKey) {
		viewModel.toggleBookmark(viewModel.bookmarks().find(function(x) {
			return x.Key === bookmarkKey;
		}));
	});
	viewModel.bookmarkKey("Pending");
};
namespace("Crm.PerDiem.ViewModels").PerDiemReportListViewModel.prototype =
	Object.create(window.Main.ViewModels.GenericListViewModel.prototype);
namespace("Crm.PerDiem.ViewModels").PerDiemReportListViewModel.prototype.applyFilters = function (query) {
	var viewModel = this;
	if (!!viewModel.date()) {
		query = query.filter("it.From <= this.date && it.To >= this.date",
			{ date: viewModel.date() });
	}
	if (!!viewModel.selectedUser()) {
		query = query.filter("it.User.Id == this.selectedUser",
			{ selectedUser: viewModel.selectedUser() });
	}
	return query;
};