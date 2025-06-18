///<reference path="../../../../Content/@types/index.d.ts" />
import { namespace } from "../../../../Content/ts/namespace";

export class MarketInsightEditModalViewModel extends window.Main.ViewModels.ViewModelBase {

	args: any;
	marketInsight = ko.observable<Crm.MarketInsight.Rest.Model.ObservableCrmMarketInsight_MarketInsight>(null);
	lookups: LookupType = {
		marketInsightStatus: { $tableName: "CrmMarketInsight_MarketInsightStatus" },
		marketInsightReference: { $tableName: "CrmMarketInsight_MarketInsightContactRelationshipType" },
		marketInsightRelationshipTypes: { $tableName: "CrmMarketInsight_MarketInsightReference" },
	};

	constructor(args) {
		super();
		this.args = args;
	}

	async init(id): Promise<void> {
		const marketInsightData = await window.database.CrmMarketInsight_MarketInsight
			.include("ProductFamily")
			.find(id);
		this.marketInsight(marketInsightData.asKoObservable());
		window.database.attachOrGet(this.marketInsight().innerInstance);
	}

	async save(): Promise<void> {
		this.loading(true);
		try {
			await window.database.saveChanges();
			this.loading(false);
			$(".modal:visible").modal("hide");
			await this.args.currentTab().init()
		} catch (e) {
			this.loading(false);
			window.swal(window.Helper.String.getTranslatedString("UnknownError"), window.Helper.String.getTranslatedString("Error_InternalServerError"), "error");
		}
	}

	dispose(): void {
		window.database.CrmMarketInsight_MarketInsight.detach(this.marketInsight().innerInstance);
	}

}
namespace("Crm.MarketInsight.ViewModels").MarketInsightEditModalViewModel = MarketInsightEditModalViewModel;

