///<reference path="../../../../../Content/@types/index.d.ts" />
///<reference path="../../@types/index.d.ts" />

import _ from "lodash";

export class HelperProject {

	static getRatingValues(): number[] {
		return [null, 1, 2, 3, 4, 5];
	}

	static getRatingDisplayName(data: number): string {
		if (data === null) {
			return window.Helper.String.getTranslatedString("Unspecified");
		}
		return data.toString();
	}

	static getItemGroup(groupedData, item, viewModel) {
		let groupedValueString = "";
		const fallbackText = window.Helper.String.getTranslatedString("Unspecified");
		for (let element of groupedData) {
			const currency = viewModel.lookups.currencies[`${element.Currency}`];
			const number = window.Globalize.formatNumber(element.CurrencySum, {
				maximumFractionDigits: 2,
				minimumFractionDigits: 2
			});
			groupedValueString += `(${number} ${currency !== null ? currency.Value : fallbackText})  `;
			groupedValueString += "  ";
		}
		const status = viewModel.lookups.projectStatuses[`${item.StatusKey()}`];
		return {title: status !== null ? status.Value : fallbackText, subtitle: groupedValueString};
	}

	static getCurrencySumByStatus(parentId) {
		return window.database.CrmProject_Project.CurrencySumByStatus(parentId)
			.toArray()
			.then(function (result) {
				let dict = {};
				result.forEach(el => {
					const property = "Currencies"

					if (!dict[el.Status]) {
						dict[el.Status] = {};
						dict[el.Status].Status = el.Status
					}
					if (!dict[el.Status].hasOwnProperty(property)) {
						dict[el.Status].Currencies = new Array()
					}
					dict[el.Status].Currencies.push(el);
				})
				return dict;
			})
	}

	static allProjectsCurrencySum() {
		return window.database.CrmProject_Project.AllProjectsCurrencySum()
			.toArray()
			.then(function (result) {
				let dict = {};
				result.forEach(el => {
					const property = "Currencies"

					if (!dict[el.Status]) {
						dict[el.Status] = {};
						dict[el.Status].Status = el.Status
					}
					if (!dict[el.Status].hasOwnProperty(property)) {
						dict[el.Status].Currencies = new Array()
					}
					dict[el.Status].Currencies.push(el);
				})
				return dict;
			})
	}

	static FilteredProjectsSum(query) {
		return query
			.filter("it.Value !== null")
			.toArray()
			.then(res => {
				let results = res.map(function (it) {
					return {
						Currency: it.CurrencyKey,
						CurrencySum: it.Value,
						Status: it.StatusKey
					}
				});
				results = _.groupBy(results, function (result) {
					return result.Status;
				});
				_.forEach(results, function (value, key) {
					results[key] = _.groupBy(results[key], function (item) {
						return item.Currency;
					});
				});
				Object.keys(results).forEach(statusKey => {
					Object.keys(results[statusKey]).forEach(currencyKey => {
						let temp = {};
						results[statusKey][currencyKey].forEach(item => {
							temp["Currency"] = item["Currency"];
							temp["CurrencySum"] = (temp["CurrencySum"] || 0) + item["CurrencySum"];
							temp["Status"] = item["Status"];
						})
						results[statusKey][currencyKey] = temp;
					})
				});
				let dict = {};
				Object.values(results).forEach(objects => {
					Object.values(objects).forEach(el => {
						const property = "Currencies"
						if (!dict[el.Status]) {
							dict[el.Status] = {};
							dict[el.Status].Status = el.Status
						}
						if (!dict[el.Status].hasOwnProperty(property)) {
							dict[el.Status].Currencies = new Array()
						}
						dict[el.Status].Currencies.push(el);
					})
				})
				return dict;
			});
	}
	static getQueries(productFamilyId, parentId, item, queries) {
		queries.push(
			{
				queryable: window.database.CrmProject_Project.CountOfProjectsByStatus(productFamilyId, parentId),
				method: "toArray",
				handler: function (result) {
					let dict = {}
					result.forEach(el => {
						delete el.$type;
						dict[el.Status] = el
					})
					item.valuePerProjectAndCurrency(dict);
				}
			},
			{
				queryable: window.database.CrmProject_Project.CurrencySumByStatusAndCurrencyKey(productFamilyId, parentId),
				method: "toArray",
				handler: function (result) {
					const property = "Currencies"
					const valuePerProjectAndCurrency = ko.unwrap(item.valuePerProjectAndCurrency)
					result.forEach(el => {
						delete el.$type;
						if (valuePerProjectAndCurrency[el.Status] && valuePerProjectAndCurrency[el.Status].Status === el.Status) {
							if (!valuePerProjectAndCurrency[el.Status].hasOwnProperty(property)) {
								valuePerProjectAndCurrency[el.Status].Currencies = new Array()
							}
							valuePerProjectAndCurrency[el.Status].Currencies.push(el)
						}
					})
					item.valuePerProjectAndCurrency(valuePerProjectAndCurrency);
				}
			},
		)
		return queries;
	}

