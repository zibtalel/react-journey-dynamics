/// <reference path="../../../../Content/js/knockout.custom.material.js" />
/// <reference path="../../../../Content/js/knockout.custom.validation.js" />
;
; (function (ko, Helper) {
	ko.validationRules.add("CrmProject_Project", function (entity) {
		ko.validation.addRule(entity.CategoryKey, {
			rule: "required",
			message: Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", Helper.String.getTranslatedString("Category")),
			params: true
		});
		ko.validation.addRule(entity.ParentId, {
			rule: "required",
			message: Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", Helper.String.getTranslatedString("ParentId")),
			params: true
		});
		entity.Value.extend({
			validation: [
				{
					rule: "min",
					params: 0,
					message: Helper.String.getTranslatedString("RuleViolation.NotNegative").replace("{0}", Helper.String.getTranslatedString("Value"))
				},
				{
					rule: "max",
					params: 100000000000000000,
					message: window.Helper.String.getTranslatedString("RuleViolation.MaxValue").replace("{0}", window.Helper.String.getTranslatedString("Value"))
				}
			]
		});
		entity.ProjectNo.extend({
			unique: {
				params: [window.database.CrmProject_Project, 'ProjectNo', entity.Id],
				message: Helper.String.getTranslatedString("RuleViolation.Unique")
					.replace("{0}", Helper.String.getTranslatedString("ProjectNo"))
			}
		});
		ko.validation.addRule(entity.CurrencyKey, {
			rule: "required",
			message: Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", Helper.String.getTranslatedString("Currency")),
			params: true
		});
		ko.validation.addRule(entity.Name, {
			rule: "maxLength",
			message: window.Helper.String.getTranslatedString("RuleViolation.MaxLength").replace("{0}", window.Helper.String.getTranslatedString("Name")),
			params: 450
		});
	});
	ko.validationRules.add("CrmProject_Potential", function (entity) {
		ko.validation.addRule(entity.SourceTypeKey, {
			rule: "required",
			message: Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", Helper.String.getTranslatedString("SourceTypeKey")),
			params: true
		});
		entity.PotentialNo.extend({
			unique: {
				params: [window.database.CrmProject_Potential, 'PotentialNo', entity.Id],
				message: Helper.String.getTranslatedString("RuleViolation.Unique")
					.replace("{0}", Helper.String.getTranslatedString("PotentialNo"))
			}
		});
	});
	ko.validationRules.add("CrmProject_DocumentEntry", function (entity) {
		ko.validation.addRule(entity.PersonKey, {
			rule: "required",
			message: Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", Helper.String.getTranslatedString("Person")),
			params: true
		});
		ko.validation.addRule(entity.DocumentKey, {
			rule: "required",
			message: Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", Helper.String.getTranslatedString("Document")),
			params: true
		});
	});
})(window.ko, window.Helper);
