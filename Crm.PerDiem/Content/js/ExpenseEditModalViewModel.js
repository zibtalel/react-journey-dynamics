namespace("Crm.PerDiem.ViewModels").ExpenseEditModalViewModel = function() {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	viewModel.expense = window.ko.observable(null);
	viewModel.fileResource = window.ko.observable(null);
	viewModel.imageCompressionQualityLevel = window.Helper.Image.CompressionQualityLevel.Poor;
	viewModel.minDate = window.ko.observable(window.Crm.PerDiem.Settings.Expense.MaxDaysAgo ? window.moment().startOf("day").utc().add(-parseInt(window.Crm.PerDiem.Settings.Expense.MaxDaysAgo), "days") : false);
	viewModel.maxDate = window.ko.pureComputed(function() {
		return new Date();
	});
	viewModel.selectedExpenseType = window.ko.observable(null);
	viewModel.validCostCenters = window.ko.computed(function() {
		return viewModel.selectedExpenseType() ? viewModel.selectedExpenseType().ValidCostCenters() : [];
	});
	viewModel.lookups = {
		costCenters: { $tableName: "Main_CostCenter" },
		currencies: { $tableName: "Main_Currency" },
		expenseTypes: { $tableName: "CrmPerDiem_ExpenseType" }
	}
};
namespace("Crm.PerDiem.ViewModels").ExpenseEditModalViewModel.prototype.init = function(id, params) {
	var viewModel = this;
	return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups).then(function() {
		if (id) {
			return window.database.CrmPerDiem_UserExpense.find(id);
		}
		var newExpense = window.database.CrmPerDiem_UserExpense.CrmPerDiem_UserExpense.create();
		newExpense.CostCenterKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.costCenters, newExpense.CostCenterKey);
		newExpense.CurrencyKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.currencies, newExpense.CurrencyKey);
		newExpense.Date = window.moment(params.selectedDate).toDate();
		newExpense.ExpenseTypeKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.expenseTypes, newExpense.ExpenseTypeKey);
		newExpense.ResponsibleUser = params.username;
		return newExpense;
	}).then(function(expense) {
		viewModel.expense(expense.asKoObservable());
		if (expense.FileResourceKey) {
			return window.database.Main_FileResource.find(expense.FileResourceKey)
				.then(window.database.attachOrGet.bind(window.database));
		}
		var newFileResource = window.database.Main_FileResource.Main_FileResource.create();
		window.database.add(newFileResource);
		return newFileResource;
	}).then(function(fileResource) {
		viewModel.fileResource(fileResource.asKoObservable());
		var expense = Helper.Database.getDatabaseEntity(viewModel.expense);
		if (expense.Id !== Helper.String.emptyGuid()) {
			window.database.attachOrGet(expense);
		} else {
			window.database.add(expense);
		}
	});
};
namespace("Crm.PerDiem.ViewModels").ExpenseEditModalViewModel.prototype.dispose = function() {
	var viewModel = this;
	var fileResource = Helper.Database.getDatabaseEntity(viewModel.fileResource);
	var expense = Helper.Database.getDatabaseEntity(viewModel.expense);
	window.database.detach(fileResource);
	window.database.detach(expense);
};
namespace("Crm.PerDiem.ViewModels").ExpenseEditModalViewModel.prototype.save = function() {
	var viewModel = this;
	viewModel.loading(true);

	var fileResource = Helper.Database.getDatabaseEntity(viewModel.fileResource);
	if (fileResource && fileResource.ContentType && !fileResource.ContentType.startsWith('image/') && fileResource.ContentType != 'application/pdf') {
		viewModel.loading(false);
		window.Helper.Confirm.genericConfirm({
			text:  window.Helper.String.getTranslatedString("M_SelectedFileNotImage"),
			title: fileResource.Filename,
			type: "warning",
			showCancelButton: false
		});
		return;
	}

	var errors = window.ko.validation.group([viewModel.expense, !fileResource.Content ? null : viewModel.fileResource], { deep: true });
	if (errors().length > 0) {
		viewModel.loading(false);
		errors.showAllMessages();
		return;
	}

	if (fileResource.entityState === $data.EntityState.Added) {
		if (fileResource.Content) {
			viewModel.expense().FileResourceKey(fileResource.Id);
		} else {
			window.database.detach(fileResource);
		}
	} else if (fileResource.entityState === $data.EntityState.Modified) {
		if (fileResource.Content) {
			var newFileResource = window.database.Main_FileResource.Main_FileResource.create();
			window.database.add(newFileResource);
			newFileResource.Content = fileResource.Content;
			newFileResource.Length = fileResource.Length;
			newFileResource.ContentType = fileResource.ContentType;
			newFileResource.Filename = fileResource.Filename;
			viewModel.expense().FileResourceKey(newFileResource.Id);
			window.database.remove(fileResource);

			var file = window.database.stateManager.trackedEntities[2];
			window.database.stateManager.trackedEntities[2] = window.database.stateManager.trackedEntities[1];
			window.database.stateManager.trackedEntities[1] = file;
		} else {
			viewModel.expense().FileResourceKey(null);
			window.database.remove(fileResource);
		}
	}

	window.database.saveChanges().then(function() {
		viewModel.loading(false);
		$(".modal:visible").modal("hide");
	}).fail(function() {
		viewModel.loading(false);
		window.swal(window.Helper.String.getTranslatedString("UnknownError"),
			window.Helper.String.getTranslatedString("Error_InternalServerError"),
			"error");
	});
};

namespace("Crm.PerDiem.ViewModels").ExpenseEditModalViewModel.prototype.onSelectExpenseType = function (expenseType) {
	var viewModel = this;
	viewModel.selectedExpenseType(expenseType !== null ? expenseType.asKoObservable() : null);
	if(expenseType && expenseType.Key !== viewModel.expense().ExpenseTypeKey())
		viewModel.expense().CostCenterKey(null);
}