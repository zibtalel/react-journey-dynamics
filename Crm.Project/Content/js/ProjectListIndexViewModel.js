/// <reference path="../../../../Content/js/ViewModels/ContactListViewModel.js" />
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel = function() {
	var viewModel = this;
	viewModel.lookups = {
		projectCategories: { $tableName: "CrmProject_ProjectCategory" },
		projectStatuses: { $tableName: "CrmProject_ProjectStatus" },
		currencies: { $tableName: "Main_Currency" }
	};
	viewModel.currentUserName = window.Helper.User.getCurrentUserName();
	viewModel.currenciesValues = ko.observableArray([]);

	var joins = ["ResponsibleUserUser", "Parent", {
		Selector: "Tags",
		Operation: "orderBy(function(t) { return t.Name; })"
	}];
	joins.push("ProductFamily")
	if (window.Crm.Project.Settings.ProjectsHaveAddresses) {
		joins.push("ProjectAddress");
	} else {
		joins.push({
			Selector: "Parent.Addresses",
			Operation: "filter(function(a) { return a.IsCompanyStandardAddress === true; })"
		});
	}
	window.Main.ViewModels.ContactListViewModel.call(this, "CrmProject_Project", ["StatusKey","Name"], ["ASC", "ASC"], joins);
	if (window.Crm.Project.Settings.ProjectsHaveAddresses) {
		viewModel.getFilter("ProjectAddress.IsCompanyStandardAddress")(true);
		viewModel.latitudeFilterColumn = "ProjectAddress.Latitude";
		viewModel.longitudeFilterColumn = "ProjectAddress.Longitude";
	} else {
		viewModel.getFilter("Parent.StandardAddress.IsCompanyStandardAddress")(true);
		viewModel.latitudeFilterColumn = "Parent.StandardAddress.Latitude";
		viewModel.longitudeFilterColumn = "Parent.StandardAddress.Longitude";
	}
	const bookmarksCategory = "Filter";
	const activeBookmark = {
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("All"),
		Key: "All",
		Expression: function (query) {
			return query;
		}
	}
	viewModel.bookmark(activeBookmark)
	viewModel.bookmarks.push(activeBookmark)

	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("AllOpenProjects"),
		Key: "AllOpenProjects",
		Expression: function (query) {
			return query.filter("it.StatusKey in this.statusKey", { statusKey:  viewModel.openStatuses() });
		}
	});

	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("AllWonProjects"),
		Key: "AllWonProjects",
		Expression: function (query) {
			return query.filter("it.StatusKey in this.statusKey", { statusKey: viewModel.wonStatuses() });
		}
	});

	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("AllLostProjects"),
		Key: "AllLostProjects",
		Expression: function (query) {
			return query.filter("it.StatusKey in this.statusKey", { statusKey: viewModel.lostStatuses() });
		}
	})

	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("OverdueProjects"),
		Key: "OverdueProjects",
		Expression: function (query) {
			return query.filter("it.StatusKey in this.statusKey && it.DueDate < this.date ", { statusKey: viewModel.openStatuses(), date: new Date() });
		}
	});

	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("OwnOverdueProjects"),
		Key: "OwnOverdueProjects",
		Expression: function (query) {
			return query.filter("it.StatusKey in this.statusKey && it.DueDate < this.date && it.ResponsibleUser === this.username",
				{ statusKey: viewModel.openStatuses(), date: new Date(), username: viewModel.currentUserName });
		}
	});

	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("OwnOpenProjectsThisMonth"),
		Key: "OwnOpenProjectsThisMonth",
		Expression: function (query) {
			return query.filter("it.StatusKey === this.statusKey && it.ResponsibleUser === this.username && it.DueDate >= this.startOfMonth && it.DueDate <= this.endOfMonth",
				{
					statusKey: viewModel.openStatuses(),
					username: viewModel.currentUserName,
					startOfMonth: moment().startOf('month').toISOString(),
					endOfMonth: moment().endOf('month').toISOString()
				});
		}
	});

	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("OwnOpenProjectsNextMonth"),
		Key: "OwnOpenProjectsNextMonth",
		Expression: function (query) {
			return query.filter("it.StatusKey in this.statusKey && it.ResponsibleUser === this.username && it.DueDate >= this.startOfMonth && it.DueDate <= this.endOfMonth",
				{
					statusKey: viewModel.openStatuses(),
					username: viewModel.currentUserName,
					startOfMonth: moment().startOf('month').add(1, 'M').toISOString(),
					endOfMonth: moment().endOf('month').add(1, 'M').toISOString()
				});
		}
	});

	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("OwnOpenProjects"),
		Key: "OwnOpenProjects",
		Expression: function (query) {
			return query.filter("it.StatusKey in this.statusKey && it.ResponsibleUser === this.username",
				{ statusKey: viewModel.openStatuses(), username: viewModel.currentUserName });
		}
	});

	viewModel.bookmarks.push({
		Category: window.Helper.String.getTranslatedString(bookmarksCategory),
		Name: window.Helper.String.getTranslatedString("AllOpenProjectsFromTheMonthAfterTheNextMonth"),
		Key: "AllOpenProjectsFromTheMonthAfterTheNextMonth",
		Expression: function (query) {
			return query.filter("it.StatusKey in this.statusKey && it.ResponsibleUser === this.username && it.DueDate >= this.startOfMonth && it.DueDate <= this.endOfMonth",
				{
					statusKey: viewModel.openStatuses(),
					username: viewModel.currentUserName,
					startOfMonth: moment().startOf('month').add(2, 'M').toISOString(),
					endOfMonth: moment().endOf('month').add(2, 'M').toISOString()
				});
		}
	});

	viewModel.timelineProperties.push({ Start: "CreateDate", End: "CreateDate", Caption: window.Helper.String.getTranslatedString("CreateDate") });
	viewModel.timelineProperties.push({ Start: "DueDate", End: "DueDate", Caption: window.Helper.String.getTranslatedString("DueDate") });
	viewModel.timelineProperties.push({ Start: "StatusDate", End: "StatusDate", Caption: window.Helper.String.getTranslatedString("StatusDate") });

	window.Main.ViewModels.GenericListChartViewModel.call(this, this.mapAndGroupBy.bind(this), window.ko.pureComputed(this.chartSource.bind(this)));
	window.Main.ViewModels.GenericListMapViewModel.call(this);
	viewModel.chartAxisXLabel("Date");
	viewModel.chartAxisYLabel("Value");
	viewModel.getChartColor = function(categoryKey) {
		var category = viewModel.lookups.projectCategories[categoryKey];
		return category != null ? category.Color : null;
	};
	viewModel.getChartLabel = function(categoryKey) {
		var category = viewModel.lookups.projectCategories[categoryKey];
		return category != null ? category.Value : window.Helper.String.getTranslatedString("Unknown");
	};
}
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype = Object.create(window.Main.ViewModels.ContactListViewModel.prototype);
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.init = async function(id, params) {
	var viewModel = this;
	if (params && params.productFamilyKey && params.companyId) {
		viewModel.companyId = params.companyId;
		viewModel.productFamilyKey = params.productFamilyKey;
	}
	await  window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups)
	await window.Helper.Project.allProjectsCurrencySum().then(result => viewModel.currenciesValues(result))
	await window.Main.ViewModels.ContactListViewModel.prototype.init.apply(viewModel, arguments)
};
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.getIcsLinkAllowed = function() {
	return window.AuthorizationManager.isAuthorizedForAction("Project", "Ics");
};
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.getTimelineEvent = function(project, property) {
	let timeLineEvent = window.Main.ViewModels.GenericListViewModel.prototype.getTimelineEvent.call(this, project, property);
	timeLineEvent.entityType = window.Helper.getTranslatedString("Project");
	timeLineEvent.title = window.Helper.Project.getName(project);
	timeLineEvent.backgroundColor = window.Helper.Lookup.getLookupColor(this.lookups.projectCategories, project.CategoryKey);
	timeLineEvent.url = "#/Crm.Project/Project/DetailsTemplate/" + project.Id();
	return timeLineEvent;
};
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.downloadIcs = function () {
	var viewModel = this;
	var cal = window.ics();
	viewModel.query().toArray(function (result) {
		result.forEach(function (x) {
			var title = x.ProjectNo + " - " + x.Name;
			var description = x.ResponsibleUserDisplayName + "; " + x.ParentName;
			cal.addEvent(title, description, "", x.DueDate, x.DueDate);
		});
	}).then(function () {
		cal.download(viewModel.entityType + "_" + viewModel.currentUserName);
	});
}
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.applyFilters = function (query) {
	const viewModel = this;
	query = window.Main.ViewModels.ContactListViewModel.prototype.applyFilters.call(viewModel, query);
	if (viewModel.companyId && viewModel.productFamilyKey) {
		query = query.filter("it.ProductFamilyKey == this.productFamilyKey && it.ParentId == this.companyId",
			{ productFamilyKey: window.$data.createGuid(viewModel.productFamilyKey), companyId: window.$data.createGuid(viewModel.companyId) });
	}
	return query;
}

namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.getAddress = function(item) {
	if (window.Crm.Project.Settings.ProjectsHaveAddresses) {
		return (window.ko.unwrap(item.Addresses) || [])[0];
	} else {
		var parent = window.ko.unwrap(item.Parent) || {};
		return (window.ko.unwrap(parent.Addresses) || [])[0];
	}
};
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.getLatitude = function(item) {
	var address = this.getAddress(item);
	return address ? window.ko.unwrap(address.Latitude) : null;
};
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.getLongitude = function(item) {
	var address = this.getAddress(item);
	return address ? window.ko.unwrap(address.Longitude) : null;
};
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.getPopupInformation = function(item) {
	return window.ko.unwrap(item.LegacyName);
};
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.chartSource = function() {
	var ids = this.items().map(function(x) { return window.ko.unwrap(x.Id) });
	return window.database.CrmProject_Project.ValuePerCategoryAndYear(ids);
};
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.mapAndGroupBy = function(query) {
	return query;
};
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.lostStatuses = function () {
	const viewModel = this;
	return viewModel.lookups
		.projectStatuses.$array.filter(function (x) { return x.Groups !== "Won" && x.Groups !== "Open" }).map(function (x) { return x.Key });
}
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.openStatuses = function () {
	const viewModel = this;
	return viewModel.lookups
		.projectStatuses.$array.filter(function (x) { return x.Key !== null && x.Groups !== "Won" && x.Groups !== "Lost" }).map(function (x) { return x.Key });
}
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.wonStatuses = function () {
	const viewModel = this;
	return viewModel.lookups
		.projectStatuses.$array.filter(function (x) { return x.Key !== null && x.Groups !== "Open" && x.Groups !== "Lost" }).map(function (x) { return x.Key });
}
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.getItemGroup = function (item) {
	const groupedData = this.currenciesValues()[item.StatusKey()]?.Currencies
	return window.Helper.Project.getItemGroup(groupedData, item, this);
}
namespace("Crm.Project.ViewModels").ProjectListIndexViewModel.prototype.initItems = function (items) {
	let viewModel = this;
	if (viewModel.getFiltersWithValue().length > 0) {
		return window.Helper.Project.FilteredProjectsSum(this.applyFilters(window.database.CrmProject_Project)).then(result => {
			items.forEach(item => {
				let groupedData = result[item.StatusKey()]?.Currencies;
				item.itemGroup = window.Helper.Project.getItemGroup(groupedData, item, this);
			});
			return items;
		})
	} else {
		return items;
	}
}

