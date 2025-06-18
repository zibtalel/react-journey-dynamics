///<reference path="../../../../Content/@types/index.d.ts" />
import { HelperBatch } from "../../../../Content/ts/helper/Helper.Batch";
import { HelperLookup } from "../../../../Content/ts/helper/Helper.Lookup";
import { namespace } from "../../../../Content/ts/namespace";

export class CompanyDetailsMarketInsightsTabViewModel extends window.Crm.Article.ViewModels.ProductFamilyListIndexViewModel {
	projectStatusesKeys = ko.observableArray<string>([]);
	potentialStatusesKeys = ko.observableArray<string>([]);
	marketInsightStatusFilter = ko.observable<string>(null);
	marketInsightPotentialStatusFilter = ko.observable<string>(null);
	marketInsightProjectStatusFilter = ko.observable<string>(null);
	productFamilyKeysFilter = ko.observableArray<string>([]);
	allowApplyFilter = ko.observable<boolean>(false);
	lookups: LookupType = {
		marketInsightStatuses: { $tableName: "CrmMarketInsight_MarketInsightStatus" },
		projectStatuses: { $tableName: "CrmProject_ProjectStatus" },
		potentialStatuses: { $tableName: "CrmProject_PotentialStatus" },
		potentialPriorities: { $tableName: "CrmProject_PotentialPriority" },
		currencies: { $tableName: "Main_Currency" },
	}
	filterParameter: string;

	constructor(args) {
		super();
		this.ParentId = ko.observable(args.company().Id());
	}

	async init(): Promise<void> {
		const self = this;
		await super.init();
		await HelperLookup.getLocalizedArrayMaps(this.lookups);
		await window.database.CrmProject_ProjectStatus
			.select("it.Key")
			.toArray(function (keys) {
				const uniqueKeys = [...Array.from(new Set(keys))];
				self.projectStatusesKeys(uniqueKeys);
			});
		await window.database.CrmProject_PotentialStatus
			.select("it.Key")
			.toArray(function (keys) {
				const uniqueKeys = [...Array.from(new Set(keys))];
				self.potentialStatusesKeys(uniqueKeys);
			});
	}

	applyFilters(query) {
		if (this.allowApplyFilter()) {
			return window.Crm.Article.ViewModels.ProductFamilyListIndexViewModel.prototype.applyFilters.apply(this, arguments)
				.filter("it.Id in this.productFamilies", { productFamilies: this.productFamilyKeysFilter() });
		}
		return query;
	}

	async applyFiltersMarketInsight(): Promise<void> {
		this.loading(true);
		await this.createFilter();
		super.init();
		this.loading(false)
	}

	async resetFilter(): Promise<void> {
		this.loading(true);
		this.allowApplyFilter(false);
		this.marketInsightStatusFilter(null);
		this.marketInsightPotentialStatusFilter(null);
		this.marketInsightProjectStatusFilter(null);
		await super.init()
		this.loading(false)
	}

