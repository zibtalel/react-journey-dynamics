;
(function(ko) {
	var perDiemReportDetailsModalViewModel = window.Crm.PerDiem.ViewModels.PerDiemReportDetailsModalViewModel;
	window.Crm.PerDiem.ViewModels.PerDiemReportDetailsModalViewModel = function() {
		perDiemReportDetailsModalViewModel.apply(this, arguments);
		var viewModel = this;
		viewModel.perDiemAllowanceEntries = ko.observableArray([]);
		var reportEntries = viewModel.reportEntries;
		viewModel.reportEntries = ko.pureComputed(function() {
			return reportEntries().concat(viewModel.perDiemAllowanceEntries());
		});
		viewModel.lookups.perDiemAllowances = {};
		viewModel.lookups.adjustments = {};
	};
	window.Crm.PerDiem.ViewModels.PerDiemReportDetailsModalViewModel.prototype = perDiemReportDetailsModalViewModel.prototype;

	var perDiemReportDetailsModalViewModelLoadReportEntries =
		window.Crm.PerDiem.ViewModels.PerDiemReportDetailsModalViewModel.prototype.loadReportEntries;
	window.Crm.PerDiem.ViewModels.PerDiemReportDetailsModalViewModel.prototype.loadReportEntries = function(id, params) {
		var viewModel = this;
		return perDiemReportDetailsModalViewModelLoadReportEntries.apply(this, arguments).then(function() {
			var filter = id
				? function(it) { return it.PerDiemReportId === this.id; }
				: function(it) { return it.ResponsibleUser === this.username && it.Date >= this.fromDate && it.Date <= this.toDate; };
			var filterParams = id ? { id: id } : params;
			return window.database.CrmPerDiemGermany_PerDiemAllowanceEntry
				.include("AdjustmentReferences")
				.filter(filter, filterParams)
				.orderBy("it.Date")
				.toArray(viewModel.perDiemAllowanceEntries);
		}).then(function() {
			var perDiemAllowanceKeys = window._.uniq(viewModel.perDiemAllowanceEntries().map(function(x) {
				return x.PerDiemAllowanceKey();
			}));
			return window.Helper.Lookup
				.getLocalizedArrayMap("CrmPerDiemGermany_PerDiemAllowance",
					null,
					function(it) { return it.Key in this.perDiemAllowanceKeys; },
					{ perDiemAllowanceKeys: perDiemAllowanceKeys });
		}).then(function(lookups) {
			viewModel.lookups.perDiemAllowances = lookups;
			return window.Helper.Lookup
				.getLocalizedArrayMap("CrmPerDiemGermany_PerDiemAllowanceAdjustment");
		})
			.then(function (adjustments) {
				viewModel.lookups.adjustments = adjustments;
			});
	};
})(window.ko);