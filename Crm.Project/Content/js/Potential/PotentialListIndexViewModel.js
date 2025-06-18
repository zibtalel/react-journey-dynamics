/// <reference path="../../../../../Content/js/ViewModels/ContactListViewModel.js" />
namespace("Crm.Project.ViewModels").PotentialListIndexViewModel = function () {
	var viewModel = this;
	viewModel.lookups = {
		potentialPriorities: { $tableName: "CrmProject_PotentialPriority" },
		potentialStatuses: { $tableName: "CrmProject_PotentialStatus" },
		sourceType: { $tableName: "Main_SourceType" }
	};
	viewModel.currentUserName = document.getElementById("meta.CurrentUser").content;
	var joins = ["ResponsibleUserUser", "Parent", {
		Selector: "Tags",
		Operation: "orderBy(function(t) { return t.Name; })"
	}];
	joins.push("ProductFamily")
	window.Main.ViewModels.ContactListViewModel.call(this, "CrmProject_Potential", "Name", "ASC", joins);
	var activeBookmark = {
		Category: window.Helper.String.getTranslatedString("Filter"),
		Name: window.Helper.String.getTranslatedString("All"),
		Key: "All",
		Expression: function (query) {
			return query;
		}
	};
	viewModel.bookmarks.push(activeBookmark);
	viewModel.bookmark(activeBookmark);
	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString("Filter"),
		Name: window.Helper.String.getTranslatedString("MyOpenedPotentials"),
		Key: "MyOpenedPotentials",
		Expression: function (query) {
			return query.filter("it.StatusKey != 'closed' && it.ResponsibleUser === this.responsibleUser", { responsibleUser: viewModel.currentUserName });
		}
	});
	
}
namespace("Crm.Project.ViewModels").PotentialListIndexViewModel.prototype = Object.create(window.Main.ViewModels.ContactListViewModel.prototype);
namespace("Crm.Project.ViewModels").PotentialListIndexViewModel.prototype.init = function (id, params) {
	var viewModel = this;
	if (params && params.companyId && params.productFamilyKey) {
		viewModel.companyIdFilter = params.companyId;
		viewModel.productFamilyKey = params.productFamilyKey;
	}
	return window.Main.ViewModels.ContactListViewModel.prototype.init.apply(viewModel, arguments)
		.then(function () { return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups); });
};
namespace("Crm.Project.ViewModels").PotentialListIndexViewModel.prototype.initItems = function (items) {
	const viewModel = this;
	const args = arguments;
	let queries = [];
	items.map(item => {
		queries.push(
			{
				queryable: window.database.Main_Note
					.filter("it.ContactId === potentialId", { potentialId: item.Id() })
					.orderByDescending("it.CreateDate")
					.map("it.ModifyDate")
					.take(1),
				method: "toArray",
				handler: function (result) {
					if (result.length > 0) {
						item.LastNoteDate(result[0])
					}
					return items
				}
			}
		)
	});

	return window.Helper.Batch.Execute(queries).then(function () {
		return window.Main.ViewModels.ContactListViewModel.prototype.initItems.apply(viewModel, args);
	});
};
namespace("Crm.Project.ViewModels").PotentialListIndexViewModel.prototype.applyFilters = function (query) {
	const viewModel = this;
	query = window.Main.ViewModels.ContactListViewModel.prototype.applyFilters.call(viewModel, query);
	if (viewModel.companyIdFilter && viewModel.productFamilyKey) {
		query = query.filter("it.ProductFamilyKey == this.productFamilyKey && it.ParentId == this.companyId",
			{productFamilyKey: window.$data.createGuid(viewModel.productFamilyKey), companyId: window.$data.createGuid(viewModel.companyIdFilter) });
	}
	return query;
};

