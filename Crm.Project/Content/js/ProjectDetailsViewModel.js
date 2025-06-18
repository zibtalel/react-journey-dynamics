namespace("Crm.Project.ViewModels").ProjectDetailsViewModel = function() {
	var viewModel = this;
	viewModel.tabs = window.ko.observable({});
	viewModel.loading = window.ko.observable(true);
	viewModel.competitorCompanyTypeKeys = window.ko.observableArray([]);
	viewModel.project = window.ko.observable(null);
	viewModel.parentProductFamily = window.ko.observable(null);
	viewModel.projectStatuses = window.ko.observableArray([]);
	viewModel.sourceTypes = window.ko.observableArray([]);
	viewModel.standardAddress = window.ko.pureComputed(function() {
		var project = window.ko.unwrap(viewModel.project);
		if (project) {
			if (window.Crm.Project.Settings.ProjectsHaveAddresses) {
				var projectAddress = window.ko.unwrap(project.ProjectAddress);
				if (projectAddress.length > 1) {
					throw "more than one standard address found for project " + window.ko.unwrap(project.Id);
				}
				return projectAddress[0];
			} else {
				var parent = window.ko.unwrap(project.Parent);
				var parentAddresses = window.ko.unwrap(parent.Addresses);
				var standardAddresses = parentAddresses.filter(function (x) {
					return window.ko.unwrap(x.IsCompanyStandardAddress);
				});
				if (standardAddresses.length > 1) {
					throw "more than one standard address found for company " + window.ko.unwrap(parent.Id);
				}
				return standardAddresses[0];
			}
		}
		return null;
	});
	viewModel.primaryPhone = window.ko.pureComputed(function () {
		var standardAddress = viewModel.standardAddress();
		if (standardAddress) {
			return window.Helper.Address.getPrimaryCommunication(standardAddress.Phones);
		}
		return null;
	});
	viewModel.primaryEmail = window.ko.pureComputed(function () {
		var standardAddress = viewModel.standardAddress();
		if (standardAddress) {
			return window.Helper.Address.getPrimaryCommunication(standardAddress.Emails);
		}
		return null;
	});
	window.Main.ViewModels.ContactDetailsViewModel.apply(this, arguments);
	viewModel.contactType("Project");
	viewModel.lookups = {
		categories: { $tableName: "CrmProject_ProjectCategory" },
		currencies: { $tableName: "Main_Currency" },
		regions: { $tableName: "Main_Region"},
		countries: { $tableName: "Main_Country"},
		sourceTypes: { $tableName: "Main_SourceType"},
		projectLostReasonCategories: { $tableName: "CrmProject_ProjectLostReasonCategory" },
		projectStatuses: { $tableName: "CrmProject_ProjectStatus" },
		phoneTypes: { $tableName: "Main_PhoneType" },
	};
	if (window.Crm.Project.Settings.Configuration.BravoActiveForProjects) {
		viewModel.lookups.bravoCategories = { $tableName: "Main_BravoCategory" };
	}
	viewModel.settableStatuses = window.ko.pureComputed(function () {
		return viewModel.lookups.projectStatuses.$array.filter(function (status) {
			return status !== null
		});
	});
	viewModel.canSetStatus = window.ko.pureComputed(function () {
		return viewModel.settableStatuses().length > 1 &&
			window.AuthorizationManager.isAuthorizedForAction("Project", "SetStatus");
	});
	viewModel.isConfirm = ko.observable(false);
	viewModel.ratingValues = [1, 2, 3, 4, 5];
}
namespace("Crm.Project.ViewModels").ProjectDetailsViewModel.prototype = Object.create(window.Main.ViewModels.ContactDetailsViewModel.prototype);
namespace("Crm.Project.ViewModels").ProjectDetailsViewModel.prototype.init = function(id) {
	var viewModel = this;
	viewModel.contactId(id);
	return window.Main.ViewModels.ContactDetailsViewModel.prototype.init.apply(this, arguments)
		.pipe(function() {
			var query = window.database.CrmProject_Project
				.include2("Tags.orderBy(function(t) { return t.Name; })")
				.include("Competitor")
				.include("Potential")
				.include("MasterProductFamily")
				.include("ProductFamily")
				.include("ResponsibleUserUser");
			if (window.Crm.Project.Settings.ProjectsHaveAddresses) {
				query = query.include("ProjectAddress")
					.include("ProjectAddress.Emails")
					.include("ProjectAddress.Phones")
					.filter("it.ProjectAddress.IsCompanyStandardAddress === true");
			} else {
				query = query
					.include2("Parent.Addresses.filter(function(a) { return a.IsCompanyStandardAddress === true; })")
					.include("Parent.Addresses.Emails")
					.include("Parent.Addresses.Phones");
			}
			return query.find(id);
		})
		.pipe(function(project) {
			viewModel.project(project.asKoObservable());
			viewModel.contact(viewModel.project());
			viewModel.contactName(window.Helper.Project.getName(viewModel.project()));
			viewModel.dropboxName = window.ko.pureComputed(function () {
				return viewModel.project().ProjectNo() + "-" + viewModel.project().Parent().Name() + "-" + viewModel.project().Name().substring(0,25);
			});
			viewModel.isEditable(window.Crm.Project.Settings.AllowEditProjectWithLegacyId === true || project.LegacyId === null);
			return project;
		})
		.then(function () { return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups); })
		.then(function () {
			return window.Helper.Lookup.queryLookup("Main_CompanyType", null, "it.ExtensionValues.Competitor === true", {}).toArray();
		}).then(function(companyTypes){
			viewModel.competitorCompanyTypeKeys(companyTypes.map(function(x) { return x.Key; }));
			window.Helper.Database.registerEventHandlers(viewModel, {
				Main_Bravo: {
					afterCreate: viewModel.onBravoInsert,
					afterUpdate: viewModel.onBravoUpdate,
					afterDelete: viewModel.onBravoDelete
				}
			});
		})
		.then(function() { return viewModel.updateDisplayableBravos(false, false); })
		.then(function () { return viewModel.setVisibilityAlertText(); })
		.then(() => viewModel.setBreadcrumbs(id));
}
namespace("Crm.Project.ViewModels").ProjectDetailsViewModel.prototype.competitorFilter = function (query,term) {
	if (term) {
		query = query.filter('it.Name.toLowerCase().contains(this.term)', { term: term });
	}
	if (this.competitorCompanyTypeKeys().length > 0){
		query = query.filter("it.CompanyTypeKey in this.companyTypeKeys", { companyTypeKeys: this.competitorCompanyTypeKeys() });
	}
	return query;
};
namespace("Crm.Project.ViewModels").ProjectDetailsViewModel.prototype.setRating = function(rating) {
	var viewModel = this;
	if (viewModel.project().Rating() === rating) {
		return;
	}
	viewModel.loading(true);
	window.database.attachOrGet(viewModel.project().innerInstance);
	viewModel.project().Rating(rating);
	window.database.saveChanges().then(function() {
		viewModel.loading(false);
	});
};

