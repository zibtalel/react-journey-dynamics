class HelperPerDiem {
	static queryPerDiemReportStatus(query, term) {
		if (!window.AuthorizationManager.isAuthorizedForAction("PerDiemReportStatus", "SelectNonMobileLookupValues")) {
			query = query.filter("it.ShowInMobileClient === true");
		}
		if(!window.AuthorizationManager.isAuthorizedForAction("PerDiemReport", "CloseReport"))
			query = query.filter("it.Key !== 'Closed'");
		if(!window.AuthorizationManager.isAuthorizedForAction("PerDiemReport", "RequestCloseReport"))
			query = query.filter("it.Key !== 'RequestClose'");
		return Helper.Lookup.queryLookup(query, term);
	}
	
	static autocompleterOptionsToCostCenter(tableName, validCostCenters){
		return {
			table: tableName,
			mapDisplayObject: window.Helper.Lookup.mapLookupForSelect2Display,
			getElementByIdQuery: window.Helper.Lookup.getLookupByKeyQuery,
			customFilter: function (query, term) {
				query = query.filter("it.Key in this.selectedValidCostCenter",
					{ selectedValidCostCenter: ko.unwrap(validCostCenters) });

				return window.Helper.Lookup.queryLookup(query, term);
			}
		}
	}

	static autocompleterOptionsToPerDiemLookups(tableName, onSelectedItem){
		return {
			table: tableName,
			mapDisplayObject: window.Helper.Lookup.mapLookupForSelect2Display,
			getElementByIdQuery: window.Helper.Lookup.getLookupByKeyQuery,
			onSelect: onSelectedItem,
			customFilter: function (query, term) {
				if(tableName === "CrmPerDiem_TimeEntryType") {
					if (!window.AuthorizationManager.isAuthorizedForAction("TimeEntryType", "SelectNonMobileLookupValues")) {
						query = query.filter("it.ShowInMobileClient === true");
					}
				}

				return window.Helper.Lookup.queryLookup(query, term);
			}
		}
	}
}

(window.Helper = window.Helper || {}).PerDiem = HelperPerDiem;

document.addEventListener("DatabaseInitialized", function () {

	function applyExpenseFilter(query, userId) {
		return query.filter(function (it) {
			return it.ResponsibleUser === this.currentUser
				&& (it.IsClosed === 0 && it.PerDiemReportId === null);
		}, {currentUser: userId});
	}

	function applyTimeEntryFilter(query, userId) {
		return query.filter(function (it) {
			return it.ResponsibleUser === this.currentUser
				&& (it.IsClosed === 0 && it.PerDiemReportId === null);
		}, {currentUser: userId});
	}

	function configureWebSqlGetUserExpenseDistinctDates() {
		if (!window.database.CrmPerDiem_UserExpense) {
			return;
		}
		if (window.database.CrmPerDiem_UserExpense.GetDistinctUserExpenseDates) {
			throw "CrmPerDiem_UserExpense.GetDistinctUserExpenseDates must be undefined at this point";
		}
		window.database.CrmPerDiem_UserExpense.GetDistinctUserExpenseDates = function (userId) {
			return applyExpenseFilter(window.database.CrmPerDiem_UserExpense, userId)
				.filter(function (item) {
					return item.PerDiemReportId === null
				})
				.map(function (it) {
					return it.Date;
				})
				.distinct();
		};
	}

	function configureWebSqlGetTimeEntryDistinctDates() {
		if (!window.database.CrmPerDiem_UserTimeEntry) {
			return;
		}
		if (window.database.CrmPerDiem_UserTimeEntry.GetDistinctUserTimeEntryDates) {
			throw "CrmPerDiem_UserTimeEntry.GetDistinctUserTimeEntryDates must be undefined at this point";
		}
		window.database.CrmPerDiem_UserTimeEntry.GetDistinctUserTimeEntryDates = function (userId) {
			return applyTimeEntryFilter(window.database.CrmPerDiem_UserTimeEntry, userId)
				.filter(function (item) {
					return item.PerDiemReportId === null
				})
				.map(function (it) {
					return it.Date;
				})
				.distinct();
		};
	}

	function configureOdataGetUserExpenseDistinctDates() {
		if (!window.database.CrmPerDiem_UserExpense) {
			return;
		}
		if (!window.database.CrmPerDiem_UserExpense.GetDistinctUserExpenseDates) {
			throw "CrmPerDiem_UserExpense.GetDistinctUserExpenseDates must be defined at this point";
		}
		const origGetDistinctUserExpenseDates = window.database.CrmPerDiem_UserExpense.GetDistinctUserExpenseDates;
		window.database.CrmPerDiem_UserExpense.GetDistinctUserExpenseDates = function (userId) {
			return applyExpenseFilter(origGetDistinctUserExpenseDates.call(this), userId);
		};
	}

	function configureOdataGetTimeEntryDistinctDates() {
		if (!window.database.CrmPerDiem_UserTimeEntry) {
			return;
		}
		if (!window.database.CrmPerDiem_UserTimeEntry.GetDistinctUserTimeEntryDates) {
			throw "CrmPerDiem_UserTimeEntry.GetDistinctUserTimeEntryDates must be defined at this point";
		}
		const origGetDistinctUserTimeEntryDates = window.database.CrmPerDiem_UserTimeEntry.GetDistinctUserTimeEntryDates;
		window.database.CrmPerDiem_UserTimeEntry.GetDistinctUserTimeEntryDates = function (userId) {
			return applyTimeEntryFilter(origGetDistinctUserTimeEntryDates.call(this), userId);
		};
	}

	if (window.database.storageProvider.name === "webSql") {
		configureWebSqlGetUserExpenseDistinctDates();
		configureWebSqlGetTimeEntryDistinctDates();
	} else {
		configureOdataGetUserExpenseDistinctDates();
		configureOdataGetTimeEntryDistinctDates();
	}
});
