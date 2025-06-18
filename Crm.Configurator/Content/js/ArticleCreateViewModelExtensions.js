(function() {
	window.Crm.Article.ViewModels.ArticleCreateViewModel.prototype.articleFactories["Variable"] = x => {
		x.ExtensionValues = null;
		const article = window.database.CrmConfigurator_Variable.defaultType.create(x);
		article.QuantityUnitKey = "Stk";
		return article;
	};
	window.Crm.Article.ViewModels.ArticleCreateViewModel.prototype.articleFactories["ConfigurationBase"] = x => {
		x.ExtensionValues = null;
		const article = window.database.CrmConfigurator_ConfigurationBase.defaultType.create(x);
		article.QuantityUnitKey = "Stk";
		return article;
	};
})();