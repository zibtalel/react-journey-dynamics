///<reference path="../../../../Content/@types/index.d.ts" />
import { namespace } from "../../../../Content/ts/namespace";

export class MarketInsightDetailsPotentialsTabViewModel extends window.Crm.Project.ViewModels.PotentialListIndexViewModel {

	lookups: LookupType = {
		potentialStatuses: { $tableName: "CrmProject_PotentialStatus" },
		potentialPriorities: { $tableName: "CrmProject_PotentialPriority" },
		currencies: { $tableName: "Main_Currency" },
		sourceType: { $tableName: "Main_SourceType" },
	};
	filterStatus: string = "";
	
	constructor(args) {
		super();
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
	applyFilters(query) {
		if (this.filterStatus !== "") {
			return super.applyFilters(query).filter("it.StatusKey === this.status", { status: this.filterStatus })
		}
		return super.applyFilters(query);
		
	}

}
namespace("Crm.MarketInsight.ViewModels").MarketInsightDetailsPotentialsTabViewModel = MarketInsightDetailsPotentialsTabViewModel;
