/// <reference path="../../../../Content/js/namespace.js" />
/// <reference path="../../../../Content/js/system/moment.js" />
/// <reference path="../../../../node_modules/lodash/lodash.js" />
namespace("Crm.PerDiem.ViewModels").PerDiemReportDetailsModalViewModel = function () {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	viewModel.expenses = window.ko.observableArray([]);
	viewModel.perDiemReport = window.ko.observable(null);
	viewModel.timeEntries = window.ko.observableArray([]);
	viewModel.lookups = {
		currencies: { $tableName: "Main_Currency" },
		expenseTypes: { $tableName: "CrmPerDiem_ExpenseType" },
		perDiemReportStatuses: { $tableName: "CrmPerDiem_PerDiemReportStatus" },
		perDiemReportTypes: { $tableName: "CrmPerDiem_PerDiemReportType" },
		timeEntryTypes: { $tableName: "CrmPerDiem_TimeEntryType" }
	};
	viewModel.reportEntries = window.ko.pureComputed(function () {
		return viewModel.timeEntries().concat(viewModel.expenses());
	});
	viewModel.users = window.ko.observableArray([]);

	viewModel.footerHeight = window.ko.observable(null);
	viewModel.headerHeight = window.ko.observable(null);
	viewModel.site = window.ko.observable(null);
};
namespace("Crm.PerDiem.ViewModels").PerDiemReportDetailsModalViewModel.prototype.getDurationSum = function (reportEntries) {
	return window.moment.duration(reportEntries.filter(function (x) {
		return !!ko.unwrap(x.Duration);
	}).map(function (x) { return window.moment.duration(x.Duration()); })
		.reduce(function (acc, val) {
			return acc.add(val);
		},
			window.moment.duration()));
};
namespace("Crm.PerDiem.ViewModels").PerDiemReportDetailsModalViewModel.prototype.getExpenseSums = function (reportEntries) {
	return Object.entries(reportEntries.filter(function (x) {
		return !!ko.unwrap(x.Amount);
	}).reduce(function (acc, val) {
		acc[val.CurrencyKey()] = acc[val.CurrencyKey()] || 0;
		acc[val.CurrencyKey()] += val.Amount();
		return acc;
	},
		{}));
};
namespace("Crm.PerDiem.ViewModels").PerDiemReportDetailsModalViewModel.prototype.getGroupedReportEntries = function () {
	var viewModel = this;
	var distinctReportEntries = window._.uniqBy(viewModel.reportEntries().map(function (entry) {
		return entry.Date().getTime();
	}));
	return distinctReportEntries.map(function (time) {
			return window.moment(time).toDate();
		})
		.sort(function (a, b) { return a < b ? -1 : a > b ? 1 : 0; })
		.map(function (date) {
			var dateEntries = viewModel.reportEntries().filter(function (x) {
				return window.moment(x.Date()).isSame(date);
			});
			var users = window._.uniq(dateEntries.map(function (x) {
				return x.ResponsibleUser();
			}));
			return {
				Date: date,
				Users: users.map(function (username) {
					var userDateEntries = dateEntries.filter(function (x) {
						return x.ResponsibleUser() === username;
					});
					return {
						Duration: viewModel.getDurationSum(userDateEntries),
						Entries: userDateEntries,
						From: new Date(Math.min.apply(Math,
							userDateEntries.filter(function (x) { return !!window.ko.unwrap(x.From); }).map(
								function (x) {
									return x.From().getTime();
								}))),
						Sums: viewModel.getExpenseSums(userDateEntries),
						To: new Date(Math.max.apply(Math,
							userDateEntries.filter(function (x) { return !!window.ko.unwrap(x.To); }).map(
								function (x) {
									return x.To().getTime();
								}))),
						User: viewModel.users().find(function (x) { return x.Id() === username; })
					};
				})
			};
		});
};
namespace("Crm.PerDiem.ViewModels").PerDiemReportDetailsModalViewModel.prototype.getUserExpenseSums = function (username) {
	var viewModel = this;
	return viewModel.getExpenseSums(viewModel.reportEntries().filter(function (x) {
		return x.ResponsibleUser() === username;
	}));
};
namespace("Crm.PerDiem.ViewModels").PerDiemReportDetailsModalViewModel.prototype.getUserDurationSum = function (username) {
	var viewModel = this;
	return viewModel.getDurationSum(viewModel.reportEntries().filter(function (x) {
		return x.ResponsibleUser() === username;
	}));
};
namespace("Crm.PerDiem.ViewModels").PerDiemReportDetailsModalViewModel.prototype.init = function (id, params) {
	var viewModel = this;
	return window.Helper.Database.initialize().then(function () {
		return window.Crm.Offline.Bootstrapper.initializeSettings();
	}).then(function () {
		return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups);
	}).then(function () {
		return id ? window.database.CrmPerDiem_PerDiemReport.find(id) : null;
	}).then(function (perDiemReport) {
		viewModel.perDiemReport(perDiemReport ? perDiemReport.asKoObservable() : null);
		return viewModel.loadReportEntries(id, params);
	}).then(function () {
		var usernames = window._.uniq(viewModel.reportEntries().map(function (x) { return x.ResponsibleUser(); }));
		return window.database.Main_User
			.filter(function (it) { return it.Id in this.usernames; }, { usernames: usernames })
			.toArray(viewModel.users);
	}).then(function() {
		return window.database.Main_Site.GetCurrentSite().first();
	}).then(function (site) {
		viewModel.site(site);
		var headerHeight = +window.Main.Settings.Report.HeaderHeight +
			+window.Main.Settings.Report.HeaderSpacing;
		viewModel.headerHeight(headerHeight);
		var footerHeight = +window.Main.Settings.Report.FooterHeight +
			+window.Main.Settings.Report.FooterSpacing;
		viewModel.footerHeight(footerHeight);
	});
};
namespace("Crm.PerDiem.ViewModels").PerDiemReportDetailsModalViewModel.prototype.loadReportEntries = function (id, params) {
	var viewModel = this;
	var timeEntryFilter = id
		? function (it) { return it.PerDiemReportId === this.id; }
		: function (it) { return it.ResponsibleUser === this.username && it.From >= this.fromDate && it.To <= this.toDate; };
	var expenseFilter = id
		? function (it) { return it.PerDiemReportId === this.id; }
		: function (it) { return it.ResponsibleUser === this.username && it.Date >= this.fromDate && it.Date <= this.toDate; };
	var filterParams = id ? { id: id } : params;
	return window.database.CrmPerDiem_UserTimeEntry
		.filter(timeEntryFilter, filterParams)
		.orderBy("it.Date")
		.orderBy("it.From")
		.toArray(viewModel.timeEntries)
		.then(function () {
			return window.database.CrmPerDiem_UserExpense
				.include("FileResource")
				.filter(expenseFilter, filterParams)
				.orderBy("it.Date")
				.toArray(viewModel.expenses);
		});
};