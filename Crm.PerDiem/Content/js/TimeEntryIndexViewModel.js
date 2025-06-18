/// <reference path="../../../../content/js/viewmodels/viewmodelbase.js" />
/// <reference path="../../../../content/js/namespace.js" />
/// <reference path="../../../../content/js/system/moment.js" />
/// <reference path="../../../../content/js/knockout.component.dashboardcalendarwidget.js" />
/// <reference path="perdiemreportlistviewmodel.js" />

namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel = function () {
	var viewModel = this;
	viewModel.username = window.ko.observable(window.Helper.User.getCurrentUserName());
	viewModel.username.subscribe(function () {
		viewModel.perDiemReportListViewModel.selectedUser(viewModel.username());
	});
	viewModel.minDate = window.ko.observable(window.Crm.PerDiem.Settings.TimeEntry.MaxDaysAgo ? window.moment().startOf("day").utc().add(-parseInt(window.Crm.PerDiem.Settings.TimeEntry.MaxDaysAgo), "days") : false);
	viewModel.selectedDate = window.ko.observable(null);
	viewModel.dates = window.ko.computed(function () {
		var dates = [];
		if (!viewModel.selectedDate()) {
			return dates;
		}
		for (var i = 0; i < 7; i++) {
			dates.push(moment.utc(viewModel.selectedDate()).startOf("isoWeek").add(i, "days").toDate());
		}
		return dates;
	});
	viewModel.dateFilterFrom = window.ko.observable(null);
	viewModel.dateFilterTo = window.ko.observable(null);
	viewModel.expenses = window.ko.observableArray([]);
	viewModel.timeEntries = window.ko.observableArray([]);
	viewModel.lookups = {
		costCenters: { $tableName: "Main_CostCenter" },
		currencies: { $tableName: "Main_Currency" },
		expenseTypes: { $tableName: "CrmPerDiem_ExpenseType" },
		perDiemReportStatuses: { $tableName: "CrmPerDiem_PerDiemReportStatus" },
		timeEntryTypes: { $tableName: "CrmPerDiem_TimeEntryType" }
	};
	viewModel.timeSummaries = window.ko.pureComputed(function () {
		var durations = viewModel.dates().map(function (date) {
			return viewModel.getTimesForDate(date)
				.map(function (time) { return window.moment.duration(time.Duration()).asMinutes(); })
				.reduce(function (acc, val) { return acc + val; }, 0);
		});
		return [
			{
				Name: window.Helper.String.getTranslatedString("DurationHours"),
				Values: durations,
				Format: viewModel.formatDuration
			}
		];
	});
	viewModel.expenseSummaries = window.ko.pureComputed(function () {
		return viewModel.getCurrencyKeys().map(function (currencyKey) {
			return {
				Name: currencyKey,
				Values: viewModel.dates().map(function (date) {
					return (viewModel.getExpensesForDate(date)).filter(
						function (expense) { return expense.CurrencyKey() === currencyKey; })
						.map(function (expense) { return parseFloat(expense.Amount()); })
						.reduce(function (acc, val) { return acc + val; }, 0);
				}),
				Format: viewModel.formatExpenseValue
			};
		});
	});
	viewModel.summaries = window.ko.pureComputed(function () {
		return viewModel.timeSummaries().concat(viewModel.expenseSummaries())
			.filter(function(summary) {
				return summary.Values.reduce(function(acc, val) { return acc + val; }, 0) > 0;
			});
	});
	viewModel.summary = window.ko.pureComputed(function () {
		return viewModel.summaries().map(function(summary) {
			return {
				Name: summary.Name,
				Value: summary.Values.reduce(function(acc, val) { return acc + val; }, 0),
				Format: summary.Format
			};
		});
	});
	viewModel.canAddExpense = window.ko.pureComputed(function() {
		if (window.Crm.PerDiem.Settings.Expense.MaxDaysAgo) {
			var minDate = window.moment().startOf("day").utc(true)
				.add(-parseInt(window.Crm.PerDiem.Settings.Expense.MaxDaysAgo), "days");
			return !minDate.isAfter(viewModel.selectedDate()) && !window.moment().isBefore(viewModel.selectedDate(), "days");
		}
		return true;
	});
	viewModel.canAddTimeEntry = window.ko.pureComputed(function() {
		if (window.Crm.PerDiem.Settings.TimeEntry.MaxDaysAgo) {
			var minDate = window.moment().startOf("day").utc(true)
				.add(-parseInt(window.Crm.PerDiem.Settings.TimeEntry.MaxDaysAgo), "days");
			return !minDate.isAfter(viewModel.selectedDate()) && !window.moment().isBefore(viewModel.selectedDate(), "days");
		}
		return true;

	});
	window.Main.ViewModels.ViewModelBase.apply(this, arguments);
	viewModel.perDiemReportListViewModel = new window.Crm.PerDiem.ViewModels.PerDiemReportListViewModel();
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype =
	Object.create(window.Main.ViewModels.ViewModelBase.prototype);
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.init = function () {
	var viewModel = this;
	viewModel.dates.subscribe(function (dates) {
		if (!window.moment(dates[0]).isSame(window.moment(viewModel.dateFilterFrom()))) {
			viewModel.dateFilterFrom(dates[0]);
		}
		var dateFilterTo = window.moment(dates[dates.length - 1]).endOf("day");
		if (!dateFilterTo.isSame(window.moment(viewModel.dateFilterTo()))) {
			viewModel.dateFilterTo(dateFilterTo.toDate());
		}
	});
	viewModel.selectedDate(window.moment().utc(true).startOf("day").toDate());
	viewModel.filterParameters = window.ko.computed(function() {
		return [viewModel.dateFilterFrom(), viewModel.dateFilterTo(), viewModel.username()];
	}).extend({ rateLimit: { timeout: 0, method: "notifyWhenChangesStop" } });
	viewModel.filterParameters.subscribe(viewModel.refresh.bind(viewModel));
	return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups)
		.then(function() {
			window.Helper.Database.registerEventHandlers(viewModel,
				{
					"CrmPerDiem_UserExpense": {
						"afterCreate": viewModel.refresh,
						"afterDelete": viewModel.refresh,
						"afterUpdate": viewModel.refresh
					},
					"CrmPerDiem_UserTimeEntry": {
						"afterCreate": viewModel.refresh,
						"afterDelete": viewModel.refresh,
						"afterUpdate": viewModel.refresh
					},
					"Main_FileResource": {
						"afterUpdate": viewModel.refresh
					}
				});
			return viewModel.refresh();
		}).then(function() {
			return viewModel.perDiemReportListViewModel.init();
		}).then(function() {
			viewModel.perDiemReportListViewModel.loading(false);
		});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.refresh = function () {
	var viewModel = this;
	viewModel.loading(true);
	return window.database.CrmPerDiem_UserTimeEntry
		.filter(function (timeEntry) {
			return timeEntry.ResponsibleUser === this.username &&
				timeEntry.Date >= this.dateMin &&
				timeEntry.Date < this.dateMax;
		},
			{
				dateMin: viewModel.dateFilterFrom(),
				dateMax: viewModel.dateFilterTo(),
				username: viewModel.username()
			})
		.toArray(viewModel.timeEntries)
		.then(function () {
			return window.database.CrmPerDiem_UserExpense
				.include("FileResource")
				.filter(function (expense) {
					return expense.ResponsibleUser === this.username &&
						expense.Date >= this.dateMin &&
						expense.Date < this.dateMax;
				},
					{
						dateMin: viewModel.dateFilterFrom(),
						dateMax: viewModel.dateFilterTo(),
						username: viewModel.username()
					})
				.toArray(viewModel.expenses);
		}).then(function () {
			viewModel.loading(false);
		});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.formatDuration = function(durationInMinutes) {
	return window.moment.duration().add(durationInMinutes, "minutes").format("hh:mm", { stopTrim: "h" });
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.formatExpenseValue = function(value) {
	return window.Globalize.formatNumber(parseFloat(value), { maximumFractionDigits: 2, minimumFractionDigits: 2 });
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.getCurrencyKeys = function() {
	var viewModel = this;
	return window._.uniq(viewModel.expenses().map(function(x) {
		return x.CurrencyKey();
	}));
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.getExpensesForDate = function(date) {
	var viewModel = this;
	return viewModel.expenses().filter(function(x) {
		return window.moment(x.Date()).isSame(date);
	});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.getTimesForDate = function(date) {
	var viewModel = this;
	return viewModel.timeEntries().filter(function(x) {
		return window.moment(x.Date()).isSame(date);
	});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.deleteExpense = function (expense) {
	var viewModel = this;
	if (expense.IsClosed()) {
		return new $.Deferred().resolve().promise();
	}
	return window.Helper.Confirm.confirmDelete().done(function () {
		viewModel.loading(true);
		if (expense.FileResource()) {
			window.database.remove(expense.FileResource());
		}
		window.database.remove(expense);
		return window.database.saveChanges();
	});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.deleteTimeEntry = function(timeEntry) {
	var viewModel = this;
	if (timeEntry.IsClosed()) {
		return;
	}
	window.Helper.Confirm.confirmDelete().done(function() {
			viewModel.loading(true);
			window.database.remove(timeEntry);
			return window.database.saveChanges();
		});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.getCircleColor = function (index, date) {
	var viewModel = this;
	if (viewModel.timeSummaries()[0].Values[index] > 10 * 60) {
		return "c-red";
	}
	if (viewModel.getExpensesForDate(date).length > 0 || viewModel.getTimesForDate(date).length > 0) {
		return "c-green";
	}
	return "c-gray";
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.goTo = function(date, username) {
	var viewModel = this;
	viewModel.selectedDate(window.moment(window.ko.unwrap(date)).startOf("day").utc(true).toDate());
	viewModel.username(window.ko.unwrap(username));
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.goToNextWeek = function () {
	var viewModel = this;
	viewModel.selectedDate(window.moment(viewModel.selectedDate()).add(24 * 7, "hours").toDate());
};
namespace("Crm.PerDiem.ViewModels").TimeEntryIndexViewModel.prototype.goToPreviousWeek = function () {
	var viewModel = this;
	viewModel.selectedDate(window.moment(viewModel.selectedDate()).subtract(24 * 7, "hours").toDate());
};

(function() {
	var dashboardCalendarWidgetViewModel = window.Main.ViewModels.DashboardCalendarWidgetViewModel;
	window.Main.ViewModels.DashboardCalendarWidgetViewModel = function() {
		var viewModel = this;
		dashboardCalendarWidgetViewModel.apply(this, arguments);
		if (window.database.CrmPerDiem_TimeEntryType) {
			viewModel.lookups.timeEntryTypes = { $tableName: "CrmPerDiem_TimeEntryType" };
		}
		if (window.database.CrmPerDiem_UserTimeEntry) {
			viewModel.timeEntryFilterOption = {
				Value: window.database.CrmPerDiem_UserTimeEntry.collectionName,
				Caption: window.Helper.String.getTranslatedString("TimeEntries")
			};
			viewModel.filterOptions.push(viewModel.timeEntryFilterOption);
			viewModel.selectedFilters.subscribe(function (changes) {
				if (changes.some(function (change) { return change.status === viewModel.changeStatus.added && change.moved === undefined && change.value.Value === viewModel.timeEntryFilterOption.Value })) {
					viewModel.loading(true);
					viewModel.getTimeEntryTimelineEvents().then(function (results) {
						viewModel.timelineEvents(viewModel.timelineEvents().filter(function (event) { return event.innerInstance.constructor.name != viewModel.timeEntryFilterOption.Value }));
						viewModel.timelineEvents(viewModel.timelineEvents().concat(results));
						viewModel.loading(false);
					});
				}
				if (changes.some(function (change) { return change.status === viewModel.changeStatus.deleted && change.moved === undefined && change.value.Value === viewModel.timeEntryFilterOption.Value })) {
					viewModel.timelineEvents(viewModel.timelineEvents().filter(function (event) { return event.innerInstance.constructor.name != viewModel.timeEntryFilterOption.Value }));
				}
			}, null, "arrayChange");
		}
	};
	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype = dashboardCalendarWidgetViewModel.prototype;
	var getTimelineEvent = window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getTimelineEvent;
	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getTimelineEvent = function(it) {
		var viewModel = this;
		if (window.database.CrmPerDiem_UserTimeEntry &&
			it.innerInstance instanceof
			window.database.CrmPerDiem_UserTimeEntry.CrmPerDiem_UserTimeEntry) {
			return {
				entityType: window.Helper.getTranslatedString("TimeEntry"),
				title: window.Helper.Lookup.getLookupValue(viewModel.lookups.timeEntryTypes, it.TimeEntryTypeKey()),
				start: it.From() || it.Date(),
				end: it.To() || it.Date(),
				allDay: it.From() && it.To() && moment(it.From()).clone().startOf("day").isSame(it.From()) && moment(it.From()).clone().add(1, "day").startOf("day").isSame(it.To()),
				backgroundColor: window.Helper.Lookup.getLookupColor(viewModel.lookups.timeEntryTypes, it.TimeEntryTypeKey())
			};

		}
		return getTimelineEvent.apply(this, arguments);
	};
	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getTimeEntryTimelineEvents = function() {
		var viewModel = this;
		if (window.database.CrmPerDiem_UserTimeEntry && viewModel.currentUser()) {
			return window.database.CrmPerDiem_UserTimeEntry
				.filter(function (it) {
					return it.ResponsibleUser === this.currentUser &&
						it.Date >= this.start &&
						it.Date <= this.end;
				},
					{
						entityType: window.Helper.getTranslatedString("TimeEntry"),
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