///<reference path="../../../../Content/@types/index.d.ts" />
import { Breadcrumb } from "../../../../Content/ts/breadcrumbs";
import { namespace } from "../../../../Content/ts/namespace";
import { HelperBatch } from '../../../../Content/ts/helper/Helper.Batch'
import { HelperProject } from '../../../../Plugins/Crm.Project/Content/ts/helpers/Helper.Project'
import { HelperPotential } from '../../../../Plugins/Crm.Project/Content/ts/helpers/Helper.Potential'

export class MarketInsightDetailsViewModel extends window.Main.ViewModels.ContactDetailsViewModel {

	marketInsight = ko.observable<Crm.MarketInsight.Rest.Model.ObservableCrmMarketInsight_MarketInsight>(null);
	valuePerProjectAndCurrency = ko.observableArray<any>([]);
	countOfPotentialsByStatus = ko.observableArray<any>([]);
	projectStatusesKeys = ko.observableArray<string>([]);
	potentialStatusesKeys = ko.observableArray<string>([]);

	constructor() {
		super();
		this.contactType("MarketInsight");
	}

	lookups: LookupType = {
		potentialPriorities: { $tableName: "CrmProject_PotentialPriority" },
		currencies: { $tableName: "Main_Currency" },
		projectStatuses: { $tableName: "CrmProject_ProjectStatus" },
		potentialStatuses: { $tableName: "CrmProject_PotentialStatus" }
	}

	async init(id: string): Promise<void> {
		const self = this;
		let queries = [];
		const marketInsightData = await window.database.CrmMarketInsight_MarketInsight
			.include("ProductFamily")
			.include2("Company")
			.find(id);
		this.marketInsight(marketInsightData.asKoObservable());

		this.companyId = ko.unwrap(this.marketInsight().ParentId);
		const company = await window.database.Main_Company.find(this.companyId);
		this.marketInsight().Company(company.asKoObservable());

		this.contactId(id);
		this.contact(this.marketInsight());
		this.contactName(this.marketInsight().Name());

		await this.setVisibilityAlertText();
		this.setBreadcrumbs(id);

		HelperProject.getQueries(this.marketInsight().ProductFamilyKey(), this.companyId, this, queries);
		HelperPotential.getQueries(this.marketInsight().ProductFamilyKey(), this.companyId, this, queries);
		await HelperBatch.Execute(queries);

		await window.Helper.Lookup.getLocalizedArrayMaps(this.lookups);
		await window.database.CrmProject_ProjectStatus
			.select("it.Key")
			.toArray(function (keys) {
				const uniqueKeys = [...new Set(keys)]
				self.projectStatusesKeys(uniqueKeys)
			});
		await window.database.CrmProject_PotentialStatus
			.select("it.Key")
			.toArray(function (keys: string[]) {
				const uniqueKeys = [...new Set(keys)]
				self.potentialStatusesKeys(uniqueKeys)
			});
	}
	setBreadcrumbs(id): void {
		window.breadcrumbsViewModel.setBreadcrumbs([
			new Breadcrumb(window.Helper.String.getTranslatedString("Company"), "#/Main/CompanyList/IndexTemplate"),
			new Breadcrumb(window.Helper.Company.getDisplayName(this.marketInsight().Company), `#/Main/Company/DetailsTemplate/${this.companyId}`),
			new Breadcrumb(window.Helper.String.getTranslatedString("MarketInsight"), `#/Main/Company/DetailsTemplate/${this.companyId}?tab=tab-marketinsights`),
			new Breadcrumb(this.marketInsight().ProductFamily().Name(), window.location.hash, null, id)
		]);
	}
}
namespace("Crm.MarketInsight.ViewModels").MarketInsightDetailsViewModel = MarketInsightDetailsViewModel;
