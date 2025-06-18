/// <reference path="../../../../Content/js/namespace.js" />
/// <reference path="../../../../Content/js/system/knockout-3.5.1.js" />
/// <reference path="../../../../Content/js/system/knockout.validation.js" />
/// <reference path="../../../../Content/js/knockout.custom.js" />


namespace("Crm.PerDiem.OfflineModel").UserTimeEntries = function () {
	return window.ko.observableArray([]).config({ storage: "CrmPerDiem_UserTimeEntry", model: "UserTimeEntry", pluginName: "Crm.PerDiem" });
};

namespace("Crm.PerDiem.OfflineModel").UserTimeEntry = function () {
	return window.ko.validatedObservable(window.Helper.Database.createInstance("UserTimeEntry", "Crm.PerDiem"))
		.config({ storage: "CrmPerDiem_UserTimeEntry", model: "UserTimeEntry", pluginName: "Crm.PerDiem" });
};

namespace("Crm.PerDiem.OfflineModel").TimeEntryTypes = function () {
	return window.ko.observableArray([]).config({ storage: "CrmPerDiem_TimeEntryType", model: "TimeEntryType", pluginName: "Crm.PerDiem" });
};

namespace("Crm.OfflineModel").CostCenters = function () {
	return window.ko.observableArray([]).config({ storage: "Main_CostCenter", model: "CostCenter", pluginName: "Main" });
};

namespace("Crm.PerDiem.OfflineModel").UserExpenses = function () {
	return window.ko.observableArray([]).config({ storage: "CrmPerDiem_UserExpense", model: "UserExpense", pluginName: "Crm.PerDiem" });
};

namespace("Crm.PerDiem.OfflineModel").UserExpense = function () {
	var expense = window.Helper.Database.createInstance("UserExpense", "Crm.PerDiem");
	expense.Amount.extend({
		validation: {
			validator: function (val) {
				return val !== "0" && val !== 0;
			},
			message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Amount"))
		}
	});
	expense.Amount.extend({
		max: 9999999
	});
	return window.ko.validatedObservable(expense)
		.config({ storage: "CrmPerDiem_UserExpense", model: "UserExpense", pluginName: "Crm.PerDiem" });
};

namespace("Crm.PerDiem.OfflineModel").ExpenseTypes = function () {
	return window.ko.observableArray([]).config({ storage: "CrmPerDiem_ExpenseType", model: "ExpenseType", pluginName: "Crm.PerDiem" });
};

namespace("Crm.PerDiem.OfflineModel").PerDiemReport = function () {
	var perDiemReport = window.Helper.Database.createInstance("PerDiemReport", "Crm.PerDiem");
	perDiemReport.TypeKey.extend({
		required: {
			params: true,
			message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Type"))
		}
	});
	perDiemReport.StatusKey.extend({
		required: {
			params: true,
			message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Status"))
		}
	});
	perDiemReport.From.extend({
		required: {
			params: true,
			message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Period"))
		}
	});
	perDiemReport.To.extend({
		required: {
			params: true,
			message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Period"))
		}
	});
	return window.ko.validatedObservable(perDiemReport)
		.config({ storage: "CrmPerDiem_PerDiemReport", model: "PerDiemReport", pluginName: "Crm.PerDiem" });
};

namespace("Crm.PerDiem.OfflineModel").PerDiemReports = function () {
	return window.ko.observableArray([]).config({ storage: "CrmPerDiem_PerDiemReport", model: "PerDiemReport", pluginName: "Crm.PerDiem" });
};

namespace("Crm.PerDiem.OfflineModel").PerDiemReportStatuses = function () {
	return window.ko.observableArray([]).config({ storage: "CrmPerDiem_PerDiemReportStatus", model: "PerDiemReportStatus", pluginName: "Crm.PerDiem" });
};

namespace("Crm.PerDiem.OfflineModel").PerDiemReportTypes = function () {
	return window.ko.observableArray([]).config({ storage: "CrmPerDiem_PerDiemReportType", model: "PerDiemReportType", pluginName: "Crm.PerDiem" });
};
window.Helper.Database.registerTable("CrmPerDiem_PerDiemReport", {
	User: { type: "Crm.Offline.DatabaseModel.Main_User", inverseProperty: "$$unbound", defaultValue: null, keys: ["UserName"] },
});
window.Helper.Database.registerTable("CrmPerDiem_UserExpense", {
	FileResource: { type: "Crm.Offline.DatabaseModel.Main_FileResource", inverseProperty: "$$unbound", keys: ["FileResourceKey"] },
	PerDiemReport: { type: "Crm.Offline.DatabaseModel.CrmPerDiem_PerDiemReport", inverseProperty: "$$unbound", keys: ["PerDiemReportId"] },
	ResponsibleUserObject: { type: "Crm.Offline.DatabaseModel.Main_User", inverseProperty: "$$unbound", defaultValue: null, keys: ["ResponsibleUser"] }
});
window.Helper.Database.registerTable("CrmPerDiem_UserTimeEntry", {
	PerDiemReport: { type: "Crm.Offline.DatabaseModel.CrmPerDiem_PerDiemReport", inverseProperty: "$$unbound", keys: ["PerDiemReportId"] },
	ResponsibleUserObject: { type: "Crm.Offline.DatabaseModel.Main_User", inverseProperty: "$$unbound", defaultValue: null, keys: ["ResponsibleUser"] }
});
window.Helper.Database.setTransactionId("CrmPerDiem_PerDiemReport",
	function (perDiemReport) {
		return new $.Deferred().resolve(perDiemReport.Id).promise();
	});
window.Helper.Database.setTransactionId("CrmPerDiem_UserExpense",
	function (expense) {
		return new $.Deferred().resolve([expense.PerDiemReportId, expense.FileResourceKey]).promise();
	});
window.Helper.Database.setTransactionId("CrmPerDiem_UserTimeEntry",
	function (timeEntry) {
		return new $.Deferred().resolve(timeEntry.PerDiemReportId).promise();
	});