document.addEventListener("DatabaseInitialized", function () {
	function configureWebSqlGetInformation() {
		if (!window.database.CrmErpExtension_SalesOrder) {
			return;
		}
		if (window.database.CrmErpExtension_SalesOrder.GetInformation) {
			throw "CrmErpExtension_SalesOrder.GetInformation must be undefined at this point";
		}
		window.database.CrmErpExtension_SalesOrder.GetInformation = function (companyId) {
			return window.database.CrmErpExtension_SalesOrder
				.filter(function (x) {
					return x.ContactKey === this.contactKey;
				}, {contactKey: companyId})
				.map(function (x) {
					return {
						FirstOrder: x.OrderConfirmationDate.min(),
						LastOrder: x.OrderConfirmationDate.max(),
						TotalOrders: x.Id.count()
					};
				})
				.toArray();
		};
	}

	function configureWebSqlGetDistinctProperty() {
		if (!window.database.CrmErpExtension_ErpTurnover) {
			return;
		}
		if (window.database.CrmErpExtension_ErpTurnover.GetDistinctProperty) {
			throw "CrmErpExtension_ErpTurnover.GetDistinctProperty must be undefined at this point";
		}
		window.database.CrmErpExtension_ErpTurnover.GetDistinctProperty = function (companyId, property) {
			return window.database.CrmErpExtension_ErpTurnover
				.filter(function (x) {
					return x.ContactKey === this.contactKey;
				}, {contactKey: companyId})
				.map("it." + property)
				.distinct();
		};
	}

	function configureWebSqlTurnoverPerArticleGroup01AndYear() {
		if (!window.database.CrmErpExtension_ErpTurnover) {
			return;
		}
		if (window.database.CrmErpExtension_ErpTurnover.TurnoverPerArticleGroup01AndYear) {
			throw "CrmErpExtension_ErpTurnover.TurnoverPerArticleGroup01AndYear must be undefined at this point";
		}
		//online version needs all the parameters to filter, offline we can use the predefined query
		window.database.CrmErpExtension_ErpTurnover.TurnoverPerArticleGroup01AndYear =
			function (companyId, isVolume, currencyKey, quantityUnitKey, query) {
				return query
					.map(function (it) {
						return {
							d: it.ArticleGroup01Key,
							x: it.Year,
							y: it.Total.sum()
						};
					})
					.groupBy("it.ArticleGroup01Key")
					.groupBy("it.Year");
			};
	}

	if (window.database.storageProvider.name === "webSql") {
		configureWebSqlGetInformation();
		configureWebSqlGetDistinctProperty();
		configureWebSqlTurnoverPerArticleGroup01AndYear();
	}
});