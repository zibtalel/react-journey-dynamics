(function () {
	var baseViewModel = window.Main.ViewModels.BaseRelationshipsTabViewModel;
	namespace("Crm.Project.ViewModels").PotentialDetailsRelationshipsTabViewModel = function (parentViewModel) {
		var viewModel = this;
		baseViewModel.apply(viewModel, arguments);
		viewModel.potentialId = parentViewModel.potential().Id();
		var childCompanyAddresses = {
			Selector: "ChildCompany.Addresses",
			Operation: "filter(function (a) {return a.IsCompanyStandardAddress == true;})"
		};

		viewModel.genericPotentialContactRelationships = new window.Main.ViewModels.GenericListViewModel("CrmProject_PotentialContactRelationship"
			, ["RelationshipTypeKey", "ChildCompany.Name", "ChildPerson.Surname", "ChildPerson.Firstname"], []
			, ["ChildCompany", childCompanyAddresses, "ChildCompany.ResponsibleUserUser", "ChildCompany.Tags", "ChildCompany.ParentCompany",
				"ChildPerson", "ChildPerson.Address", "ChildPerson.ResponsibleUserUser", "ChildPerson.Tags", "ChildPerson.Parent", "ChildPerson.Emails", "ChildPerson.Faxes", "ChildPerson.Phones"]);

		viewModel.genericPotentialContactRelationships.getFilter("ParentId").extend({ filterOperator: "===" })(viewModel.potentialId);
		viewModel.potentialContactRelationships = viewModel.genericPotentialContactRelationships.items;
		viewModel.potentialContactRelationships.distinct("RelationshipTypeKey");
		viewModel.subViewModels.push(viewModel.genericPotentialContactRelationships);
		viewModel.lookups = {
			phoneTypes: { $tableName: "Main_PhoneType" },
			companyTypes: { $tableName: "Main_CompanyType"},
			potentialContactsRelationshipTypes: { $tableName: "CrmProject_PotentialContactRelationshipType" }
		}
	};
	namespace("Crm.Project.ViewModels").PotentialDetailsRelationshipsTabViewModel.prototype = Object.create(baseViewModel.prototype);
	var baseInit = baseViewModel.prototype.init;
	namespace("Crm.Project.ViewModels").PotentialDetailsRelationshipsTabViewModel.prototype.init = function (id, params) {
		var args = arguments;
		var viewModel = this;
		viewModel.loading(true);
		return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups)
			.then(function () {
				if (window.Main.Settings.Person.BusinessTitleIsLookup) {
					return window.Helper.Lookup.getLocalizedArrayMap("Main_BusinessTitle").then(function (lookup) { viewModel.lookups.businessTitles = lookup; });
				}
				return null;
			})
			.then(function () {
				return baseInit.apply(viewModel, args);
			});
	};
})();
