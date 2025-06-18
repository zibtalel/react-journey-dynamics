function setupPerDiemGermany() {

	function applyExpenseFilter(query, userId) {
		return query.filter(function(it) {
			return it.ResponsibleUser === this.currentUser && (it.IsClosed === 0 && it.PerDiemReportId === null);
		}, { currentUser: userId });
	}

	function configureOdataGetPerDiemAllowanceEntryDistinctDates() {
		if (!window.database.CrmPerDiemGermany_PerDiemAllowanceEntry) {
			return;
		}
		if (!window.database.CrmPerDiemGermany_PerDiemAllowanceEntry.GetDistinctPerDiemAllowanceEntryDates) {
			throw "CrmPerDiemGermany_PerDiemAllowanceEntry.GetDistinctPerDiemAllowanceEntryDates must be defined at this point";
		}
		const origGetDistinctPerDiemAllowanceEntryDates = window.database.CrmPerDiemGermany_PerDiemAllowanceEntry.GetDistinctPerDiemAllowanceEntryDates;
		window.database.CrmPerDiemGermany_PerDiemAllowanceEntry.GetDistinctPerDiemAllowanceEntryDates = function(userId) {
			return applyExpenseFilter(origGetDistinctPerDiemAllowanceEntryDates.call(this), userId);
		};
	}

	function configureWebSqlGetPerDiemAllowanceEntryDistinctDates() {
		if (!window.database.CrmPerDiemGermany_PerDiemAllowanceEntry) {
			return;
		}
		if (window.database.CrmPerDiemGermany_PerDiemAllowanceEntry.GetDistinctPerDiemAllowanceEntryDates) {
			throw "CrmPerDiemGermany_PerDiemAllowanceEntry.GetDistinctPerDiemAllowanceEntryDates must be undefined at this point";
		}
		window.database.CrmPerDiemGermany_PerDiemAllowanceEntry.GetDistinctPerDiemAllowanceEntryDates = function(userId) {
			return applyExpenseFilter(window.database.CrmPerDiemGermany_PerDiemAllowanceEntry, userId)
				.map(function(it) { return it.Date; })
				.distinct();
		};
	}

	document.addEventListener("DatabaseInitialized", function() {
		if (window.database.storageProvider.name === "webSql") {
			configureWebSqlGetPerDiemAllowanceEntryDistinctDates();
		} else {
			configureOdataGetPerDiemAllowanceEntryDistinctDates();
		}
	});
}

setupPerDiemGermany();