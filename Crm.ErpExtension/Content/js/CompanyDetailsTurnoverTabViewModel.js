namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel = function (parentViewModel) {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	var companyId = parentViewModel.company().Id();
	viewModel.lookups = {
		articleGroups01: { $tableName: "CrmArticle_ArticleGroup01" },
		articleGroups02: { $tableName: "CrmArticle_ArticleGroup02" },
		articleGroups03: { $tableName: "CrmArticle_ArticleGroup03" },
		articleGroups04: { $tableName: "CrmArticle_ArticleGroup04" },
		articleGroups05: { $tableName: "CrmArticle_ArticleGroup05" },
		currencies: { $tableName: "Main_Currency" },
		quantityUnits: { $tableName: "CrmArticle_QuantityUnit" }
	};
	viewModel.company = parentViewModel.company;
	viewModel.companyId = window.ko.observable(companyId);
	viewModel.currency = window.ko.observable(null);
	viewModel.firstOrder = window.ko.observable(null);
	viewModel.latestOrder = window.ko.observable(null);
	viewModel.quantityUnit = window.ko.observable(null);
	viewModel.showVolume = window.ko.observable(false);
	viewModel.totalOrders = window.ko.observable(null);
	viewModel.daysSinceLastOrder = window.ko.pureComputed(function () {
		if (!viewModel.latestOrder()) {
			return null;
		}
		return window.moment().diff(viewModel.latestOrder(), "days");
	});
	viewModel.orderEveryXDays = window.ko.pureComputed(function () {
		if (!viewModel.firstOrder() || !viewModel.latestOrder() || !viewModel.totalOrders() || viewModel.totalOrders() <= 1) {
			return null;
		}
		var days = window.moment(viewModel.latestOrder()).diff(viewModel.firstOrder(), "days") - 1;
		var daysBetweenOrders = Math.round(days / viewModel.totalOrders());
		return daysBetweenOrders + 1;
	});
	var currentMonth = new Date().getMonth() + 1;
	var currentYear = new Date();
	var previousYear = window.moment().add(-1, "year").toDate();
	viewModel.groupedTurnoverItems = window.ko.pureComputed(function () {
		var id = 0;

		function getGroupedTurnoverItems(articleGroupLevel, key, items, parent) {
			items = window.ko.isObservable(items) ? items : window.ko.observableArray(items);
			var articleGroup = viewModel.lookups["articleGroups" + ("00" + articleGroupLevel).slice(-2)];
			var articleGroupKeyProperty = "ArticleGroup" + ("00" + articleGroupLevel).slice(-2) + "Key";
			var articleGroupKeys = items.distinct(articleGroupKeyProperty).indexKeys[articleGroupKeyProperty]()
				.sort(function (a, b) {
					if (!a) {
						return 1;
					}
					if (!b) {
						return -1;
					}
					return articleGroup.$array.indexOf(articleGroup[a]) - articleGroup.$array.indexOf(articleGroup[b]);
				});
			var parentArticleGroup = articleGroupLevel > 1
				? viewModel.lookups["articleGroups" + ("00" + (articleGroupLevel - 1)).slice(-2)][key]
				: null;
			var groupedTurnoverItems = {};
			groupedTurnoverItems.Id = id++;
			groupedTurnoverItems.Level = articleGroupLevel;
			groupedTurnoverItems.Key = parentArticleGroup ? parentArticleGroup.Value : key;
			groupedTurnoverItems.Items = window.ko.pureComputed(function () {
				var itemsWithoutArticleGroup = window.ko.observableArray(items.index[articleGroupKeyProperty]()[""] || [])
					.distinct("ItemNo");
				var itemNos = itemsWithoutArticleGroup.indexKeys.ItemNo().sort();
				return window.ko.utils.arrayMap(itemNos,
					function (itemNo) {
						var item = window.ko.observableArray(itemsWithoutArticleGroup.index.ItemNo()[itemNo]).distinct("Year");

						function getTotal(year) {
							year = year ? year.toString() : year;
							if (item.index.Year()[year]) {
								return item.index.Year()[year][0].Total();
							}
							return 0;
						};

						var previousYearCurrentMonth = 0;
						var previousYearItem = item.index.Year()[previousYear.getFullYear().toString()];
						if (previousYearItem) {
							for (var i = 1; i < currentMonth; i++) {
								previousYearCurrentMonth += previousYearItem[0]["Month" + i.toString()]() || 0;
							}
						}
						var currentYearTotal = getTotal(currentYear.getFullYear());
						var trend = "flat";
						if (currentYearTotal * 1.05 > previousYearCurrentMonth) {
							trend = "up";
						} else if (currentYearTotal * 0.95 < previousYearCurrentMonth) {
							trend = "down";
						}
						var unit = viewModel.showVolume() ? viewModel.quantityUnit() : viewModel.currency();
						return {
							Item: item()[0],
							Trend: trend,
							CurrentYear: currentYear,
							CurrentYearValue: currentYearTotal,
							PreviousYearCurrentMonth: previousYearCurrentMonth,
							PreviousYear: previousYear,
							PreviousYearValue: getTotal(previousYear.getFullYear()),
							AdditionalYears: viewModel.turnoverAdditionalYears().map(function (year) {
								return {
									Year: year,
									Value: getTotal(year)
								};
							}),
							Unit: unit ? unit.Value : null
						};
					});
			});
			groupedTurnoverItems.HasArticleGroups = window._.without(articleGroupKeys, "").length > 0;
			groupedTurnoverItems.Visible = window.ko.observable(parent && parent.Id === 0 && !parent.HasArticleGroups);
			groupedTurnoverItems.ArticleGroups = window.ko.pureComputed(function () {
				return window.ko.utils.arrayMap(articleGroupKeys,
					function (articleGroupKey) {
						return getGroupedTurnoverItems(articleGroupLevel + 1, articleGroupKey, items.index[articleGroupKeyProperty]()[articleGroupKey], groupedTurnoverItems);
					});
			});
			return groupedTurnoverItems;
		};

		return getGroupedTurnoverItems(1, null, viewModel.turnoverItems, null);
	});
	viewModel.hasTurnoverStatistics = window.ko.observable(false);
	viewModel.turnoverItems = window.ko.observableArray([]).distinct("Year");
	viewModel.turnoverItemsQuery = window.ko.observable(null);
	viewModel.turnoverAdditionalYears = window.ko.pureComputed(function () {
		var turnoverYears = viewModel.turnoverItems.indexKeys.Year().map(function (x) { return parseInt(x); });
		return window._.without(turnoverYears, currentYear.getFullYear(), previousYear.getFullYear()).sort().reverse();
	});
	viewModel.showAllTurnoverItems = window.ko.observable(false);
	viewModel.showAllTurnoverItems.subscribe(function () {
		viewModel.loadTurnoverItems();
	});
	viewModel.chartUnit = window.ko.pureComputed(function () {
		if (viewModel.showVolume() && viewModel.quantityUnit()) {
			return viewModel.quantityUnit().Value;
		}
		if (!viewModel.showVolume() && viewModel.currency()) {
			return viewModel.currency().Value;
		}
		return null;
	});
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype = Object
	.create(window.Main.ViewModels.ViewModelBase.prototype);
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.init = function () {
	var viewModel = this;
	return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups)
		.then(function () {
		return viewModel.getDistinctProperty("CurrencyKey");
	}).then(function (currencyKeys) {
			viewModel.lookups.currencies.$array = viewModel.lookups.currencies.$array.filter(function(x) {
				return !(x.Key === null || currencyKeys[0].indexOf(x.Key) === -1);
			});
			if (viewModel.lookups.currencies.$array.length > 0) {
				viewModel.currency(viewModel.lookups.currencies.$array[0]);
			} else {
				viewModel.showVolume(true);
			}
		return viewModel.getDistinctProperty("QuantityUnitKey");
	}).then(function (quantityUnitKeys) {
			viewModel.lookups.quantityUnits.$array = viewModel.lookups.quantityUnits.$array.filter(function (x) {
				return !(x.Key === null || quantityUnitKeys[0].indexOf(x.Key) === -1);
			});
		if (viewModel.showVolume() & viewModel.lookups.quantityUnits.$array.length > 0) {
			viewModel.quantityUnit(viewModel.lookups.quantityUnits.$array[0]);
		}
		return window.database.CrmErpExtension_ErpTurnover
			.filter(function (x) { return x.ContactKey === this.contactKey; }, { contactKey: viewModel.companyId() })
			.count()
			.then(function (count) {
				viewModel.hasTurnoverStatistics(count > 0);
			});
	}).then(function () {
		return viewModel.getSalesOrderInformation();
	}).then(function (result) {
		if (result) {
			viewModel.firstOrder(result.FirstOrder);
			viewModel.latestOrder(result.LastOrder);
			viewModel.totalOrders(result.TotalOrders);
		}
	}).then(viewModel.loadTurnoverItems.bind(viewModel));
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.getSalesOrderInformation = function() {
	return window.database.CrmErpExtension_SalesOrder.GetInformation(this.companyId());
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.getDistinctProperty = function(property) {
	return window.database.CrmErpExtension_ErpTurnover.GetDistinctProperty(this.companyId(), property).toArray();
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.getChartColor = function(articleGroupKey) {
	var viewModel = this;
	var articleGroup = viewModel.lookups.articleGroups01[articleGroupKey];
	return articleGroup != null ? articleGroup.Color : null;
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.getChartLabel = function(articleGroupKey) {
	var viewModel = this;
	var articleGroup = viewModel.lookups.articleGroups01[articleGroupKey];
	return articleGroup != null && articleGroup.Key != null
		? articleGroup.Value
		: window.Helper.String.getTranslatedString('Miscellaneous');
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.getTurnoverItemsQuery = function(queryForChart) {
	var viewModel = this;
	var currencyKey = viewModel.showVolume() || !viewModel.currency() ? null : viewModel.currency().Key;
	var quantityUnitKey = viewModel.showVolume() && viewModel.quantityUnit() ? viewModel.quantityUnit().Key : null;
	var query = window.database.CrmErpExtension_ErpTurnover
		.filter(function(x) {
				return x.ContactKey === this.contactKey &&
					x.Total > 0 &&
					x.IsVolume === this.isVolume &&
					x.CurrencyKey === this.currencyKey &&
					x.QuantityUnitKey === this.quantityUnitKey;
			},
			{
				contactKey: viewModel.companyId(),
				isVolume: viewModel.showVolume(),
				currencyKey: currencyKey,
				quantityUnitKey: quantityUnitKey
			});
	if (queryForChart) {
		return window.database.CrmErpExtension_ErpTurnover.TurnoverPerArticleGroup01AndYear(viewModel.companyId(), viewModel.showVolume(), currencyKey, quantityUnitKey, query);
	}
	return query;
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.loadTurnoverItems = function () {
	var viewModel = this;
	viewModel.loading(true);
	viewModel.turnoverItemsQuery(viewModel.getTurnoverItemsQuery(true));
	var query = viewModel.getTurnoverItemsQuery();
	if (!viewModel.showAllTurnoverItems()) {
		query = query.filter(function (x) {
			return x.Year === this.year || x.Year === this.previousYear;
		},
			{
				year: new Date().getFullYear(),
				previousYear: new Date().getFullYear() - 1
			});
	}
	return query.orderByDescending(function (x) {
		return x.Year;
	}).toArray(viewModel.turnoverItems)
		.then(function () {
			viewModel.loading(false);
		});
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.setCurrency = function (currency) {
	var viewModel = this;
	if (viewModel.currency() === currency) {
		return;
	}
	viewModel.turnoverItems([]);
	viewModel.currency(currency);
	viewModel.quantityUnit(null);
	viewModel.showVolume(false);
	viewModel.loadTurnoverItems();
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.setQuantityUnit = function (quantityUnit) {
	var viewModel = this;
	if (viewModel.quantityUnit() === quantityUnit) {
		return;
	}
	viewModel.turnoverItems([]);
	viewModel.quantityUnit(quantityUnit);
	viewModel.currency(null);
	viewModel.showVolume(true);
	viewModel.loadTurnoverItems();
};
namespace("Main.ViewModels").CompanyDetailsTurnoverTabViewModel.prototype.toggleSelectedArticleGroup = function (toggledArticleGroupViewModel) {
	if (!toggledArticleGroupViewModel.Visible()) {
		toggledArticleGroupViewModel.Visible(true);
	}
};