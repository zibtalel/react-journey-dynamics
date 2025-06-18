namespace("Crm.PerDiem.ViewModels").TimeEntryEditModalViewModel = function() {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	viewModel.timeEntry = window.ko.observable(null);
	viewModel.allDay = window.ko.observable(false);
	viewModel.minDate = window.ko.observable(window.Crm.PerDiem.Settings.TimeEntry.MaxDaysAgo ? window.moment().startOf("day").utc().add(-parseInt(window.Crm.PerDiem.Settings.TimeEntry.MaxDaysAgo), "days") : false);
	viewModel.maxDate = window.ko.pureComputed(function() {
		return new Date();
	});
	viewModel.lookups = {
		costCenters: { $tableName: "Main_CostCenter" },
		timeEntryTypes: { $tableName: "CrmPerDiem_TimeEntryType" }
	};
	viewModel.errors = window.ko.validation.group(viewModel.timeEntry, { deep: true });
	viewModel.selectedTimeEntryType = window.ko.observable(null);
	viewModel.selectedResponsibleUser = window.ko.observable(null);
	viewModel.validCostCenters = window.ko.computed(function() {
		return viewModel.selectedTimeEntryType() ? viewModel.selectedTimeEntryType().ValidCostCenters() : [];
	});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryEditModalViewModel.prototype.init = function(id, params) {
	var viewModel = this;
	return window.Helper.Lookup.getLocalizedArrayMaps(viewModel.lookups).then(function() {
			if (id) {
				return window.database.CrmPerDiem_UserTimeEntry.find(id)
					.then(window.database.attachOrGet.bind(window.database));
			}
			var newTimeEntry = window.database.CrmPerDiem_UserTimeEntry.CrmPerDiem_UserTimeEntry.create();
			newTimeEntry.CostCenterKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.costCenters, newTimeEntry.CostCenterKey);
			newTimeEntry.Date = window.moment(params.selectedDate).toDate();
			newTimeEntry.ResponsibleUser = params.username;
			newTimeEntry.TimeEntryTypeKey = window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.timeEntryTypes, newTimeEntry.TimeEntryTypeKey);
			return window.Helper.TimeEntry.getLatestTimeEntryToOrDefault(newTimeEntry.ResponsibleUser, newTimeEntry.Date).then(function(latestTo) {
				newTimeEntry.From = latestTo;
				window.database.add(newTimeEntry);
				return newTimeEntry;
			});
		}).then(function(timeEntry) {
			viewModel.timeEntry(timeEntry.asKoObservable());
		}).then(function() {
			var from, to;
			viewModel.allDay.subscribe(async function(allDay) {
				if (allDay) {
					from = viewModel.timeEntry().From();
					to = viewModel.timeEntry().To();
					if (!viewModel.selectedResponsibleUser()) {
						viewModel.selectedResponsibleUser(await window.database.Main_User.find(viewModel.timeEntry().ResponsibleUser()));
					}
					const workingHoursPerDay = viewModel.selectedResponsibleUser().ExtensionValues.WorkingHoursPerDay > 0 ? viewModel.selectedResponsibleUser().ExtensionValues.WorkingHoursPerDay : parseInt(window.Crm.PerDiem.Settings.TimeEntry.DefaultWorkingHoursPerDay);
					viewModel.timeEntry().From(window.moment(window.Crm.PerDiem.Settings.TimeEntry.DefaultStart, 'HH:mm').toDate());
					viewModel.timeEntry().To(window.moment(viewModel.timeEntry().From()).add(workingHoursPerDay, "hour").toDate());
				} else {
					viewModel.timeEntry().From(from);
					viewModel.timeEntry().To(to);
				}
			});
			viewModel.timeEntry().To.subscribe(function() {
				viewModel.timeEntry().From.valueHasMutated();
			});
			viewModel.timeEntry().Date.subscribe(function() {
				viewModel.timeEntry().From.valueHasMutated();
				viewModel.timeEntry().From(Helper.Date.setTimeToDate(viewModel.timeEntry().Date(), viewModel.timeEntry().From()));
				viewModel.timeEntry().To(Helper.Date.setTimeToDate(viewModel.timeEntry().Date(), viewModel.timeEntry().To()));
			});
			viewModel.timeEntry().ResponsibleUser.subscribe(function() {
				viewModel.timeEntry().From.valueHasMutated();
			});
			viewModel.timeEntry().From.subscribe(Helper.TimeEntry.updateToAndDuration.bind(viewModel.timeEntry()));
			viewModel.timeEntry().To.subscribe(Helper.TimeEntry.updateFromAndDuration.bind(viewModel.timeEntry()));
			viewModel.timeEntry().Duration.subscribe(Helper.TimeEntry.initFromAndTo.bind(viewModel.timeEntry()));
		});
};
namespace("Crm.PerDiem.ViewModels").TimeEntryEditModalViewModel.prototype.dispose = function() {
	var viewModel = this;
	window.database.detach(viewModel.timeEntry().innerInstance);
};
namespace("Crm.PerDiem.ViewModels").TimeEntryEditModalViewModel.prototype.save = function() {
	var viewModel = this;
	viewModel.loading(true);

	if (viewModel.errors().length > 0) {
		viewModel.loading(false);
		viewModel.errors.showAllMessages();
		return;
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

namespace("Crm.PerDiem.ViewModels").TimeEntryEditModalViewModel.prototype.onSelectTimeEntryType = function (timeEntryType) {
	var viewModel = this;
	viewModel.selectedTimeEntryType(timeEntryType !== null ? timeEntryType.asKoObservable() : null);
	viewModel.timeEntry().CostCenterKey(null);
}
namespace("Crm.PerDiem.ViewModels").TimeEntryEditModalViewModel.prototype.onResponsibleUserSelected = function (responsibleUser) {
	const viewModel = this;
	viewModel.selectedResponsibleUser(responsibleUser);
}
