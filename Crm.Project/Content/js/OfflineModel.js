window.Helper.Database.registerTable("CrmProject_Project", {
	Bravos: { 
		type: "Array",
		elementType: "Crm.Offline.DatabaseModel.Main_Bravo",
		inverseProperty: "$$unbound",
		defaultValue: [],
		keys: ["ContactId"]
	},
	Competitor: {
		type: "Crm.Offline.DatabaseModel.Main_Company",
		inverseProperty: "$$unbound",
		defaultValue: null,
		keys: ["CompetitorId"]
	},
	Parent: {
		type: "Crm.Offline.DatabaseModel.Main_Company",
		inverseProperty: "$$unbound",
		defaultValue: null,
		keys: ["ParentId"]
	},
	Potential: {
		type: "Crm.Offline.DatabaseModel.CrmProject_Potential",
		inverseProperty: "$$unbound",
		defaultValue: null,
		keys: ["PotentialId"]
	},
	ProjectAddress: {
		type: "Array",
		elementType: "Crm.Offline.DatabaseModel.Main_Address",
		inverseProperty: "$$unbound",
		defaultValue: null,
		keys: ["CompanyId"]
	},
	Tags: {
		type: "Array",
		elementType: "Crm.Offline.DatabaseModel.Main_Tag",
		inverseProperty: "$$unbound",
		defaultValue: [],
		keys: ["ContactKey"]
	},
	ResponsibleUserUser: { type: "Crm.Offline.DatabaseModel.Main_User", inverseProperty: "$$unbound", defaultValue: null, keys: ["ResponsibleUser"] },
	MasterProductFamily: { type: "Crm.Offline.DatabaseModel.CrmArticle_ProductFamily", inverseProperty: "$$unbound", defaultValue: null, keys: ["MasterProductFamilyKey"] },
	ProductFamily: { type: "Crm.Offline.DatabaseModel.CrmArticle_ProductFamily", inverseProperty: "$$unbound", defaultValue: null, keys: ["ProductFamilyKey"] }
});
window.Helper.Database.registerTable("CrmProject_ProjectContactRelationship", {
	Project: { type: "Crm.Offline.DatabaseModel.CrmProject_Project", inverseProperty: "$$unbound", defaultValue: null, keys: ["ParentId"] },
	ChildPerson: { type: "Crm.Offline.DatabaseModel.Main_Person", inverseProperty: "$$unbound", defaultValue: null, keys: ["ChildId"] },
	ChildCompany: { type: "Crm.Offline.DatabaseModel.Main_Company", inverseProperty: "$$unbound", defaultValue: null, keys: ["ChildId"] }
});
window.Helper.Database.addIndex("CrmProject_Project", ["ParentId"]);
window.Helper.Database.addIndex("CrmProject_Project", ["ResponsibleUser"]);
window.Helper.Database.addIndex("CrmProject_ProjectContactRelationship", ["ChildId"]);

window.Helper.Database.setTransactionId("CrmProject_Project",
	function (project) {
		return new $.Deferred().resolve([project.Id, project.CampaignSource, project.ParentId]).promise();
	});
window.Helper.Database.setTransactionId("CrmProject_ProjectContactRelationship",
	function (relationship) {
		return new $.Deferred().resolve([relationship.ParentId, relationship.ChildId]).promise();
	});

window.Helper.Database.registerTable("CrmProject_Potential", {
	Parent: {
		type: "Crm.Offline.DatabaseModel.Main_Company",
		inverseProperty: "$$unbound",
		defaultValue: null,
		keys: ["ParentId"]
	},
	PotentialAddress: {
		type: "Array",
		elementType: "Crm.Offline.DatabaseModel.Main_Address",
		inverseProperty: "$$unbound",
		defaultValue: null,
		keys: ["CompanyId"]
	},
	Tags: {
		type: "Array",
		elementType: "Crm.Offline.DatabaseModel.Main_Tag",
		inverseProperty: "$$unbound",
		defaultValue: [],
		keys: ["ContactKey"]
	},
	ResponsibleUserUser: { type: "Crm.Offline.DatabaseModel.Main_User", inverseProperty: "$$unbound", defaultValue: null, keys: ["ResponsibleUser"] },
	MasterProductFamily: { type: "Crm.Offline.DatabaseModel.CrmArticle_ProductFamily", inverseProperty: "$$unbound", defaultValue: null, keys: ["MasterProductFamilyKey"] },
	ProductFamily: { type: "Crm.Offline.DatabaseModel.CrmArticle_ProductFamily", inverseProperty: "$$unbound", defaultValue: null, keys: ["ProductFamilyKey"] }
});
window.Helper.Database.registerTable("CrmProject_PotentialContactRelationship", {
	Potential: { type: "Crm.Offline.DatabaseModel.CrmProject_Potential", inverseProperty: "$$unbound", defaultValue: null, keys: ["ParentId"] },
	ChildPerson: { type: "Crm.Offline.DatabaseModel.Main_Person", inverseProperty: "$$unbound", defaultValue: null, keys: ["ChildId"] },
	ChildCompany: { type: "Crm.Offline.DatabaseModel.Main_Company", inverseProperty: "$$unbound", defaultValue: null, keys: ["ChildId"] }
});
window.Helper.Database.addIndex("CrmProject_Potential", ["ParentId"]);
window.Helper.Database.addIndex("CrmProject_Potential", ["ResponsibleUser"]);
window.Helper.Database.addIndex("CrmProject_PotentialContactRelationship", ["ChildId"]);

window.Helper.Database.setTransactionId("CrmProject_Potential",
	function (potential) {
		return new $.Deferred().resolve([potential.Id, potential.CampaignSource, potential.ParentId]).promise();
	});
window.Helper.Database.setTransactionId("CrmProject_PotentialContactRelationship",
	function (relationship) {
		return new $.Deferred().resolve([relationship.ParentId, relationship.ChildId]).promise();
	});

window.Helper.Database.registerTable("CrmProject_DocumentEntry", {
	Person: {
		type: "Crm.Offline.DatabaseModel.Main_Person",
		inverseProperty: "$$unbound",
		defaultValue: null,
		keys: ["PersonKey"]
	},
	Document: {
		type: "Crm.Offline.DatabaseModel.Main_DocumentAttribute",
		inverseProperty: "$$unbound",
		defaultValue: null,
		keys: ["DocumentKey"]
	}
});
window.Helper.Database.addIndex("CrmProject_DocumentEntry", ["PersonKey"]);
window.Helper.Database.setTransactionId("CrmProject_DocumentEntry",
	function (entry) {
		return new $.Deferred().resolve([entry.Id, entry.PersonKey]).promise();
	});
