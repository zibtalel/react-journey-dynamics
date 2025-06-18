///<reference path="../../../../Content/@types/index.d.ts" />
///<reference path="../@types/index.d.ts" />
import { namespace } from "../../../../Content/ts/namespace";

export class CompanyDetailsPotentialsTabViewModel extends window.Crm.Project.ViewModels.PotentialListIndexViewModel {

	companyId: KnockoutObservable<string>;
	lookups: LookupType = {
		potentialStatuses: { $tableName: "CrmProject_PotentialStatus" },
		potentialPriorities: { $tableName: "CrmProject_PotentialPriority" },
		currencies: { $tableName: "Main_Currency" },
		sourceType: { $tableName: "Main_SourceType" },
	};

	constructor(parentViewModel) {
		super();
		this.companyId = window.ko.observable(parentViewModel.company().Id());
		this.getFilter("ParentId").extend({ filterOperator: "===" })(this.companyId());
	}

	async init(): Promise<void> {
		await super.init();
		await window.Helper.Lookup.getLocalizedArrayMaps(this.lookups);
		this.bulkActions([]);
	}

}
namespace("Main.ViewModels").CompanyDetailsPotentialsTabViewModel = CompanyDetailsPotentialsTabViewModel;
