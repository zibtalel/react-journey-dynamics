window.Helper.Database.registerTable("CrmConfigurator_ConfigurationBase", {
	ConfigurationRules: {
		type: "Array",
		elementType: "Crm.Offline.DatabaseModel.CrmConfigurator_ConfigurationRule",
		inverseProperty: "ConfigurationBase",
		defaultValue: [],
		keys: ["ConfigurationBaseId"]
	},
	Variables: {
		type: "Array",
		elementType: "Crm.Offline.DatabaseModel.CrmConfigurator_Variable",
		inverseProperty: "ConfigurationBase",
		defaultValue: [],
		keys: ["ParentId"]
	}
});
window.Helper.Database.registerTable("CrmConfigurator_ConfigurationRule", {
	ConfigurationBase: {
		type: "Crm.Offline.DatabaseModel.CrmConfigurator_ConfigurationBase",
		inverseProperty: "ConfigurationRules",
		defaultValue: null,
		keys: ["ConfigurationBaseId"]
	}
});
window.Helper.Database.registerTable("CrmConfigurator_Variable", {
	ConfigurationBase: {
		type: "Crm.Offline.DatabaseModel.CrmConfigurator_ConfigurationBase",
		inverseProperty: "Variables",
		defaultValue: null,
		keys: ["ParentId"]
	}
});
window.Helper.Database.addIndex("CrmConfigurator_ConfigurationRule", ["ConfigurationBaseId"]);
window.Helper.Database.addIndex("CrmConfigurator_Variable", ["ParentId"]);