	async createFilter(): Promise<void> {
		let marketInsights: any = [];
		let filter: string = "";
		let expression: any = {};
		let keys: string[] = [];

		if (this.marketInsightStatusFilter() && this.marketInsightStatusFilter() !== "unqualified") {
			filter += "it.StatusKey === this.status";
			expression.status = this.marketInsightStatusFilter()
		}

		if (this.marketInsightPotentialStatusFilter()) {
			keys = await window.database.CrmProject_Potential
				.filter("it.StatusKey === this.status && it.ParentId == companyId",
					{ status: this.marketInsightPotentialStatusFilter(), companyId: this.ParentId() })
				.select("it.MasterProductFamilyKey")
				.toArray();

			if (filter !== "") {
				filter += " && "
			}
			filter += "it.ProductFamilyKey in this.keys";
			expression.keys = keys;
		}

		if (this.marketInsightProjectStatusFilter()) {
			const projectKeys = await window.database.CrmProject_Project
				.filter("it.StatusKey === this.status && it.ParentId == companyId",
					{ status: this.marketInsightProjectStatusFilter(), companyId: this.ParentId() })
				.select("it.MasterProductFamilyKey")
				.toArray();

			if (filter !== "" && this.marketInsightPotentialStatusFilter()) {
				const intersectionKeys = keys.filter(element => projectKeys.includes(element));
				expression.keys = intersectionKeys;
			} else {
				if (filter !== "") {
					filter += " && "
				}
				filter += "it.ProductFamilyKey in this.keys";
				expression.keys = projectKeys;
			}
		}

		if (this.marketInsightStatusFilter() === "unqualified") {
			if (this.marketInsightProjectStatusFilter() || this.marketInsightPotentialStatusFilter()) {
				this.productFamilyKeysFilter([]);
			} else {
				marketInsights = await this.findUnqualifiedMarketInsight(this.ParentId());
				const existingUnqualifiedMarketInsight = await window.database.CrmMarketInsight_MarketInsight
					.filter("it.StatusKey === 'unqualified' && it.ParentId === this.parentId", { parentId: this.ParentId() })
					.select("it.ProductFamilyKey")
					.toArray();
				marketInsights = [...Array.from(new Set([...marketInsights, ...existingUnqualifiedMarketInsight]))];
				this.productFamilyKeysFilter(marketInsights);
			}
		}
		else if (filter !== "" && Object.keys(expression).length > 0) {

			if (expression.hasOwnProperty("keys")) {
				expression.keys = [...Array.from(new Set(expression.keys))].filter(x => x !== null);
			}

			filter += " && it.ParentId === this.companyId";
			expression.companyId = this.ParentId();

			marketInsights = await window.database.CrmMarketInsight_MarketInsight
				.filter(filter, expression)
				.select("it.ProductFamilyKey")
				.toArray();

			this.productFamilyKeysFilter(marketInsights);
		} else {
			this.resetFilter();
			return;
		}
		this.allowApplyFilter(true);
	}

	initItems(items) {
		const viewModel = this;
		const queries = [];

		items.map(item => {
			const productFamilyId = ko.unwrap(item.Id);
			const parentId = ko.unwrap(viewModel.ParentId);
			item.marketInsight = ko.observable("");
			item.valuePerProjectAndCurrency = ko.observableArray([]);
			item.countOfPotentialsByStatus = ko.observableArray([]);

			queries.push(
				{
					queryable: window.database.CrmMarketInsight_MarketInsight.filter("it.ProductFamilyKey == productFamilyId && it.ParentId == companyId",
						{ productFamilyId: productFamilyId, companyId: parentId }),
					method: "toArray",
					handler: function (marketInsight) {
						if (marketInsight.length > 0) {
							item.marketInsight(marketInsight[0])
						} else {
							viewModel.createMarketInsight(item);
						}
					}
				});

			window.Helper.Project.getQueries(productFamilyId, parentId, item, queries);
			window.Helper.Potential.getQueries(productFamilyId, parentId, item, queries);
		})

		return HelperBatch.Execute(queries).then(function () {
			return items;
		});
	}

	async findUnqualifiedMarketInsight(companyId): Promise<string[]> {
		const allProductFamilies = await window.database.CrmArticle_ProductFamily
			.filter("it.ParentId === null")
			.select("it.Id")
			.toArray();

		const allMarketInsight = await window.database.CrmMarketInsight_MarketInsight
			.filter("it.ParentId == this.companyId", { companyId: companyId })
			.select("it.ProductFamilyKey")
			.toArray();

		return allProductFamilies.filter(x => !allMarketInsight.includes(x));
	}

	createMarketInsight(item) {
		const marketInsight = window.database.CrmMarketInsight_MarketInsight.defaultType.create();
		marketInsight.Id = window.$data.createGuid().toString().toLowerCase()
		marketInsight.ParentId = this.ParentId();
		marketInsight.Name = item.Name;
		marketInsight.StatusKey = "unqualified";
		marketInsight.ProductFamilyKey = item.Id;

		item.marketInsight(marketInsight);

		return item;
	}

	setFilterParameter(viewModel, status, id) {
		viewModel.filterParameters = status;
		let url = `#/Crm.MarketInsight/MarketInsight/DetailsTemplate/${id}?tab=tab-potentials`
		window.location.hash = url;
	}
}
namespace("Main.ViewModels").CompanyDetailsMarketinsightsTabViewModel = CompanyDetailsMarketInsightsTabViewModel;


