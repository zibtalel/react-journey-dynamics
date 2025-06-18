namespace("Crm.Configurator.Rules.Restriction").getAlertCssClass = function() {
	return "alert alert-danger";
}
namespace("Crm.Configurator.Rules.Restriction").getAlertMessage = function (ruleMatches) {
  if (!ruleMatches || ruleMatches.length === 0) {
    return window.Helper.String.getTranslatedString("DefaultRestrictionRuleAlertMessage");
  }
  return window.Helper.String.getTranslatedString("RestrictionRuleAlertMessage");
}
namespace("Crm.Configurator.Rules.Restriction").applyAdd = function(articleIds, rule) {
	rule.AffectedVariableValues.forEach(function(articleId) {
		var indexOf = articleIds.indexOf(articleId);
		if (indexOf !== -1) {
			articleIds.splice(indexOf, 1);
			}
		});
	return articleIds;
}
namespace("Crm.Configurator.Rules.Restriction").applyRemove = function(articleIds, rule) {
	return articleIds;
}
namespace("Crm.Configurator.Rules.Restriction").getAddRuleMatches = function(articleId, ruleArticleIds, currentArticleIds) {
	var ruleMatches = [];
	ruleArticleIds.forEach(function(x) {
		if (x !== articleId && currentArticleIds.indexOf(x) !== -1) {
			ruleMatches.push(x);
		}
	});
	return ruleMatches;
}
namespace("Crm.Configurator.Rules.Restriction").getRemoveRuleMatches = function(rule, itemNo, itemNos) {
	return [];
}