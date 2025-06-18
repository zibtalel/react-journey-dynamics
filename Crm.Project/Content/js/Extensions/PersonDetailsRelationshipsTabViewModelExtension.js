(function() {
	var baseViewModel = window.Main.ViewModels.PersonDetailsRelationshipsTabViewModel;
	window.Main.ViewModels.PersonDetailsRelationshipsTabViewModel = function() {
		var viewModel = this;
		baseViewModel.apply(viewModel, arguments);
		var joinTags = {
			Selector: "Project.Tags",
			Operation: "orderBy(function(t) { return t.Name; })"
		};
		if (window.AuthorizationManager.currentUserIsAuthorizedForAction("Project", "RelationshipsTab")) {
			viewModel.genericProjectContactRelationships = new window.Main.ViewModels.GenericListViewModel(
				"CrmProject_ProjectContactRelationship",
				["RelationshipTypeKey"],
				["ASC"],
				["Project.Parent", "Project.ResponsibleUserUser", joinTags]);
			viewModel.genericProjectContactRelationships.getFilter("ChildId").extend({ filterOperator: "===" })(
				viewModel.personId);
			viewModel.projectContactRelationships = viewModel.genericProjectContactRelationships.items;
			viewModel.projectContactRelationships.distinct("RelationshipTypeKey");

			viewModel.subViewModels.push(viewModel.genericProjectContactRelationships);
			var joinPotentialTags = {
				Selector: "Potential.Tags",
				Operation: "orderBy(function(t) { return t.Name; })"
			};
			viewModel.genericPotentialContactRelationships = new window.Main.ViewModels.GenericListViewModel(
				"CrmProject_PotentialContactRelationship",
				["RelationshipTypeKey"],
				["ASC"],
				["Potential.Parent", "Potential.ResponsibleUserUser", joinPotentialTags]);
			viewModel.genericPotentialContactRelationships.getFilter("ChildId").extend({ filterOperator: "===" })(
				viewModel.companyId);
			viewModel.potentialContactRelationships = viewModel.genericPotentialContactRelationships.items;
			viewModel.potentialContactRelationships.distinct("RelationshipTypeKey");

			viewModel.subViewModels.push(viewModel.genericPotentialContactRelationships);
			viewModel.lookups = {
				projectContactsRelationshipTypes: { $tableName: "CrmProject_ProjectContactRelationshipType" },
				projectStatuses: { $tableName: "CrmProject_ProjectStatus" },
				currencies: { $tableName: "Main_Currency" },
				potentialContactsRelationshipTypes: { $tableName: "CrmProject_PotentialContactRelationshipType" },
				potentialStatuses: { $tableName: "CrmProject_PotentialStatus" },
				potentialPriorities: { $tableName: "CrmProject_PotentialPriority" },
				projectCategories: { $tableName: "CrmProject_ProjectCategory" },
				sourceType: { $tableName: "Main_SourceType" },
			};
		}
	}
	window.Main.ViewModels.PersonDetailsRelationshipsTabViewModel.prototype = baseViewModel.prototype;

	var baseInit = baseViewModel.prototype.init;
	window.Main.ViewModels.PersonDetailsRelationshipsTabViewModel.prototype.init = function(id, params) {
		var args = arguments;
		var viewModel = this;
		viewModel.loading(true);
		return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups)
			.then(function() {
				return baseInit.apply(viewModel, args);
			});
	};
})();