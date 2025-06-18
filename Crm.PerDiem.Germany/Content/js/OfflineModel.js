window.Helper.Database.registerTable("CrmPerDiemGermany_PerDiemAllowanceEntry",
	{
		PerDiemReport: {
			type: "Crm.Offline.DatabaseModel.CrmPerDiem_PerDiemReport",
			inverseProperty: "$$unbound",
			keys: ["PerDiemReportId"]
		},
		ResponsibleUserObject: {
			type: "Crm.Offline.DatabaseModel.Main_User",
			inverseProperty: "$$unbound",
			keys: ["ResponsibleUser"]
		},
		AdjustmentReferences: { type: Array, elementType: "Crm.Offline.DatabaseModel.CrmPerDiemGermany_PerDiemAllowanceEntryAllowanceAdjustmentReference", inverseProperty: "$$unbound", defaultValue: [], keys: ["PerDiemAllowanceEntryKey"] },
	});

//window.Helper.Database.registerTable("CrmPerDiemGermany_PerDiemAllowanceEntryAllowanceAdjustmentReference");

window.Helper.Database.setTransactionId("CrmPerDiemGermany_PerDiemAllowanceEntry",
	function (response) {
		return new $.Deferred().resolve([response.PerDiemReportId, response.Id]).promise();
	});

window.Helper.Database.setTransactionId("CrmPerDiemGermany_PerDiemAllowanceEntryAllowanceAdjustmentReference",
	function (response) {
		return new $.Deferred().resolve(response.PerDiemAllowanceEntryKey).promise();
	});







