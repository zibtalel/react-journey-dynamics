namespace("Crm.PerDiem.ViewModels").TimeEntryCloseModalViewModel = function(parentViewModel) {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	viewModel.currentUser = window.ko.observable(null);
	viewModel.selectedUser = window.ko.observable(parentViewModel.username());
	viewModel.distinctDates = window.ko.observableArray([]);
	viewModel.expenses = window.ko.observableArray([]);
	viewModel.selectedDate = window.ko.observable(null);
	viewModel.perDiemReport = window.ko.observable(null);
	viewModel.perDiemReportId = window.ko.observable(null);
	viewModel.perDiemReportTypeKey = window.ko.observable("Custom");
	viewModel.perDiemReportStatuses = window.ko.observableArray([]);
	viewModel.timeEntries = window.ko.observableArray([]);
	viewModel.timeExpenses = window.ko.observableArray([]);
	viewModel.lookups = {
		currencies: { $tableName: "Main_Currency" },
		expenseTypes: { $tableName: "CrmPerDiem_ExpenseType" },
		perDiemReportStatuses: { },
		perDiemReportTypes: { $tableName: "CrmPerDiem_PerDiemReportType" },
		timeEntryTypes: { $tableName: "CrmPerDiem_TimeEntryType" }
	};
	viewModel.errors = window.ko.validation.group(viewModel.perDiemReport, { deep: true });
	viewModel.distinctFromDates = window.ko.pureComputed(function() {
		return window._.uniqBy(viewModel.distinctDates().map(function(date) {
				return viewModel.getFromDate(date);
			}),
			function(date) {
				return date.getTime();
			}).sort(function(a, b) { return a < b ? -1 : a > b ? 1 : 0; });
	});
	viewModel.reportEntries = window.ko.pureComputed(function() {
		return viewModel.timeEntries().concat(viewModel.expenses());
	});
	viewModel.selectedDate.subscribe(function(selectedDate) {
		viewModel.perDiemReport().From(viewModel.getFromDate(selectedDate)?.setMilliseconds(0));
		viewModel.perDiemReport().To(viewModel.getToDate(selectedDate)?.setMilliseconds(0));
	});
	viewModel.selectedUser.subscribe(function () {
		return viewModel.getDistinctDates();
	});
	viewModel.sortedReportEntries = window.ko.pureComputed(function() {
		return viewModel.reportEntries().sort(function(a, b) {
			return a.Date() < b.Date() ? -1 : a.Date() > b.Date() ? 1 : (a.From ? a.From() : 0) > (b.From ? b.From() : 0) ? 1 : -1;
		});
	});
	viewModel.perDiemReportTypeKey.subscribe(function() {
		viewModel.selectedDate(null);
		if (viewModel.perDiemReportTypeKey() === "Custom") {
			viewModel.perDiemReport().From(window.moment().startOf("day").toDate());
			viewModel.perDiemReport().To(window.moment().endOf("day").toDate());
		} else {
			viewModel.perDiemReport().From(null);
			viewModel.perDiemReport().To(null);
		}
		viewModel.loading(true);
		viewModel.refresh().then(function () {
			viewModel.loading(false);
		});
	});
	viewModel.refreshParentViewModel = function() {
		if (parentViewModel.refresh) {
			parentViewModel.refresh();
		}
	};
	viewModel.filterParameters = window.ko.computed(function () {
		return viewModel.perDiemReport() ? [viewModel.perDiemReport().From(), viewModel.perDiemReport().To(), viewModel.selectedUser()] : [];
	}).extend({ rateLimit: { timeout: 0, method: "notifyWhenChangesStop" } });
	viewModel.filterParametersSubscription = viewModel.filterParameters.subscribe(function() {
		if (!!viewModel.perDiemReport().From() && !!viewModel.perDiemReport().To()) {
			viewModel.loading(true);
			viewModel.refresh().then(function () {
				viewModel.loading(false);
			});
		}
	});
	viewModel.canSave = window.ko.computed(function () {
		return viewModel.reportEntries().length > 0
			&& !!viewModel.perDiemReport().From()
			&& !!viewModel.perDiemReport().To()
			&& !viewModel.loading();
	});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryCloseModalViewModel.prototype.init = function(id) {
	var viewModel = this;
	viewModel.perDiemReportId(id || null);
	return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups)
		.then(function() {
			return window.Helper.Lookup.getLocalizedArrayMap("CrmPerDiem_PerDiemReportStatus",
				null,
				window.AuthorizationManager.isAuthorizedForAction("PerDiemReportStatus", "SelectNonMobileLookupValues") ? undefined : function(it) {
					return it.ShowInMobileClient === true;
				});
		}).then(function(lookups) {
			if(!window.AuthorizationManager.isAuthorizedForAction("PerDiemReport", "CloseReport"))
				lookups.$array = lookups.$array.filter(function (x) { return !(x.Key === "Closed"); });
			if(!window.AuthorizationManager.isAuthorizedForAction("PerDiemReport", "RequestCloseReport"))
				lookups.$array = lookups.$array.filter(function (x) { return !(x.Key === "RequestClose"); });
			viewModel.lookups.perDiemReportStatuses = lookups;
			return window.Helper.User.getCurrentUser();
		}).then(function(user) {
			viewModel.currentUser(user);
			if (id) {
				return null;
			}
			return viewModel.getDistinctDates();
		}).then(function() {
			if (id) {
				viewModel.filterParametersSubscription.dispose();
				return window.database.CrmPerDiem_PerDiemReport.find(id)
					.then(window.database.attachOrGet.bind(window.database));
			}
			var newPerDiemReport = window.database.CrmPerDiem_PerDiemReport.CrmPerDiem_PerDiemReport.create();
			newPerDiemReport.CreateUser = window.Helper.User.getCurrentUserName();
			newPerDiemReport.StatusKey = null;
			return newPerDiemReport;
		}).then(function(perDiemReport) {
			viewModel.perDiemReport(perDiemReport.asKoObservable());
			if (!id) {
				if (viewModel.lookups.perDiemReportTypes.$array.length === 1) {
					viewModel.perDiemReportTypeKey(viewModel.lookups.perDiemReportTypes.$array[0].Key);
				}
				else if (viewModel.lookups.perDiemReportTypes.$array.length > 1) {
					viewModel.perDiemReportTypeKey(viewModel.lookups.perDiemReportTypes.$array[1].Key);
				}
				viewModel.perDiemReport().From.subscribe(function(from) {
					if (from > viewModel.perDiemReport().To()) {
						viewModel.perDiemReport().To(from);
					}
				});
				viewModel.perDiemReport().To.subscribe(function(to) {
					if (to < viewModel.perDiemReport().From()) {
						viewModel.perDiemReport().From(to);
					}
				});
				if (viewModel.lookups.perDiemReportStatuses.$array.length === 1) {
					viewModel.perDiemReport().StatusKey(viewModel.lookups.perDiemReportStatuses.$array[0].Key);
				}
				window.database.add(viewModel.perDiemReport().innerInstance);
			} else {
				viewModel.selectedUser(viewModel.perDiemReport().UserName());
				return viewModel.refresh();
			}
		});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryCloseModalViewModel.prototype.refresh = function() {
	var viewModel = this;
	var id = viewModel.perDiemReportId();
	if (!viewModel.perDiemReport().From() || !viewModel.perDiemReport().To()) {
		viewModel.timeEntries([]);
		viewModel.expenses([]);
		return new $.Deferred().resolve().promise();
	}
	var userTimeEntryQuery = window.database.CrmPerDiem_UserTimeEntry.include("ResponsibleUserObject");
	userTimeEntryQuery = id
		? userTimeEntryQuery
		.filter(function(it) { return it.PerDiemReportId === this.id; }, { id: id })
		: userTimeEntryQuery
		.filter(function(it) {
			return it.ResponsibleUser === this.selectedUser &&
					it.IsClosed === 0 &&
					it.Date >= this.from &&
					it.Date <= this.to &&
					it.PerDiemReportId === null;
			},
			{
				selectedUser: viewModel.selectedUser(),
				from: viewModel.perDiemReport().From(),
				to: viewModel.perDiemReport().To()
			});
	return userTimeEntryQuery
		.orderBy("it.Date")
		.orderBy("it.From")
		.toArray(viewModel.timeEntries)
		.then(function() {
			var userExpenseQuery = window.database.CrmPerDiem_UserExpense.include("ResponsibleUserObject");
			userExpenseQuery = id
				? userExpenseQuery
				.filter(function(it) { return it.PerDiemReportId === this.id; }, { id: id })
				: userExpenseQuery
				.filter(function(it) {
					return it.ResponsibleUser === this.selectedUser &&
							it.IsClosed === 0 &&
							it.Date >= this.from &&
							it.Date <= this.to &&
							it.PerDiemReportId === null;
					},
					{
						selectedUser: viewModel.selectedUser(),
						from: viewModel.perDiemReport().From(),
						to: viewModel.perDiemReport().To()
					});
			return userExpenseQuery
				.orderBy("it.Date")
				.toArray(viewModel.expenses);
		});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryCloseModalViewModel.prototype.dispose = function() {
	var viewModel = this;
	window.database.detach(viewModel.perDiemReport().innerInstance);
};
namespace("Crm.PerDiem.ViewModels").TimeEntryCloseModalViewModel.prototype.getDistinctDates = function () {
	var viewModel = this;
	viewModel.loading(true);
	var queries = [];
	viewModel.distinctDates([]);
	var saveDates = function (dates) {
		dates.forEach(function (date) {
			if (viewModel.distinctDates.indexOf(date) === -1) {
				viewModel.distinctDates.push(date);
			}
		});
	};
	queries.push({
		queryable: window.database.CrmPerDiem_UserExpense.GetDistinctUserExpenseDates(viewModel.selectedUser()),
		method: "toArray",
		handler: saveDates
	});
	queries.push({
		queryable: window.database.CrmPerDiem_UserTimeEntry.GetDistinctUserTimeEntryDates(viewModel.selectedUser()),
		method: "toArray",
		handler: saveDates
	});
	return Helper.Batch.Execute(queries).then(function () {
		viewModel.loading(false);
	});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryCloseModalViewModel.prototype.save = async function() {
	var viewModel = this;
	viewModel.loading(true);

	if (viewModel.errors().length > 0) {
		viewModel.loading(false);
		viewModel.errors.showAllMessages();
		return;
	}

	viewModel.perDiemReport().UserName(viewModel.selectedUser());
	if (viewModel.perDiemReport().StatusKey() === "Open") {
		let existingReport = await window.database.CrmPerDiem_PerDiemReport
			.filter(function (report) {
					return report.StatusKey === "Open" &&
						report.From === this.from &&
						report.To === this.to &&
						report.UserName === this.selectedUser
				},
				{
					selectedUser: viewModel.selectedUser(),
					from: viewModel.perDiemReport().From(),
					to: viewModel.perDiemReport().To()
				}).toArray();
		if (existingReport[0]) {
			window.database.remove(viewModel.perDiemReport().innerInstance);
			viewModel.perDiemReport(existingReport[0].asKoObservable());
		}
	}
	viewModel.reportEntries().forEach(function(reportEntry) {
		window.database.attachOrGet(reportEntry.innerInstance);
		if (viewModel.perDiemReport().StatusKey() === "Closed") {
			reportEntry.IsClosed(true);
		}
		if (viewModel.perDiemReport().StatusKey() === "Open") {
			reportEntry.IsClosed(false);
		}
		reportEntry.PerDiemReportId(viewModel.perDiemReport().Id());
	});

	if (viewModel.perDiemReport().StatusKey() === "Closed") {
		viewModel.perDiemReport().ApprovedBy(viewModel.currentUser().Id);
		viewModel.perDiemReport().ApprovedDate(new Date());
	} else {
		viewModel.perDiemReport().ApprovedBy(null);
		viewModel.perDiemReport().ApprovedDate(null);
	}

	window.database.saveChanges().then(function() {
		viewModel.loading(false);
		viewModel.refreshParentViewModel();
		$(".modal:visible").modal("hide");
	}).fail(function() {
		viewModel.loading(false);
		window.swal(window.Helper.String.getTranslatedString("UnknownError"),
			window.Helper.String.getTranslatedString("Error_InternalServerError"),
			"error");
	});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryCloseModalViewModel.prototype.getFromDate = function(date) {
	var viewModel = this;
	if (!date || !window.moment(date).isValid()) {
		return null;
	}
	switch (viewModel.perDiemReportTypeKey()) {
		case "Custom":
			return window.moment(date).startOf("day").toDate();
		case "Monthly":
			return window.moment(date).startOf("month").toDate();
		case "Weekly":
			return window.moment(date).startOf("isoWeek").toDate();
		default:
			return date;
	}
};
namespace("Crm.PerDiem.ViewModels").TimeEntryCloseModalViewModel.prototype.getToDate = function(date) {
	var viewModel = this;
	if (!date || !window.moment(date).isValid()) {
		return null;
	}
	switch (viewModel.perDiemReportTypeKey()) {
		case "Custom":
			return window.moment(date).endOf("day").toDate();
		case "Monthly":
			return window.moment(date).endOf("month").toDate();
		case "Weekly":
			return window.moment(date).endOf("isoWeek").toDate();
		default:
			return date;
	}
};
namespace("Crm.PerDiem.ViewModels").TimeEntryCloseModalViewModel.prototype.getCalendarWeek = function (date) {
	let momentDate = window.moment(date);
	return (momentDate.week() + "").padStart(2, '0') + "/" + momentDate.year();
}