namespace("Crm.Project.ViewModels").ProjectDetailsViewModel.prototype.setBreadcrumbs = function (id) {
	var viewModel = this;
	window.breadcrumbsViewModel.setBreadcrumbs([
		new Breadcrumb(Helper.String.getTranslatedString("Project"), "#/Crm.Project/ProjectList/IndexTemplate"),
		new Breadcrumb(Helper.Project.getName(viewModel.project), window.location.hash, null, id)
	]);
};

namespace("Crm.Project.ViewModels").ProjectDetailsViewModel.prototype.setCompany = function (company) {
	var viewModel = this;
	if (!company) {
		viewModel.project().Parent(null);
		return;
	}
	viewModel.project().Parent(company.asKoObservable());
};
namespace("Crm.Project.ViewModels").ProjectDetailsViewModel.prototype.onConfirmProductFamily = function (productFamily) {
	var viewModel = this;
	viewModel.isConfirm(true);
	var deferred = new $.Deferred();
	window.Helper.ProductFamily.getParent(productFamily.Id(), viewModel);
	deferred.resolve();
	return deferred.promise();
}
namespace("Crm.Project.ViewModels").ProjectDetailsViewModel.prototype.onSavePmbBlock = function () {
	const viewModel = this;
	if (viewModel.project().ProductFamily() && viewModel.isConfirm()) {
		viewModel.project().MasterProductFamilyKey(viewModel.parentProductFamily().Id());
		return
	}
	if (viewModel.project().MasterProductFamilyKey()) {
		viewModel.project().MasterProductFamilyKey(null)
	}
	if (viewModel.project().MasterProductFamily()) {
		viewModel.project().MasterProductFamily(null);
	}
}

