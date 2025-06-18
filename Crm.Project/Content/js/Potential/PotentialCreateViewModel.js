namespace("Crm.Project.ViewModels").PotentialCreateViewModel = function () {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	viewModel.potential = window.ko.observable(null);
	viewModel.errors = window.ko.validation.group(viewModel.potential, { deep: true });
	viewModel.lookups = {
		sourceTypes: { $tableName: "Main_SourceType" },
		statuses: { $tableName: "CrmProject_PotentialStatus" },
		priorities: { $tableName: "CrmProject_PotentialPriority" }
	};
	viewModel.visibilityViewModel = new window.VisibilityViewModel(viewModel.potential, "Potential");
	viewModel.currentPrd = window.ko.observable(null);
}
namespace("Crm.Project.ViewModels").PotentialCreateViewModel.prototype.init = function (id, params) {
	var viewModel = this;
	var d = new $.Deferred();
	window.Helper.Database.initialize()
		.pipe(function () { return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups) })
		.pipe(function () {
			if (!!id) {
				return window.database.CrmProject_Potential.find(id);
			}
			var potential = window.database.CrmProject_Potential.CrmProject_Potential.create();
			potential.ParentId = params.companyId || null;
			potential.PriorityKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.priorities, potential.PriorityKey);
			potential.ProductFamilyKey = params.productFamilyKey || null;
			var currentUserName = document.getElementById("meta.CurrentUser").content;
			potential.ResponsibleUser = currentUserName;
			potential.SourceTypeKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.sourceTypes, potential.SourceTypeKey);
			var potentialStatus = window.ko.utils.arrayFirst(viewModel.lookups.statuses.$array, function (x) { return x.Key != null; });
			if (potentialStatus) {
				potential.StatusKey = potentialStatus.Key;
			}
			return potential;
		})
		.pipe(function (potential) {
			viewModel.potential(potential.asKoObservable());
			if (params) {
				if (params.parentId) {
					viewModel.potential().ParentId(params.parentId);
				}
				if (params.productFamilyKey) {
					viewModel.potential().MasterProductFamilyKey(params.productFamilyKey);
				}
				if (params.sourceTypeKey) {
					viewModel.potential().SourceTypeKey(params.sourceTypeKey);
				}
				if (params.campaignSource) {
					viewModel.potential().CampaignSource(params.campaignSource);
				}
			}
		})
		.pipe(function () {
			if (params.companyId) {
				return window.database.Main_Company.find(params.companyId).then(viewModel.onParentSelected.bind(viewModel));
			}
			return null;
		})
		.pipe(function () {
			if (params.productFamilyKey) {
				return window.database.CrmArticle_ProductFamily.find(params.productFamilyKey).then(viewModel.onProductFamilySelected.bind(viewModel));
			}
			return null;
		})
		.pipe(function () { return viewModel.visibilityViewModel.init(); })
		.pipe(function () {
			window.database.add(viewModel.potential().innerInstance);
		})
		.pipe(d.resolve)
		.fail(d.reject);
	return d.promise();
}
namespace("Crm.Project.ViewModels").PotentialCreateViewModel.prototype.cancel = function () {
	window.history.back();
}
namespace("Crm.Project.ViewModels").PotentialCreateViewModel.prototype.onParentSelected = function (company) {
	var viewModel = this;
	viewModel.potential().Parent(company);
}

namespace("Crm.Project.ViewModels").PotentialCreateViewModel.prototype.onProductFamilySelected = function (productFamily) {
	var viewModel = this;
	viewModel.potential().ProductFamily(productFamily);
}

namespace("Crm.Project.ViewModels").PotentialCreateViewModel.prototype.getParent = function (productFamilyId) {
	var viewModel = this;
	return window.database.CrmArticle_ProductFamily
		.find(productFamilyId)
		.then(function (result) {
			if (result.ParentId !== null) {
				viewModel.getParent(result.ParentId);
			}
			viewModel.currentPrd(result.asKoObservable());
			return result;
		});
}

namespace("Crm.Project.ViewModels").PotentialCreateViewModel.prototype.onConfirmProductFamily = function (productFamily) {
	var viewModel = this;
	viewModel.getParent(productFamily.Id);
}

namespace("Crm.Project.ViewModels").PotentialCreateViewModel.prototype.submit = function () {
	var viewModel = this;
	if (viewModel.potential().ProductFamilyKey() != null) {
		viewModel.potential().MasterProductFamilyKey(viewModel.currentPrd().Id);
	}
	viewModel.loading(true);
	if (viewModel.errors().length > 0) {
		viewModel.loading(false);
		viewModel.errors.showAllMessages();
		viewModel.errors.scrollToError();
		return;
	}

	return window.NumberingService.createNewNumberBasedOnAppSettings(window.Crm.Project.Settings.Potential.PotentialNoIsGenerated, window.Crm.Project.Settings.Potential.PotentialNoIsCreateable, viewModel.potential().PotentialNo(), "CRM.Potential", window.database.CrmProject_Potential, "PotentialNo")
		.pipe(function (potentialNo) {
			if (potentialNo !== undefined) {
				viewModel.potential().PotentialNo(potentialNo)
			}
			return window.database.saveChanges();
		}).pipe(function () {
			window.location.hash = "/Crm.Project/Potential/DetailsTemplate/" + viewModel.potential().Id();
		});
}
