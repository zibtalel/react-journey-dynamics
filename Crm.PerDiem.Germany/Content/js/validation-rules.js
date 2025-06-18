;
(function(ko) {
	ko.validationRules.add("CrmPerDiemGermany_PerDiemAllowanceEntry",
		function(entity) {
			entity.Date.extend({
				validation: {
					async: true,
					validator: function(val, params, callback) {
						window.database.CrmPerDiemGermany_PerDiemAllowanceEntry
							.filter(function(it) {
									return it.ResponsibleUser === this.responsibleUser &&
										it.Date === this.date &&
										it.Id !== this.id;
								},
								{
									id: entity.Id(),
									responsibleUser: entity.ResponsibleUser(),
									date: entity.Date()
								})
							.toArray(function(results) {
								if (results.length > 0) {
									callback({
										isValid: false,
										message: window.Helper.String.getTranslatedString(
											"PerDiemAllowanceExistingForDate")
									});
								} else {
									callback(true);
								}
							}).fail(function() {
								callback(true);
							});
					}
				}
			});
		});
})(window.ko);