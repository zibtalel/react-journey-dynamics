///<reference path="../../../../Content/@types/index.d.ts" />
import { namespace } from "../../../../Content/ts/namespace";
import { HelperProject } from "./helpers/Helper.Project";

export class CompanyDetailsProjectsTabViewModel extends window.Crm.Project.ViewModels.ProjectListIndexViewModel {
	currenciesValuesProjects: KnockoutObservable<any> = ko.observableArray([]);

	constructor(args) {
		super();
		this.args = args;
		this.getFilter("ParentId").extend({ filterOperator: "===" })(args.company().Id());
	}

	async init(): Promise<void> {
		this.currenciesValuesProjects(await window.Helper.Project.getCurrencySumByStatus(ko.unwrap(this.args.company().Id())));
		this.isTabViewModel(true);
		await super.init();
		this.bulkActions([]);
	}
	getItemGroup(item) {
		const groupedData = this.currenciesValuesProjects()[item.StatusKey()]?.Currencies;
		return HelperProject.getItemGroup(groupedData, item, this);
	}
}
namespace("Main.ViewModels").CompanyDetailsProjectsTabViewModel = CompanyDetailsProjectsTabViewModel
