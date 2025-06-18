;
(function(helper, ko, moment) {
	var timeEntryIndexViewModel = window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel;
	window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel = function() {
		timeEntryIndexViewModel.apply(this, arguments);
		var viewModel = this;
		viewModel.perDiemAllowanceEntries = ko.observableArray([]);
		viewModel.perDiemAllowances = ko.observableArray([]);
		viewModel.canAddPerDiemAllowanceEntry = ko.computed(function() {
			return viewModel.canAddExpense() &&
				viewModel.perDiemAllowanceEntries().some(function(x) {
					return moment(x.Date()).isSame(viewModel.selectedDate());
				}) === false;
		});
	};
	window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype = timeEntryIndexViewModel.prototype;

	var timeEntryIndexViewModelInit = window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype.init;
	window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype.init = function() {
		var viewModel = this;
		return timeEntryIndexViewModelInit.apply(this, arguments)
			.then(function () {
				return window.Helper.Lookup.getLocalizedArrayMap("CrmPerDiemGermany_PerDiemAllowanceAdjustment")
			})
			.then(function (adjustments) {
				viewModel.lookups.adjustments = adjustments;
			})
			.then(function () {
			helper.Database.registerEventHandlers(viewModel,
				{
					"CrmPerDiemGermany_PerDiemAllowanceEntry": {
						"afterCreate": viewModel.refresh,
						"afterDelete": viewModel.refresh,
						"afterUpdate": viewModel.refresh
					}
				});
		});
	};

	var timeEntryIndexViewModelRefresh = window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype.refresh;
	window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype.refresh = function() {
		var viewModel = this;
		return timeEntryIndexViewModelRefresh.apply(this, arguments).then(function() {
			return window.database.CrmPerDiemGermany_PerDiemAllowanceEntry
				.include("AdjustmentReferences")
				.filter(function(it) {
						return it.ResponsibleUser === this.username &&
							it.Date >= this.dateMin &&
							it.Date <= this.dateMax;
					},
					{
						dateMin: viewModel.dateFilterFrom(),
						dateMax: viewModel.dateFilterTo(),
						username: viewModel.username()
					})
				.toArray(viewModel.perDiemAllowanceEntries);
		}).then(function() {
			var perDiemAllowanceKeys = window._.uniq(viewModel.perDiemAllowanceEntries().map(function(x) {
				return x.PerDiemAllowanceKey();
			}));
			return window.database.CrmPerDiemGermany_PerDiemAllowance
				.filter(function(it) { return it.Key in this.keys; }, { keys: perDiemAllowanceKeys })
				.toArray(viewModel.perDiemAllowances);
		});
	};

	var timeEntryIndexViewModelGetCurrencyKeys =
		window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype.getCurrencyKeys;
	window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype.getCurrencyKeys = function() {
		var viewModel = this;
		return window._.uniq(viewModel.perDiemAllowanceEntries().map(function(x) {
			return x.CurrencyKey();
		}).concat(timeEntryIndexViewModelGetCurrencyKeys.apply(this, arguments)));
	};

	var timeEntryIndexViewModelGetExpensesForDate =
		window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype.getExpensesForDate;
	window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype.getExpensesForDate = function(date) {
		var viewModel = this;
		return viewModel.perDiemAllowanceEntries().filter(function(x) {
			return moment(x.Date()).isSame(date);
		}).concat(timeEntryIndexViewModelGetExpensesForDate.apply(this, arguments));
	};

	
	window.Crm.PerDiem.ViewModels.TimeEntryIndexViewModel.prototype.deletePerDiemAllowanceEntry = function (perDiemAllowanceEntry) {
		var viewModel = this;
		if (perDiemAllowanceEntry.IsClosed()) {
			return;
		}
		window.Helper.Confirm.confirmDelete().done(function () {
			viewModel.loading(true);
			perDiemAllowanceEntry.AdjustmentReferences().forEach(function (reference) {
				window.database.remove(reference)
			})
			window.database.remove(perDiemAllowanceEntry);
			return window.database.saveChanges();
		});
	};
})(window.Helper, window.ko, window.moment);