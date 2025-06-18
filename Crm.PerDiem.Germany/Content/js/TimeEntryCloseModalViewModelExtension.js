;
(function (helper, ko, moment) {
	var timeEntryCloseModalViewModel = window.Crm.PerDiem.ViewModels.TimeEntryCloseModalViewModel;
	window.Crm.PerDiem.ViewModels.TimeEntryCloseModalViewModel = function () {
		timeEntryCloseModalViewModel.apply(this, arguments);
		var viewModel = this;
		viewModel.lookups.adjustments = {};
		viewModel.lookups.perDiemAllowances = {};
		viewModel.perDiemAllowanceEntries = ko.observableArray([]);
		var reportEntries = viewModel.reportEntries;
		viewModel.reportEntries = ko.pureComputed(function () {
			return reportEntries().concat(viewModel.perDiemAllowanceEntries());
		});
	};
	window.Crm.PerDiem.ViewModels.TimeEntryCloseModalViewModel.prototype = timeEntryCloseModalViewModel.prototype;

	var timeEntryCloseModalViewModelInit = window.Crm.PerDiem.ViewModels.TimeEntryCloseModalViewModel.prototype.init;
	window.Crm.PerDiem.ViewModels.TimeEntryCloseModalViewModel.prototype.init = function (id) {
		var viewModel = this;
		return timeEntryCloseModalViewModelInit.apply(this, arguments)
			.then(function () {
				return window.database.CrmPerDiemGermany_PerDiemAllowanceEntry.GetDistinctPerDiemAllowanceEntryDates(viewModel.currentUser().Id).forEach(function (date) {
					if (viewModel.distinctDates.indexOf(date) === -1) {
						viewModel.distinctDates.push(date);
					}
				});
			})
			.then(function () {
				var perDiemAllowanceKeys = window._.uniq(viewModel.perDiemAllowanceEntries().map(function (x) {
					return x.PerDiemAllowanceKey();
				}));
				return window.Helper.Lookup
					.getLocalizedArrayMap("CrmPerDiemGermany_PerDiemAllowance",
						null,
						function (it) { return it.Key in this.perDiemAllowanceKeys; },
						{ perDiemAllowanceKeys: perDiemAllowanceKeys });
				return window.Helper.Lookup
					.getLocalizedArrayMap("CrmPerDiemGermany_PerDiemAllowance",
						null,
						function (it) { return it.Key in this.perDiemAllowanceKeys; },
						{ perDiemAllowanceKeys: perDiemAllowanceKeys });
			})
			.then(function (perDiemAllowances) {
				viewModel.lookups.perDiemAllowances = perDiemAllowances;
				return window.Helper.Lookup
					.getLocalizedArrayMap("CrmPerDiemGermany_PerDiemAllowanceAdjustment");
			})
			.then(function (adjustments) {
				viewModel.lookups.adjustments = adjustments;
			});
	};

	var timeEntryCloseModalViewModelRefresh =
		window.Crm.PerDiem.ViewModels.TimeEntryCloseModalViewModel.prototype.refresh;
	window.Crm.PerDiem.ViewModels.TimeEntryCloseModalViewModel.prototype.refresh = function () {
		var viewModel = this;
		var id = viewModel.perDiemReportId();
		return timeEntryCloseModalViewModelRefresh.apply(this, arguments).then(function () {
			if (!viewModel.perDiemReport().From() || !viewModel.perDiemReport().To()) {
				viewModel.perDiemAllowanceEntries([]);
				return new $.Deferred().resolve().promise();
			}
			var query = window.database.CrmPerDiemGermany_PerDiemAllowanceEntry
				.include("AdjustmentReferences")
				.include("ResponsibleUserObject");
			query = id
				? query.filter(function (it) {
					return it.PerDiemReportId === this.id;
				},
					{ id: id })
				: query.filter(function (it) {
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
			return query
				.orderBy("it.Date")
				.toArray(viewModel.perDiemAllowanceEntries);
		});
	};
})(window.Helper, window.ko, window.moment);