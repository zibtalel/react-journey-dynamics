namespace("Crm.Configurator.ViewModels").ConfigurationIndexViewModel = function() {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	viewModel.articleGroups = window.ko.observableArray([]);
	viewModel.selectedArticleGroups = window.ko.observableArray([]);
	viewModel.articles = window.ko.observableArray([]);
	viewModel.params = window.ko.observable(null);
	viewModel.paramsString = window.ko.pureComputed(function() {
	  return "&" + Object.getOwnPropertyNames(viewModel.params() || {}).map(function (param) { return param + "=" + viewModel.params()[param]; }).join("&");
	});
	viewModel.user = window.ko.observable(null);
}
namespace("Crm.Configurator.ViewModels").ConfigurationIndexViewModel.prototype.init = function(id, params) {
	var viewModel = this;
	var d = new $.Deferred();
	viewModel.params(params);
	return window.Helper.User.getCurrentUser().pipe(function(user) {
		viewModel.user(user);
		viewModel.select(null);
		viewModel.selectedArticleGroups.subscribe(() => {
			viewModel.loading(true);
			viewModel.loadArticleGroups().then(() => viewModel.loading(false));
		});
		return viewModel.loadArticleGroups();
	});
}
namespace("Crm.Configurator.ViewModels").ConfigurationIndexViewModel.prototype.select = function(articleGroup) {
	var viewModel = this;
	viewModel.selectedArticleGroups.push(articleGroup);
	viewModel.setConfiguratorBreadcrumbs()
}
namespace("Crm.Configurator.ViewModels").ConfigurationIndexViewModel.prototype.getRedirectUrl = function(configurationBase) {
	var viewModel = this;
	return "#/" + (viewModel.params().redirectPlugin || "Crm.Order") + "/" + (viewModel.params().redirectController || "Offer") + "/" + (viewModel.params().redirectAction || "Details") + "?configurationBaseId=" + configurationBase.Id() + viewModel.paramsString();
}
namespace("Crm.Configurator.ViewModels").ConfigurationIndexViewModel.prototype.loadArticleGroups = function() {
	var viewModel = this;
	var articleGroup = viewModel.selectedArticleGroups()[viewModel.selectedArticleGroups().length - 1];
	var articleGroupKey = !!articleGroup ? articleGroup.Key() : null;
	var query = window.database["CrmArticle_ArticleGroup0" + viewModel.selectedArticleGroups().length]
		.filter(function(x) { return x.Language == this.languageKey }, { languageKey: viewModel.user().DefaultLanguageKey });
	if (viewModel.selectedArticleGroups().length > 1) {
		query = query.filter(function(x) {
			return x.ParentArticleGroup == this.articleGroupKey;
		}, { articleGroupKey: articleGroupKey });
	} else {
		query = query.filter(function(x) {
			return x.ExtensionValues.ShowInConfigurator == true;
		});
	}
	return query
		.orderBy((function(x) { return x.Value; }))
		.toArray(viewModel.articleGroups)
		.then(function(results) {
			if (results.length > 0) {
				viewModel.articles([]);
			} else {
				return viewModel.loadArticles();
			}
		});
}
namespace("Crm.Configurator.ViewModels").ConfigurationIndexViewModel.prototype.loadArticles = function() {
	var viewModel = this;
	var query = window.database.CrmConfigurator_ConfigurationBase;
	for (var i = 1; i < viewModel.selectedArticleGroups().length; i++) {
		query = query.filter("it.ArticleGroup0" + i + "Key == this.articleGroupKey", { articleGroupKey: viewModel.selectedArticleGroups()[i].Key() });
	}
	return query
		.orderBy((function(x) { return x.ItemNo; }))
		.orderBy((function(x) { return x.Description; }))
		.toArray(viewModel.articles);
}
namespace("Crm.Configurator.ViewModels").ConfigurationIndexViewModel.prototype.resetToArticleGroup = function(articleGroup) {
	var viewModel = this;
	var index = viewModel.selectedArticleGroups().indexOf(articleGroup);
	if (index !== -1) {
		viewModel.selectedArticleGroups(viewModel.selectedArticleGroups().slice(0, index + 1));
	}
	viewModel.setConfiguratorBreadcrumbs();
}

namespace("Crm.Configurator.ViewModels").ConfigurationIndexViewModel.prototype.setConfiguratorBreadcrumbs = function () {
	var viewModel = this;
	const breads = [];
	for (const item of viewModel.selectedArticleGroups()) {
		const name = item ? item.Value() : null;
		breads.push(new Breadcrumb(name, "#", () => viewModel.resetToArticleGroup(item)));
	}
	window.breadcrumbsViewModel.setCustomBreadcrumbs(breads);
};