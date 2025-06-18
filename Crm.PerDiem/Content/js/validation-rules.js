; (function (ko) {
	ko.validationRules.add("CrmPerDiem_UserExpense", function (entity) {
		entity.Amount.extend({
			validation:
				[
					{
						validator: function (val) {
							return val && val !== 0;
						},
						message: window.Helper.String.getTranslatedString("RuleViolation.Required")
							.replace("{0}", Helper.String.getTranslatedString("Amount"))
					},
					{
						validator: function (val) {
							return val >= 0;
						},
						message: window.Helper.String.getTranslatedString("RuleViolation.NotNegative")
							.replace("{0}", Helper.String.getTranslatedString("Amount"))
					},
					{
						rule: "max",
						params: 10000000,
						message: window.Helper.String.getTranslatedString("RuleViolation.MaxValue").replace("{0}", window.Helper.String.getTranslatedString("Amount"))
					}
				]
		});
		entity.Date.extend({
			required: {
				params: true,
				message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Date"))
			}
		});
		entity.CurrencyKey.extend({
			required: {
				params: true,
				message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Currency"))
			}
		});
		entity.ExpenseTypeKey.extend({
			required: {
				params: true,
				message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("ExpenseType"))
			}
		});
	});
	ko.validationRules.add("CrmPerDiem_PerDiemReport", function(entity) {
		entity.StatusKey.extend({
			required: {
				params: true,
				message: window.Helper.String.getTranslatedString("RuleViolation.Required")
					.replace("{0}", window.Helper.String.getTranslatedString("Status"))
			}
		});
	});
	ko.validationRules.add("CrmPerDiem_UserTimeEntry", function (entity) {
		entity.Date.extend({
			required: {
				params: true,
				message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Date"))
			}
		});
		entity.Duration.extend({
			validation: {
				validator: function(val) {
					return val && window.moment.duration(val).isValid() && window.moment.duration(val).asMinutes() > 0;
				},
				message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Duration"))
			}
		});
		entity.From.extend({
			validation: {
				async: true,
				validator: window.OverlappingTimeEntryValidator.bind(entity)
			}
		});
		entity.TimeEntryTypeKey.extend({
			required: {
				params: true,
				message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("TimeEntryType"))
			}
		});
	});
	ko.validationRules.add("Main_User", function (entity) {
		let extensionValues = ko.unwrap(entity.ExtensionValues);
		if (extensionValues) {
			extensionValues.WorkingHoursPerDay.extend({
				validation: [
					{
						validator: function (val) {
							return val > 0;
						},
						message: window.Helper.String.getTranslatedString("RuleViolation.Greater")
							.replace("{0}", Helper.String.getTranslatedString("WorkingHoursPerDay"))
							.replace("{1}", "0")
					},
					{
						validator: function (val) {
							return val <= 24;
						},
						message: window.Helper.String.getTranslatedString("RuleViolation.LessOrEqual")
							.replace("{0}", window.Helper.String.getTranslatedString("WorkingHoursPerDay")).replace("{1}", 24)
					}
				]
			});
		}
	});
})(window.ko);