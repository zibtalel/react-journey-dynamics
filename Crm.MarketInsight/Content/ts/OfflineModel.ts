///<reference path="../../../../Content/@types/index.d.ts" />

window.Helper.Database.registerTable("CrmMarketInsight_MarketInsight",
	{
		Company: {
			type: "Crm.Offline.DatabaseModel.Main_Company",
			inverseProperty: "$$unbound",
			defaultValue: null,
			keys: ["ParentId"]
		},
		ProductFamily: {
			type: "Crm.Offline.DatabaseModel.CrmArticle_ProductFamily",
			inverseProperty: "$$unbound",
			defaultValue: null,
			keys: ["ProductFamilyKey"]
		},
		Relationships: {
			type: "Array",
			elementType: "Crm.Offline.DatabaseModel.CrmMarketInsight_MarketInsightContactRelationship",
			inverseProperty: "$$unbound",
			defaultValue: [],
			keys: ["ContactKey"]
		}
	},null);
window.Helper.Database.registerTable("Main_Company",
	{
		MarketInsights: {
			type: "Array",
			elementType: "Crm.Offline.DatabaseModel.CrmMarketInsight_MarketInsight",
			inverseProperty: "$$unbound",
			defaultValue: [],
			keys: ["ParentId"]
		}
	},null);

export {}