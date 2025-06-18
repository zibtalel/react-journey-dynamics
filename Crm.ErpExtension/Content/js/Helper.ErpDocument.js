class HelperErpDocument {
	static getDocumentAbbreviation(erpDocument, lookup) {
		const lookupValue = lookup[erpDocument.StatusKey()];
		return lookupValue ? lookupValue.Value[0] : erpDocument.StatusKey()[0];
	}

	static getDocumentColor(erpDocument, lookup) {
		const lookupValue = lookup[erpDocument.StatusKey()];
		return lookupValue ? lookupValue.Color : "#607D8B";
	}
	
	static getContactDisplayName(doc) {
		return [ko.unwrap(doc.CompanyNo), ko.unwrap(doc.CompanyName)].filter(Boolean).join(" - ");
	}
	
	static showContactLink(doc) {
		return ko.unwrap(doc.ContactType) && ko.unwrap(doc.ContactKey);
	}

	static getDocumentNo(doc) {
		var erpDoc = ko.unwrap(doc);
		if (erpDoc.DeliveryNoteNo) return ko.unwrap(erpDoc.DeliveryNoteNo);
		if (erpDoc.CreditNoteNo) return ko.unwrap(erpDoc.CreditNoteNo);
		if (erpDoc.InvoiceNo) return ko.unwrap(erpDoc.InvoiceNo);
		if (erpDoc.QuoteNo) return ko.unwrap(erpDoc.QuoteNo);
		if (erpDoc.OrderNo) return ko.unwrap(erpDoc.OrderNo);
		return null;
	}
}


(window.Helper = window.Helper || {}).ErpDocument = HelperErpDocument;
