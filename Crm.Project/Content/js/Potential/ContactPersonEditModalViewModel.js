namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel = function (parentViewModel) {
	var viewModel = this;
	viewModel.documententry = window.ko.observable(null);
	viewModel.personTypes = window.ko.observable(null);
	viewModel.parent = window.ko.observable(null);
	viewModel.loading = window.ko.observable(true);
	viewModel.contactId = parentViewModel.contactId;
	viewModel.parentId = parentViewModel.potential().ParentId();
	viewModel.PersonKey = ko.observable(null);
	viewModel.disable = ko.observable(true);
	viewModel.categoryKey = window.ko.validatedObservable(null);
	viewModel.categoryKey.extend({
		required: {
			message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Category")),
			params: true
		}
	});
	viewModel.errors = window.ko.validation.group([viewModel.categoryKey, viewModel.documententry], { deep: true });
}

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype = Object.create(window.Main.ViewModels.ViewModelBase.prototype);

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.getAutoCompleterOptions = function () {
	var p = this.parent;
	if (this.personTypes() === 'Same') {
		return {
			table: "Main_Person",
			orderBy: ["Surname"],
			key: "Id",
			mapDisplayObject: window.Helper.Person.mapForSelect2Display,
			customFilter: function (query, term) {
				return window.Helper.Person.getSelect2FilterSame(query, term, p);
			}
		};
	} else {
		return {
			table: "Main_Person",
			orderBy: ["Surname"],
			key: "Id",
			mapDisplayObject: window.Helper.Person.mapForSelect2Display,
			customFilter: function (query, term) {
				return window.Helper.Person.getSelect2FilterOther(query, term, p);
			}
		}
	}
};

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.createNewEntity = function () {
	var entry = new window.database.CrmProject_DocumentEntry.createNew();
	entry.ContactKey = this.contactId();
	entry.Id = $data.createGuid().toString().toLowerCase();
	entry.SendDate = new Date();
	return entry;
};

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.init = function (id, params) {
	var viewModel = this;
	viewModel.personTypes(params.personTypes);
	viewModel.parent(params.parentId);
	return new $.Deferred().resolve().promise()
		.pipe(function () {
			var documententry = viewModel.createNewEntity();
			window.database.add(documententry);
			return documententry;
		})
		.pipe(function (documententry) {
			viewModel.documententry(documententry.asKoObservable());
		});
}

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.save = function () {
	var viewModel = this;
	viewModel.loading(true);
	if (viewModel.errors().length > 0) {
		viewModel.loading(false);
		viewModel.errors.showAllMessages();
		return;
	}
	var deferred = new $.Deferred();
	(deferred.resolve().promise()).then(function () {
		window.database.saveChanges()
			.then(function () {
				viewModel.loading(false);
				$(".modal:visible").modal("hide");
				viewModel.showContactHistoryTab();
			})
			.fail(function (e) {
				viewModel.loading(false);
				window.swal(window.Helper.String.getTranslatedString("UnknownError"), window.Helper.String.getTranslatedString("Error_InternalServerError"), "error");
			});
	});
}

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.showContactHistoryTab = function () {
	$(".tab-nav a[href='#tab-contact-history']").tab("show");
}

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.getDocumentSelect2Filter = function (query, term) {
	const viewModel = this;

	if (term) {
		query = query.filter("it.DocumentCategoryKey === this.categoryKey && it.Description.toLowerCase().contains(this.term)",
			{ categoryKey: ko.unwrap(viewModel.categoryKey), term: term });

	}
	return query.filter("it.DocumentCategoryKey === this.categoryKey",
		{ categoryKey: ko.unwrap(viewModel.categoryKey) });
}

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.getAutocompleterOptionsDocumentCategory = function () {
	const viewModel = this;
	return {
		customFilter: viewModel.getDocumentCategorySelect2Filter,
		table: "Main_DocumentCategory",
		mapDisplayObject: window.Helper.Lookup.mapLookupForSelect2Display,
		getElementByIdQuery: window.Helper.Lookup.getLookupByKeyQuery,
		onSelect: viewModel.onSelectCompany.bind(viewModel)
	}
}
namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.getDocumentCategorySelect2Filter = function (lookupName, key) {
	if (key) {
		return window.Helper.Lookup.getLocalizedQuery(lookupName, null, "it.Value.toLowerCase().contains(this.Key) && it.ExtensionValues.SalesRelated === true", { Key: key });
	} else {
		return window.Helper.Lookup.getLocalizedQuery(lookupName, null, "it.ExtensionValues.SalesRelated === true");
	}
}

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.onSelectCompany = function (category, data) {
	const viewModel = this;
	if (category === null) {
		viewModel.disable(true);
		viewModel.documententry().DocumentKey(null)
	}
	else {
		viewModel.disable(false);
	}
}

namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.formatResult = function (data) {
	if (data.item) {
		const formatDate = window.Globalize.formatDate(data.item.CreateDate, { datetime: 'medium' })
		const fileName = data.item.FileName;
		const createDate = `<span class="text-muted"> - ${formatDate} </span>`
		const text = `${fileName}`
		return $('<span></span>')
			.text(text)
			.append(createDate)
	}
}
namespace("Crm.Project.ViewModels").ContactPersonEditModalViewModel.prototype.mapLookupForSelect2Display = function (lookup) {

	return {
		id: lookup.Id,
		item: lookup,
		text: `	${lookup.FileName} - ${window.Globalize.formatDate(lookup.CreateDate, { datetime: 'medium' })}`
	};
}