	static getOpenProjectsCount(): Promise<number> {
		const currentUser = window.Helper.User.getCurrentUserName();
		return window.database.CrmProject_Project
			.filter(function (it) {
				return it.StatusKey === "100" && it.ResponsibleUser === this.currentUser;
			}, { currentUser: currentUser })
			.count();
	}

	static getOverdueProjectsCount(): Promise<number> {
		const currentUser = window.Helper.User.getCurrentUserName();
		return window.database.CrmProject_Project
			.filter(function (it) {
				return it.StatusKey === "100" && it.DueDate < this.now && it.ResponsibleUser === this.currentUser;
			}, { now: new Date() , currentUser: currentUser })
			.count();
	}
	static getName(project): string {
		project = window.ko.unwrap(project || {});
		const projectNo = window.ko.unwrap(project.ProjectNo);
		const name = window.ko.unwrap(project.Name);
		return [projectNo, name].filter(function (x) {
			return x;
		}).join(" - ");
	}

	/**@return {{item, id: string, text: string}}*/
	static mapDisplayNameForSelect2(project) {
		return {
			id: project.Id,
			text: HelperProject.getName(project),
			item: project
		};
	}
}
export function InitializeOfflineQueriesProject() {
	document.addEventListener("DatabaseInitialized", function () {
		if (window.database.storageProvider.name === "webSql") {
			if (!window.database.CrmProject_Project) {
				return;
			}
			if (window.database.CrmProject_Project.ValuePerCategoryAndYear) {
				throw "CrmProject_Project.ValuePerCategoryAndYear must be undefined at this point";
			}
			window.database.CrmProject_Project.ValuePerCategoryAndYear = function (ids) {
				return window.database.CrmProject_Project
					.filter(function (it) {
						return it.Id in this.ids;
					}, { ids: ids })
					.map(function (it) {
						return {
							d: it.CategoryKey,
							//@ts-ignore
							x: (it.DueDate / 1000).strftime("%Y", "unixepoch"),
							y: (it.Value as unknown as $data.Queryable<number>).sum()
						};
					})
					.groupBy("it.CategoryKey")
					.groupBy(function (it) {
						//@ts-ignore
						return (it.DueDate / 1000).strftime("%Y", "unixepoch");
					});
			};
			window.database.CrmProject_Project.CountOfProjectsByStatus = function (productFamilyKey, parentId) {
				return window.database.CrmProject_Project
					.filter("it.MasterProductFamilyKey === this.productFamilyKey && it.ParentId === this.parentId",
						{ productFamilyKey: ko.unwrap(productFamilyKey), parentId: ko.unwrap(parentId) })
					.map(function (it) {
						return {
							TotalCount: (it.Id as unknown as $data.Queryable<string>).count(),
							Status: it.StatusKey,
						}
					})
					.groupBy("it.StatusKey")
			}
			if (window.database.CrmProject_Project.CurrencySumByStatusAndCurrencyKey) {
				throw "CrmProject_Project.CurrencySumByStatusAndCurrencyKey must be undefined at this point";
			}
			window.database.CrmProject_Project.CurrencySumByStatusAndCurrencyKey = function (productFamilyKey, parentId) {
				return window.database.CrmProject_Project
					.filter("it.MasterProductFamilyKey === this.productFamilyKey && it.ParentId === this.parentId",
						{ productFamilyKey: ko.unwrap(productFamilyKey), parentId: ko.unwrap(parentId) })
					.filter("it.Value !== null")
					.map(function (it) {
						return {
							Currency: it.CurrencyKey,
							CurrencySum: (it.Value as unknown as $data.Queryable<number>).sum(),
							Status: it.StatusKey
						}
					})
					.groupBy("it.StatusKey")
					.groupBy("it.CurrencyKey")
			}

			if (window.database.CrmProject_Project.CurrencySumByStatus) {
				throw "CrmProject_Project.CurrencySumByStatus must be undefined at this point";
			}
			window.database.CrmProject_Project.CurrencySumByStatus = function (parentId) {
				return window.database.CrmProject_Project
					.filter("it.ParentId === this.parentId",
						{ parentId: ko.unwrap(parentId) })
					.filter("it.Value !== null")
					.map(function (it) {
						return {
							Currency: it.CurrencyKey,
							CurrencySum: (it.Value as unknown as $data.Queryable<number>).sum(),
							Status: it.StatusKey
						}
					})
					.groupBy("it.StatusKey")
					.groupBy("it.CurrencyKey")
			}

			if (window.database.CrmProject_Project.AllProjectsCurrencySum) {
				throw "CrmProject_Project.AllProjectsCurrencySum must be undefined at this point";
			}
			window.database.CrmProject_Project.AllProjectsCurrencySum = function () {
				return window.database.CrmProject_Project
					.filter("it.Value !== null")
					.map(function (it) {
						return {
							Currency: it.CurrencyKey,
							CurrencySum: (it.Value as unknown as $data.Queryable<number>).sum(),
							Status: it.StatusKey
						}
					})
					.groupBy("it.StatusKey")
					.groupBy("it.CurrencyKey")
			}
		}
	});
}




