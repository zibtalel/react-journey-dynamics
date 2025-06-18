///<reference path="../../../../Content/@types/index.d.ts" />
import { namespace } from "../../../../Content/ts/namespace"
import { HelperProject } from "../../../Crm.Project/Content/ts/helpers/Helper.Project";

export class MarketInsightDetailsProjectsTabViewModel extends window.Crm.Project.ViewModels.ProjectListIndexViewModel {

	lookups: LookupType = {
		projectStatuses: { $tableName: "CrmProject_ProjectStatus" },
		projectCategories: { $tableName: "CrmProject_ProjectCategory" },
		currencies: { $tableName: "Main_Currency" }
	};
	filterStatus: string = "";

	constructor(args) {
		super();
		this.args = args;
		this.getFilter("ParentId").extend({ filterOperator: "===" })(args.marketInsight().ParentId());
		this.getFilter("MasterProductFamilyKey").extend({ filterOperator: "===" })(args.marketInsight().ProductFamilyKey());
	}

	async init(): Promise<void> {
		if (window.location.href.split("&status=").length > 1) {
			this.filterStatus = window.location.href.split("&status=")[1].split("&")[0];
		}
		await super.init();
		await window.Helper.Lookup.getLocalizedArrayMaps(this.lookups);
		this.bulkActions([]);
	}
	getItemGroup(item) {
		const groupedData = this.args.valuePerProjectAndCurrency()[item.StatusKey()]?.Currencies;
		return HelperProject.getItemGroup(groupedData, item, this);
	}
	applyFilters(query) {
		if (this.filterStatus !== "") {
			return super.applyFilters(query).filter("it.StatusKey === this.status", { status: this.filterStatus })
		}
		return super.applyFilters(query);
	}
}
namespace("Crm.MarketInsight.ViewModels").MarketInsightDetailsProjectsTabViewModel = MarketInsightDetailsProjectsTabViewModel;
