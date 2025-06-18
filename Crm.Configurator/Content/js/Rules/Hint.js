namespace("Crm.Configurator.Rules.Hint").getAlertCssClass = function() {
	return "alert alert-info";
}
namespace("Crm.Configurator.Rules.Hint").getAlertMessage = function(ruleMatches) {
	if (!ruleMatches || ruleMatches.length === 0) {
		return window.Helper.String.getTranslatedString("DefaultHintRuleAlertMessage");
	}
	return window.Helper.String.getTranslatedString("HintRuleAlertMessage");
}
namespace("Crm.Configurator.Rules.Hint").applyAdd = function(articleIds, rule) {
	return articleIds;
}
namespace("Crm.Configurator.Rules.Hint").applyRemove = function(articleIds, rule) {
	return articleIds;
}
namespace("Crm.Configurator.Rules.Hint").getAddRuleMatches = function(articleId, ruleArticleIds, currentArticleIds) {
	var ruleMatches = [];
	ruleArticleIds.forEach(function(x) {
		if (x !== articleId && currentArticleIds.indexOf(x) !== -1) {
			ruleMatches.push(x);
		}
	});
	return ruleMatches;
}
namespace("Crm.Configurator.Rules.Hint").getRemoveRuleMatches = function(rule, articleId, articleIds) {
	return [];
}