;(function() {
	var dashboardCalendarWidgetViewModel = window.Main.ViewModels.DashboardCalendarWidgetViewModel;
	window.Main.ViewModels.DashboardCalendarWidgetViewModel = function() {
		var viewModel = this;
		dashboardCalendarWidgetViewModel.apply(this, arguments);
		if (window.database.CrmProject_ProjectCategory) {
			viewModel.lookups.projectCategories = { $tableName: "CrmProject_ProjectCategory" };
		}
		if (window.database.CrmProject_Project) {
			viewModel.projectFilterOption = {
				Value: window.database.CrmProject_Project.collectionName,
				Caption: window.Helper.String.getTranslatedString("Projects")
			};
			viewModel.filterOptions.push(viewModel.projectFilterOption);
			viewModel.selectedFilters.subscribe(function (changes) {
				if (changes.some(function (change) { return change.status === viewModel.changeStatus.added && change.moved === undefined && change.value.Value === viewModel.projectFilterOption.Value })) {
					viewModel.loading(true);
					viewModel.getProjectTimelineEvents().then(function (results) {
						viewModel.timelineEvents(viewModel.timelineEvents().filter(function (event) { return event.innerInstance.constructor.name != viewModel.projectFilterOption.Value }));
						viewModel.timelineEvents(viewModel.timelineEvents().concat(results));
						viewModel.loading(false);
					});
				}
				if (changes.some(function (change) { return change.status === viewModel.changeStatus.deleted && change.moved === undefined && change.value.Value === viewModel.projectFilterOption.Value })) {
					viewModel.timelineEvents(viewModel.timelineEvents().filter(function (event) { return event.innerInstance.constructor.name != viewModel.projectFilterOption.Value }));
				}
			}, null, "arrayChange");
		}
	};
	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype = dashboardCalendarWidgetViewModel.prototype;
	var getTimelineEvent = window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getTimelineEvent;
	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getTimelineEvent = function(it) {
		if (window.database.CrmProject_Project &&
			it.innerInstance instanceof
			window.database.CrmProject_Project.CrmProject_Project) {
			return window.Crm.Project.ViewModels.ProjectListIndexViewModel.prototype.getTimelineEvent.call(this, it, "DueDate");
		}
		return getTimelineEvent.apply(this, arguments);
	};
	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getProjectTimelineEvents = function() {
		var viewModel = this;
		if (window.database.CrmProject_Project && viewModel.currentUser()) {
			return window.database.CrmProject_Project
				.filter(function (it) {
						return it.ResponsibleUser === this.currentUser &&
							it.DueDate >= this.start &&
							it.DueDate <= this.end;
					},
					{
						currentUser: viewModel.currentUser(),
						start: viewModel.timelineStart(),
						end: viewModel.timelineEnd()
					})
				.take(viewModel.maxResults())
				.toArray()
				.then(function (results) {
					return results.map(function (x) { return x.asKoObservable(); });
				});
		} else {
			return new $.Deferred().resolve([]).promise();
		}
	};
})();