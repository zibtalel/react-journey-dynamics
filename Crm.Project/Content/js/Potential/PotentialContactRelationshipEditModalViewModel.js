(function (ko) {
	var baseViewModel = window.Main.ViewModels.BaseRelationshipEditModalViewModel;
	namespace("Crm.Project.ViewModels").PotentialContactRelationshipEditModalViewModel = function (parentViewModel) {
		baseViewModel.apply(this, arguments);
		var viewModel = this;
		viewModel.table = window.database.CrmProject_PotentialContactRelationship;
		viewModel.lookups = parentViewModel.tabs()["tab-relationships"]().lookups;
		viewModel.relationshipTypeLookup = viewModel.lookups.potentialContactsRelationshipTypes;
		viewModel.setMode(parentViewModel);
	};
	namespace("Crm.Project.ViewModels").PotentialContactRelationshipEditModalViewModel.prototype = Object.create(baseViewModel.prototype);
	namespace("Crm.Project.ViewModels").PotentialContactRelationshipEditModalViewModel.prototype.setMode = function (parentViewModel) {
		if (parentViewModel instanceof window.Main.ViewModels.CompanyDetailsViewModel) {
			this.mode = "company";
		} else if (parentViewModel instanceof window.Crm.Project.ViewModels.PotentialDetailsViewModel) {
			this.mode = "potential";
		} else if (parentViewModel instanceof window.Main.ViewModels.PersonDetailsViewModel) {
			this.mode = "person";
		}
	};
	namespace("Crm.Project.ViewModels").PotentialContactRelationshipEditModalViewModel.prototype.init = function (id, params) {
		var viewModel = this;
		viewModel.contactType = params.contactType || "";
		viewModel.showCustomPersonSelector(true);

		baseViewModel.prototype.init.apply(this, arguments);
	};
	namespace("Crm.Project.ViewModels").PotentialContactRelationshipEditModalViewModel.prototype.getQueryForEditing = function () {
		return window.database.CrmProject_PotentialContactRelationship
			.include("ChildPerson")
			.include("ChildCompany");
	};
	namespace("Crm.Project.ViewModels").PotentialContactRelationshipEditModalViewModel.prototype.getEditableId = function (relationship) {
		switch (this.mode) {
			case "person":
			case "company": return relationship.ParentId;
			case "potential": return relationship.ChildId;
		}
		return baseViewModel.prototype.getEditableId.apply(this, arguments);
	};
	namespace("Crm.Project.ViewModels").PotentialContactRelationshipEditModalViewModel.prototype.getAutoCompleterOptions = function () {
		const viewModel = this;
		switch (this.mode) {
			case "person":
			case "company":
				return {
					key: "Id",
					table: 'CrmProject_Potential',
					orderBy: ['Name'],
					mapDisplayObject: window.Helper.Potential.mapDisplayNameForSelect2
				}
			case "potential":
				var companySource = { table: "Main_Company", orderBy: ["Name"], key: "Id", mapDisplayObject: window.Helper.Company.mapForSelect2Display, customFilter: window.Helper.Company.getSelect2Filter };
				var personSource = {
					table: "Main_Person",
					orderBy: ["Surname"],
					key: "Id",
					mapDisplayObject: window.Helper.Person.customMapForSelect2Display,
					customFilter: function (query, term,) {
						return viewModel.showAllPersons() === true
							? window.Helper.Person.getSelect2FilterOther(query, term, viewModel.parent().Id)
							: window.Helper.Person.getSelect2FilterSame(query, term, viewModel.parent().Id)
					},
					templateResult: window.Helper.Person.formatResult
				};
				var relationship = ko.unwrap(this.relationship) || {};
				if (ko.unwrap(relationship.ChildPerson)) {
					return personSource;
				} else if (ko.unwrap(relationship.ChildCompany)) {
					return companySource;
				} else {
					if (this.contactType === "Company")
						return companySource;
					else
						return personSource;
				}
		}
		return baseViewModel.prototype.getAutoCompleterOptions.apply(this, arguments);
	};
	namespace("Crm.Project.ViewModels").PotentialContactRelationshipEditModalViewModel.prototype.getAutoCompleterCaption = function () {
		switch (this.mode) {
			case "person":
			case "company": return "Potential";
			case "potential":
				if (this.contactType === "Company")
					return "Company";
				return "Person";
		}
		return baseViewModel.prototype.getAutoCompleterCaption.apply(this, arguments);
	};
	namespace("Crm.Project.ViewModels").PotentialContactRelationshipEditModalViewModel.prototype.createNewEntity = function () {
		var relationship = baseViewModel.prototype.createNewEntity.apply(this, arguments);
		switch (this.mode) {

			case "potential":
				relationship.ParentId = this.contactId();
				break;
			case "person":
			case "company":
				relationship.ParentId = null;
				relationship.ChildId = this.contactId();
				break;
			default:
				break;
		}
		return relationship;
	};
})(ko);