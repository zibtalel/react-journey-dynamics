namespace("Crm.Project.ViewModels").PotentialDetailsViewModel = function () {
	var viewModel = this;
	viewModel.tabs = window.ko.observable({});
	viewModel.loading = window.ko.observable(true);
	viewModel.potential = window.ko.observable(null);
	viewModel.company = window.ko.observable(null);
	viewModel.parentProductFamily = window.ko.observable(null);
	viewModel.potentialStatuses = window.ko.observableArray([]);
	viewModel.potentialPriorities = window.ko.observableArray([]);
	viewModel.sourceTypes = window.ko.observableArray([]);
	viewModel.standardAddress = window.ko.pureComputed(function () {
		var potential = window.ko.unwrap(viewModel.potential);
		if (potential) {
			var parent = window.ko.unwrap(potential.Parent);
			var addresses = window.ko.unwrap(parent.Addresses);
			var standardAddress = addresses.filter(function (x) { return x.IsCompanyStandardAddress() });
			if (standardAddress.length > 1) {
				throw "more than one standard address found for company " + window.ko.unwrap(parent.Id);
			}
			return standardAddress[0];
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
	viewModel.contactType("Potential");
	viewModel.lookups = {
		regions: { $tableName: "Main_Region"},
		countries: { $tableName: "Main_Country"},
		sourceTypes: { $tableName: "Main_SourceType"},
		documentCategory: { $tableName: "Main_DocumentCategory"},
		phoneTypes: { $tableName: "Main_PhoneType"},
		potentialStatuses: { $tableName: "CrmProject_PotentialStatus" },
		potentialPriorities: { $tableName: "CrmProject_PotentialPriority"}
	};
	viewModel.settableStatuses = window.ko.pureComputed(function () {
		return viewModel.lookups.potentialStatuses.$array.filter(function (status) {
			return status !== null
		});
	});
	viewModel.canSetStatus = window.ko.pureComputed(function () {
		return viewModel.settableStatuses().length > 1 &&
			window.AuthorizationManager.isAuthorizedForAction("Potential", "SetStatus");
	});
	viewModel.isConfirm = ko.observable(false);
}
namespace("Crm.Project.ViewModels").PotentialDetailsViewModel.prototype = Object.create(window.Main.ViewModels.ContactDetailsViewModel.prototype);
namespace("Crm.Project.ViewModels").PotentialDetailsViewModel.prototype.init = function (id) {
	var viewModel = this;
	viewModel.contactId(id);
	return window.Main.ViewModels.ContactDetailsViewModel.prototype.init.apply(this, arguments)
		.pipe(function () {
			return window.database.CrmProject_Potential
				.include("MasterProductFamily")
				.include("ProductFamily")
				.include2("Tags.orderBy(function(t) { return t.Name; })")
				.include2("Parent.Addresses.filter(function(a) { return a.IsCompanyStandardAddress === true; })")
				.include("Parent.Addresses.Emails")
				.include("Parent.Addresses.Phones")
				.include("ResponsibleUserUser")
				.find(id);
		})
		.pipe(function (potential) {
			viewModel.potential(potential.asKoObservable());
			viewModel.contact(viewModel.potential());
			viewModel.contactName(window.Helper.Potential.getName(viewModel.potential()));
			viewModel.company(viewModel.potential().Parent());
			viewModel.dropboxName = window.ko.pureComputed(function () {
				return viewModel.potential().PotentialNo() + "-" + viewModel.potential().Parent().Name() + "-" + viewModel.potential().Name().substring(0,25);
			});
			return potential;
		})
		.pipe(function (potential) {
			return window.database.Main_Note
				.filter("it.ContactId === potentialId", { potentialId: potential.Id })
				.orderByDescending("it.CreateDate")
				.map("it.ModifyDate")
				.take(1)
				.toArray()
				.then(function (result) {
					if (result.length > 0) {
						viewModel.potential().LastNoteDate(result[0])
					}
					return potential
				})
		})
		.then(function () { return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups)})
		.then(function () { return viewModel.setVisibilityAlertText(); })
		.then(() => viewModel.setBreadcrumbs(id));
}
namespace("Crm.Project.ViewModels").PotentialDetailsViewModel.prototype.setStatus = function(status) {
	var viewModel = this;
	if (viewModel.potential().StatusKey() === status.Key){
		return;
	}
	window.swal({
			title: window.Helper.String.getTranslatedString("PotentialStatus"),
			text: window.Helper.String.getTranslatedString("PotentialStatusChangeConfirmation"),
			type: "warning",
			showCancelButton: true,
			confirmButtonText: window.Helper.String.getTranslatedString("Ok"),
			closeOnConfirm: true
		},
		function(isConfirm) {
			if (isConfirm) {
				viewModel.loading(true);
				window.database.attachOrGet(viewModel.potential().innerInstance);
				viewModel.potential().StatusDate(new Date());
				viewModel.potential().StatusKey(status.Key);
				if (status.Key === 'closed') {
					viewModel.potential().CloseDate(new Date());
				}
				window.database.saveChanges().then(function () {
					viewModel.loading(false);
				});
				return true;
			}
		});
};
namespace("Crm.Project.ViewModels").PotentialDetailsViewModel.prototype.setBreadcrumbs = function (id) {
	var viewModel = this;
	window.breadcrumbsViewModel.setBreadcrumbs([
		new Breadcrumb(Helper.String.getTranslatedString("Potential"), "#/Crm.Project/PotentialList/IndexTemplate"),
		new Breadcrumb(Helper.Potential.getName(viewModel.potential), window.location.hash, null, id)
	]);
};

namespace("Crm.Project.ViewModels").PotentialDetailsViewModel.prototype.onConfirmProductFamily = function (productFamily) {
	const viewModel = this;
	viewModel.isConfirm(true)
	const deferred = new $.Deferred();
	window.Helper.ProductFamily.getParent(productFamily.Id(), viewModel);
	deferred.resolve();
	return deferred.promise();
}

namespace("Crm.Project.ViewModels").PotentialDetailsViewModel.prototype.onSavePmbBlock = function () {
	const viewModel = this;
	if (viewModel.potential().ProductFamily() && viewModel.isConfirm()) {
		viewModel.potential().MasterProductFamilyKey(viewModel.parentProductFamily().Id());
		return
	}
	if (viewModel.potential().MasterProductFamilyKey()) {
		viewModel.potential().MasterProductFamilyKey(null)
	}
	if (viewModel.potential().MasterProductFamily()) {
		viewModel.potential().MasterProductFamily(null);
	}
}
