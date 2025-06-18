window.Helper.Database.addIndex("CrmErpExtension_CreditNote", ["ContactKey"]);
window.Helper.Database.addIndex("CrmErpExtension_CreditNote", ["ContactKey", "StatusKey"]);
window.Helper.Database.addIndex("CrmErpExtension_CreditNote", ["OrderNo", "ModifyDate"]);
window.Helper.Database.addIndex("CrmErpExtension_DeliveryNote", ["ContactKey"]);
window.Helper.Database.addIndex("CrmErpExtension_DeliveryNote", ["ContactKey", "StatusKey"]);
window.Helper.Database.addIndex("CrmErpExtension_DeliveryNote", ["OrderNo", "ModifyDate"]);
window.Helper.Database.addIndex("CrmErpExtension_Invoice", ["ContactKey"]);
window.Helper.Database.addIndex("CrmErpExtension_Invoice", ["ContactKey", "StatusKey"]);
window.Helper.Database.addIndex("CrmErpExtension_Invoice", ["OrderNo", "ModifyDate"]);
window.Helper.Database.addIndex("CrmErpExtension_MasterContract", ["ContactKey"]);
window.Helper.Database.addIndex("CrmErpExtension_MasterContract", ["ContactKey", "StatusKey"]);
window.Helper.Database.addIndex("CrmErpExtension_MasterContract", ["OrderNo", "ModifyDate"]);
window.Helper.Database.addIndex("CrmErpExtension_Quote", ["ContactKey"]);
window.Helper.Database.addIndex("CrmErpExtension_Quote", ["ContactKey", "StatusKey"]);
window.Helper.Database.addIndex("CrmErpExtension_Quote", ["OrderNo", "ModifyDate"]);
window.Helper.Database.addIndex("CrmErpExtension_SalesOrder", ["ContactKey"]);
window.Helper.Database.addIndex("CrmErpExtension_SalesOrder", ["ContactKey", "StatusKey"]);
window.Helper.Database.addIndex("CrmErpExtension_SalesOrder", ["OrderNo", "ModifyDate"]);
window.Helper.Database.addIndex("CrmErpExtension_ErpTurnover", ["ContactKey", "Total", "IsVolume", "CurrencyKey", "QuantityUnitKey", "Year"]);
//
window.Helper.Database.registerTable("CrmErpExtension_SalesOrder", {
    Positions: { type: "Array", elementType: "Crm.Offline.DatabaseModel.CrmErpExtension_SalesOrderPosition", inverseProperty: "Parent", defaultValue: [], keys: ["ParentKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_Quote", {
    Positions: { type: "Array", elementType: "Crm.Offline.DatabaseModel.CrmErpExtension_QuotePosition", inverseProperty: "Parent", defaultValue: [], keys: ["ParentKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_CreditNote", {
    Positions: { type: "Array", elementType: "Crm.Offline.DatabaseModel.CrmErpExtension_CreditNotePosition", inverseProperty: "Parent", defaultValue: [], keys: ["ParentKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_DeliveryNote", {
    Positions: { type: "Array", elementType: "Crm.Offline.DatabaseModel.CrmErpExtension_DeliveryNotePosition", inverseProperty: "Parent", defaultValue: [], keys: ["ParentKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_MasterContract", {
    Positions: { type: "Array", elementType: "Crm.Offline.DatabaseModel.CrmErpExtension_MasterContractPosition", inverseProperty: "Parent", defaultValue: [], keys: ["ParentKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_Invoice", {
    Positions: { type: "Array", elementType: "Crm.Offline.DatabaseModel.CrmErpExtension_InvoicePosition", inverseProperty: "Parent", defaultValue: [], keys: ["ParentKey"] },
});


window.Helper.Database.registerTable("CrmErpExtension_SalesOrderPosition", {
    Parent: { type: "Crm.Offline.DatabaseModel.CrmErpExtension_SalesOrder", inverseProperty: "Positions", defaultValue: null, keys: ["ParentKey"] },
    Article: { type: "Crm.Offline.DatabaseModel.CrmArticle_Article", inverseProperty: "$$unbound", defaultValue: null, keys: ["ArticleKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_QuotePosition", {
    Parent: { type: "Crm.Offline.DatabaseModel.CrmErpExtension_Quote", inverseProperty: "Positions", defaultValue: null, keys: ["ParentKey"] },
    Article: { type: "Crm.Offline.DatabaseModel.CrmArticle_Article", inverseProperty: "$$unbound", defaultValue: null, keys: ["ArticleKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_CreditNotePosition", {
    Parent: { type: "Crm.Offline.DatabaseModel.CrmErpExtension_CreditNote", inverseProperty: "Positions", defaultValue: null, keys: ["ParentKey"] },
    Article: { type: "Crm.Offline.DatabaseModel.CrmArticle_Article", inverseProperty: "$$unbound", defaultValue: null, keys: ["ArticleKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_DeliveryNotePosition", {
    Parent: { type: "Crm.Offline.DatabaseModel.CrmErpExtension_DeliveryNote", inverseProperty: "Positions", defaultValue: null, keys: ["ParentKey"] },
    Article: { type: "Crm.Offline.DatabaseModel.CrmArticle_Article", inverseProperty: "$$unbound", defaultValue: null, keys: ["ArticleKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_MasterContractPosition", {
    Parent: { type: "Crm.Offline.DatabaseModel.CrmErpExtension_MasterContract", inverseProperty: "Positions", defaultValue: null, keys: ["ParentKey"] },
    Article: { type: "Crm.Offline.DatabaseModel.CrmArticle_Article", inverseProperty: "$$unbound", defaultValue: null, keys: ["ArticleKey"] },
});
window.Helper.Database.registerTable("CrmErpExtension_InvoicePosition", {
    Parent: { type: "Crm.Offline.DatabaseModel.CrmErpExtension_Invoice", inverseProperty: "Positions", defaultValue: null, keys: ["ParentKey"] },
    Article: { type: "Crm.Offline.DatabaseModel.CrmArticle_Article", inverseProperty: "$$unbound", defaultValue: null, keys: ["ArticleKey"] },
});
