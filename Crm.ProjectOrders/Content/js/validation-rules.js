; (function (ko, Helper) {
  ko.validationRules.add("CrmOrder_Offer", function (entity) {
		entity.ExtensionValues().ProjectId.extend({
			required: {
				message: Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", Helper.String.getTranslatedString("Project")),
				params: true,
				onlyIf: function() {
					return entity.ContactId() !== null;
				}
			}
		});
	});
  ko.validationRules.add("CrmOrder_Order", function (entity) {
		entity.ExtensionValues().ProjectId.extend({
			required: {
				message: Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", Helper.String.getTranslatedString("Project")),
				params: true,
				onlyIf: function() {
					return entity.ContactId() !== null;
				}
			}
		});
	});
})(window.ko, window.Helper);