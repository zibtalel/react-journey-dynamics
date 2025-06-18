namespace("Crm.Project.ViewModels").ProjectCreateViewModel = function () {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	viewModel.project = window.ko.observable(null);
	viewModel.potential = window.ko.observable(null);
	viewModel.errors = window.ko.validation.group(viewModel.project, { deep: true });
	viewModel.lookups = {
		categories: { $tableName: "CrmProject_ProjectCategory" },
		currencies: { $tableName: "Main_Currency" },
		sourceTypes: { $tableName: "Main_SourceType" },
		statuses: { $tableName: "CrmProject_ProjectStatus" }
	};
	viewModel.visibilityViewModel = new window.VisibilityViewModel(viewModel.project, "Project");
	viewModel.currentPrd = window.ko.observable(null);
}
namespace("Crm.Project.ViewModels").ProjectCreateViewModel.prototype.init = function (id, params) {
	var viewModel = this;
	var d = new $.Deferred();

	window.Helper.Database.initialize()
		.pipe(function () { return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups) })
		.pipe(function () {
			if (!!params.potentialId) {
				return window.database.CrmProject_Potential.filter(function (x) { return x.Id == this.potentialId }, { potentialId: params.potentialId })
					.toArray(function (potentials) { viewModel.potential(potentials[0]) });
			}
		})
		.pipe(function () {
			if (!!id) {
				return window.database.CrmProject_Project.find(id);
			}
			var project = window.database.CrmProject_Project.CrmProject_Project.create();
			project.CategoryKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.categories, project.CategoryKey);
			project.CurrencyKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.currencies, project.CurrencyKey);
			project.SourceTypeKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.sourceTypes, project.SourceTypeKey);
			if (viewModel.potential() != null) {
				project.ParentId = viewModel.potential().ParentId;
				project.PotentialId = viewModel.potential().Id;
				project.SourceTypeKey = viewModel.potential().SourceTypeKey;
				project.CampaignSource = viewModel.potential().CampaignSource;
			}
			project.ParentId = params.companyId || null;
			project.ProductFamilyKey = params.productFamilyKey || null;
			var currentUserName = document.getElementById("meta.CurrentUser").content;
			project.ResponsibleUser = currentUserName;
			var projectStatus = window.ko.utils.arrayFirst(viewModel.lookups.statuses.$array, function (x) { return x.Key != null; });
			if (projectStatus) {
				project.StatusKey = projectStatus.Key;
			}
			return project;
		})
		.pipe(function (project) {
			viewModel.project(project.asKoObservable());
			if (params) {
				if (params.parentId) {
					viewModel.project().ParentId(params.parentId);
				}
				if (params.productFamilyKey) {
					viewModel.project().MasterProductFamilyKey(params.productFamilyKey);
				}
				if (params.sourceTypeKey) {
					viewModel.project().SourceTypeKey(params.sourceTypeKey);
				}
				if (params.campaignSource) {
					viewModel.project().CampaignSource(params.campaignSource);
				}
				if (params.parentId) {
					viewModel.project().ParentId(params.parentId);
				}
			}
		})
		.pipe(function () {
			if (params.productFamilyKey) {
				return window.database.CrmArticle_ProductFamily.find(params.productFamilyKey).then(viewModel.onProductFamilySelected.bind(viewModel));
			}
			return null;
		})
		.pipe(function () { return viewModel.visibilityViewModel.init(); })
		.pipe(function () {
			window.database.add(viewModel.project().innerInstance);
		})
		.pipe(d.resolve)
		.fail(d.reject);
	return d.promise();
}
namespace("Crm.Project.ViewModels").ProjectCreateViewModel.prototype.cancel = function () {
	window.database.detach(this.project().innerInstance);
	window.history.back();
}
namespace("Crm.Project.ViewModels").ProjectCreateViewModel.prototype.onProductFamilySelected = function (productFamily) {
	var viewModel = this;
	viewModel.project().ProductFamily(productFamily);
}
namespace("Crm.Project.ViewModels").ProjectCreateViewModel.prototype.getParent = function (productFamilyId) {
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
namespace("Crm.Project.ViewModels").ProjectCreateViewModel.prototype.onConfirmProductFamily = function (productFamily) {
	var viewModel = this;
	viewModel.getParent(productFamily.Id);
}
namespace("Crm.Project.ViewModels").ProjectCreateViewModel.prototype.submit = function () {
	var viewModel = this;
	if (viewModel.project().ProductFamilyKey() != null) {
		viewModel.project().MasterProductFamilyKey(viewModel.currentPrd().Id);
	}
	viewModel.loading(true);
	if (viewModel.errors().length > 0) {
		viewModel.loading(false);
		viewModel.errors.showAllMessages();
		viewModel.errors.scrollToError();
		return;
	}

	return window.NumberingService.createNewNumberBasedOnAppSettings(window.Crm.Project.Settings.ProjectNoIsGenerated, window.Crm.Project.Settings.ProjectNoIsCreateable, viewModel.project().ProjectNo(), "CRM.Project", window.database.CrmProject_Project, "ProjectNo")
		.pipe(function (projectNo) {
			if (projectNo !== undefined) {
				viewModel.project().ProjectNo(projectNo);
			}
			return window.database.saveChanges()
		})
		.pipe(function () {
			if (viewModel.potential() != null) {
				window.database.attachOrGet(viewModel.potential());
				viewModel.potential().StatusKey = 'closed';
				viewModel.potential().CloseDate = new Date();
				viewModel.potential().StatusDate = new Date();
			}
		}).pipe(function () {
			window.location.hash = "/Crm.Project/Project/DetailsTemplate/" + viewModel.project().Id();
		});
}
