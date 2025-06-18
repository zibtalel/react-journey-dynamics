(function() {
	var baseViewModel = window.Main.ViewModels.BaseRelationshipsTabViewModel;
	namespace("Crm.Project.ViewModels").ProjectDetailsRelationshipsTabViewModel = function(parentViewModel) {
		var viewModel = this;
		baseViewModel.apply(viewModel, arguments);
		viewModel.projectId = parentViewModel.project().Id();
		var childCompanyAddresses = {
			Selector: "ChildCompany.Addresses",
			Operation: "filter(function (a) {return a.IsCompanyStandardAddress == true;})"
		};
		viewModel.genericProjectContactRelationships = new window.Main.ViewModels.GenericListViewModel("CrmProject_ProjectContactRelationship"
			, ["RelationshipTypeKey", "ChildCompany.Name", "ChildPerson.Surname", "ChildPerson.Firstname"], []
			, ["ChildCompany", childCompanyAddresses, "ChildCompany.ResponsibleUserUser", "ChildCompany.Tags", "ChildCompany.ParentCompany",
				"ChildPerson", "ChildPerson.Address", "ChildPerson.ResponsibleUserUser", "ChildPerson.Tags", "ChildPerson.Parent", "ChildPerson.Emails", "ChildPerson.Faxes", "ChildPerson.Phones"]);
		viewModel.genericProjectContactRelationships.getFilter("ParentId").extend({ filterOperator: "===" })(viewModel.projectId);
		viewModel.projectContactRelationships = viewModel.genericProjectContactRelationships.items;
		viewModel.projectContactRelationships.distinct("RelationshipTypeKey");

		viewModel.subViewModels.push(viewModel.genericProjectContactRelationships);
		viewModel.lookups = {
			projectContactsRelationshipTypes: { $tableName: "CrmProject_ProjectContactRelationshipType" },
			companyTypes: { $tableName: "Main_CompanyType" },
			projectCategories: { $tableName: "CrmProject_ProjectCategory" },
		};
		if (window.Main.Settings.Person.BusinessTitleIsLookup) {
			viewModel.lookups.businessTitles = { $tableName: "Main_BusinessTitle" }; 
		}
	};
	namespace("Crm.Project.ViewModels").ProjectDetailsRelationshipsTabViewModel.prototype = Object.create(baseViewModel.prototype);
	var baseInit = baseViewModel.prototype.init;
	namespace("Crm.Project.ViewModels").ProjectDetailsRelationshipsTabViewModel.prototype.init = function(id, params) {
		var args = arguments;
		var viewModel = this;
		viewModel.loading(true);
		return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups)
			.then(function() {
				return baseInit.apply(viewModel, args);
			});
	};
})();