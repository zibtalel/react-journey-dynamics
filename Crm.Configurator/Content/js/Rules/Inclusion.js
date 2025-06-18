namespace("Crm.Configurator.Rules.Inclusion").getAlertCssClass = function() {
	return "alert alert-success";
}
namespace("Crm.Configurator.Rules.Inclusion").getAlertMessage = function (ruleMatches) {
  if (!ruleMatches || ruleMatches.length === 0) {
    return window.Helper.String.getTranslatedString("DefaultInclusionRuleAlertMessage");
  }
  return window.Helper.String.getTranslatedString("InclusionRuleAlertMessage");
}
namespace("Crm.Configurator.Rules.Inclusion").applyAdd = function(articleIds, rule) {
	rule.AffectedVariableValues.forEach(function(articleId) {
		if (articleIds.indexOf(articleId) === -1) {
			articleIds.push(articleId);
}
	});
	return articleIds;
}
namespace("Crm.Configurator.Rules.Inclusion").applyRemove = function(articleIds, rule) {
	rule.AffectedVariableValues.forEach(function(articleId) {
		if (articleIds.indexOf(articleId) === -1) {
			rule.VariableValues.forEach(function(parentArticleId) {
				var index = articleIds.indexOf(parentArticleId);
				if (index !== -1) {
					articleIds.splice(index, 1);
				}
			});
		}
	});
	return articleIds;
}
namespace("Crm.Configurator.Rules.Inclusion").getAddRuleMatches = function(articleId, ruleArticleIds, currentArticleIds) {
	var ruleMatches = [];
	ruleArticleIds.forEach(function(x) {
		if (x !== articleId && currentArticleIds.indexOf(x) === -1) {
			ruleMatches.push(x);
		}
	});
	return ruleMatches;
}
namespace("Crm.Configurator.Rules.Inclusion").getRemoveRuleMatches = function(rule, articleId, articleIds) {
	var ruleMatches = [];
	rule.VariableValues.forEach(function(ruleVariableValue) {
		if (ruleVariableValue !== articleId && articleIds.indexOf(ruleVariableValue) !== -1) {
			ruleMatches.push(ruleVariableValue);
		}
	});
	return ruleMatches;
}