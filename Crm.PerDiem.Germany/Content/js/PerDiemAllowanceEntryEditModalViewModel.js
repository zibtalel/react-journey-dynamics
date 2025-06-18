namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel = function () {
	var viewModel = this;
	viewModel.loading = window.ko.observable(true);
	viewModel.lookups = {
		adjustments: {},
		costCenters: {},
		currencies: {}
	};
	viewModel.perDiemAllowanceAdjustmentList = window.ko.observableArray([]);
	viewModel.perDiemAllowanceEntry = window.ko.observable(null);
	viewModel.perDiemAllowances = window.ko.observableArray([]);
	viewModel.setOfflineEnum();
	viewModel.adjustmentFromEnum = Crm.PerDiem.Germany.Model.Enums.AdjustmentFrom;

	viewModel.selectedAdjustments = ko.observableArray()
	viewModel.minDate = window.ko.observable(window.Crm.PerDiem.Settings.Expense.MaxDaysAgo
		? window.moment().startOf("day").utc()
			.add(-parseInt(window.Crm.PerDiem.Settings.Expense.MaxDaysAgo), "days")
		: false);

	viewModel.maxDate = window.ko.pureComputed(function () {
		return new Date();
	});

	viewModel.perDiemAllowance = window.ko.pureComputed(function (value) {
		if (!viewModel.perDiemAllowanceEntry() || !viewModel.perDiemAllowanceEntry().PerDiemAllowanceKey()) {
			return null;
		}
		return viewModel.perDiemAllowances().find(function (x) {
			return x.Key === viewModel.perDiemAllowanceEntry().PerDiemAllowanceKey();
		}) ||
			null;
	});

	viewModel.perDiemAllowance.subscribe(function (newValue) {
		var keepSelected = [];
		viewModel.perDiemAllowanceEntry().AdjustmentReferences().forEach(function (reference) {
			keepSelected.push(reference.PerDiemAllowanceAdjustmentKey())
		})
		viewModel.filterAdjustments(keepSelected);
	})

	viewModel.calculatedAmount = window.ko.pureComputed(function () {
		if (!viewModel.perDiemAllowance()) {
			return null;
		}
		var amount = viewModel.perDiemAllowanceEntry().AllDay()
			? viewModel.perDiemAllowance().AllDayAmount
			: viewModel.perDiemAllowance().PartialDayAmount;

		viewModel.perDiemAllowanceEntry().AdjustmentReferences().forEach(function (reference) {
			if (reference.AdjustmentValue() < 0 && reference.IsPercentage()) {
				amount += viewModel.perDiemAllowance().AllDayAmount * reference.AdjustmentValue();
			}
			if (reference.AdjustmentValue() < 0 && !reference.IsPercentage()) {
				amount += reference.AdjustmentValue();
			}
			if (reference.AdjustmentValue() > 0 && reference.IsPercentage()) {
				amount += viewModel.perDiemAllowance().AllDayAmount * reference.AdjustmentValue();
			}
			if (reference.AdjustmentValue() > 0 && !reference.IsPercentage()) {
				amount += reference.AdjustmentValue();
			}
		})
		return +Math.max(amount, 0).toFixed(2);
	})

	viewModel.errors = window.ko.validation.group([viewModel.perDiemAllowanceEntry, viewModel.calculatedAmount], { deep: true });
};

namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.init = function (id, params) {
	var viewModel = this;
	return new $.Deferred().resolve().promise()
		.then(function () {
			return window.Helper.Lookup.getLocalizedArrayMap("Main_CostCenter").then(function (lookups) {
				viewModel.lookups.costCenters = lookups;
			});
		})
		.then(function () {
			return window.Helper.Lookup.getLocalizedArrayMap("Main_Currency").then(function (lookups) {
				viewModel.lookups.currencies = lookups;
			});
		})
		.then(function () {
			if (!id && params.selectedDate) {
				return window.database.CrmPerDiemGermany_PerDiemAllowanceEntry
					.include("AdjustmentReferences")
					.filter(function (it) {
						return it.ResponsibleUser === this.user && it.Date === this.previousDay;
					},
						{
							user: window.Helper.User.getCurrentUserName(),
							previousDay: window.moment(params.selectedDate).add(-1, "day").toDate()
						}).take(1).toArray().then(function (results) {
							return results.length === 1 ? results[0] : null;
						});
			}
			return $.Deferred().resolve(null).promise();
		})
		.then(function (previousPerDiemAllowanceEntry) {
			if (id) {
				return window.database.CrmPerDiemGermany_PerDiemAllowanceEntry
					.include("AdjustmentReferences")
					.find(id);
			}
			var newPerDiemAllowanceEntry = window.database.CrmPerDiemGermany_PerDiemAllowanceEntry
				.CrmPerDiemGermany_PerDiemAllowanceEntry.create();
			newPerDiemAllowanceEntry.Amount = null;
			newPerDiemAllowanceEntry.CostCenterKey =
				window.Helper.Lookup.getDefaultLookupValueSingleSelect(viewModel.lookups.costCenters,
					newPerDiemAllowanceEntry.CostCenterKey);
			newPerDiemAllowanceEntry.Date = window.moment(params.selectedDate).toDate();
			newPerDiemAllowanceEntry.ResponsibleUser = params.username;
			if (previousPerDiemAllowanceEntry) {
				newPerDiemAllowanceEntry.AllDay = true;
				newPerDiemAllowanceEntry.CostCenterKey = previousPerDiemAllowanceEntry.CostCenterKey;
				newPerDiemAllowanceEntry.PerDiemAllowanceKey = previousPerDiemAllowanceEntry.PerDiemAllowanceKey;
			}
			return newPerDiemAllowanceEntry;
		})
		.then(function (perDiemAllowanceEntry) {
			viewModel.perDiemAllowanceEntry(perDiemAllowanceEntry.asKoObservable());
			return viewModel.loadPerDiemAdjustments();
		})
		.then(function () {
			return Promise.all(
				viewModel.perDiemAllowanceEntry().AdjustmentReferences().map(function (reference) {
					return viewModel.loadSelectedAdjustments(reference);
				})
			)
		})
		.then(function () {
			return viewModel.loadPerDiemAllowances();
		})
		.then(function () {
			viewModel.calculatedAmount.subscribe(function (amount) {
				viewModel.perDiemAllowanceEntry().Amount(amount);
				viewModel.perDiemAllowanceEntry()
					.CurrencyKey(amount === null ? null : viewModel.perDiemAllowance().CurrencyKey);
			});
			viewModel.calculatedAmount.notifySubscribers(viewModel.calculatedAmount());
			viewModel.perDiemAllowanceEntry().Date.subscribe(function () {
				if (viewModel.loading() == false) {
					viewModel.loading(true);
					viewModel.loadPerDiemAllowances()
						.then(function () {
							return viewModel.loadPerDiemAdjustments();
						})
						.then(function () {
							return Promise.all(
								viewModel.perDiemAllowanceEntry().AdjustmentReferences().map(function (reference) {
									return viewModel.loadSelectedAdjustments(reference);
								})
							)
						})
						.then(function () {
							var keepSelected = [];
							viewModel.perDiemAllowanceEntry().AdjustmentReferences().forEach(function (reference) {
								keepSelected.push(reference.PerDiemAllowanceAdjustmentKey())
							})
							viewModel.filterAdjustments(keepSelected);
							viewModel.loading(false);
						});
				}
			});
			viewModel.perDiemAllowanceEntry().AllDay.subscribe(function (newValue) {
				var removeItems = [];
				var keepSelected = [];
				viewModel.perDiemAllowanceEntry().AdjustmentReferences().forEach(function (entity) {
					if (!(((entity.AdjustmentFrom() == "AllDay" || entity.AdjustmentFrom() == "Always") && newValue) || (!newValue && (entity.AdjustmentFrom() == "Partial" || entity.AdjustmentFrom() == "Always")))) {
						removeItems.push(entity)
					} else {
						keepSelected.push(entity.PerDiemAllowanceAdjustmentKey())
					}
				})
				for (var i = 0; i < removeItems.length; i++) {
					var valueToRemove = ko.utils.arrayFirst(viewModel.perDiemAllowanceEntry().AdjustmentReferences(), function (reference) {
						return reference.Id == removeItems[i].Id;
					});
					viewModel.perDiemAllowanceEntry().AdjustmentReferences.remove(valueToRemove);
					window.database.remove(valueToRemove)
				}
				removeItems = [];
				viewModel.filterAdjustments(keepSelected);
			});

			viewModel.perDiemAllowances.subscribe(function (perDiemAllowances) {
				var lookupMap = window.Helper.Lookup.mapLookups(perDiemAllowances);
				if (viewModel.perDiemAllowanceEntry().PerDiemAllowanceKey() === null) {
					viewModel.perDiemAllowanceEntry()
						.PerDiemAllowanceKey(window.Helper.Lookup.getDefaultLookupValueSingleSelect(lookupMap));
				} else if (!lookupMap[viewModel.perDiemAllowanceEntry().PerDiemAllowanceKey()]) {
					viewModel.perDiemAllowanceEntry().PerDiemAllowanceKey(null);
				}
			});
			viewModel.selectedAdjustments.subscribe(function (newValueArray) {
				newValueArray.forEach(function (newValue) {
					var adjustment = newValue.value;
					if (newValue.status == 'added' && $(".modal").is(':visible')) {
						var allowanceEntryAllowanceAdjustmentRef = window.database.CrmPerDiemGermany_PerDiemAllowanceEntryAllowanceAdjustmentReference.CrmPerDiemGermany_PerDiemAllowanceEntryAllowanceAdjustmentReference.create();
						allowanceEntryAllowanceAdjustmentRef.PerDiemAllowanceEntryKey = viewModel.perDiemAllowanceEntry().Id();
						allowanceEntryAllowanceAdjustmentRef.IsPercentage = adjustment.IsPercentage();
						allowanceEntryAllowanceAdjustmentRef.AdjustmentValue = adjustment.AdjustmentValue();
						allowanceEntryAllowanceAdjustmentRef.AdjustmentFrom = viewModel.convertEnumToString(adjustment.AdjustmentFrom());
						allowanceEntryAllowanceAdjustmentRef.PerDiemAllowanceAdjustmentKey = adjustment.Key();
						viewModel.perDiemAllowanceEntry().AdjustmentReferences.push(allowanceEntryAllowanceAdjustmentRef)
						window.database.add(allowanceEntryAllowanceAdjustmentRef);
					} else if (newValue.status === 'deleted' && $(".modal").is(':visible')) {
						var valueToRemove = ko.utils.arrayFirst(viewModel.perDiemAllowanceEntry().AdjustmentReferences(), function (reference) {
							return reference.PerDiemAllowanceAdjustmentKey() == adjustment.Key();
						});
						if (valueToRemove) {
							viewModel.perDiemAllowanceEntry().AdjustmentReferences.remove(valueToRemove);
							window.database.remove(valueToRemove)
						}
					}
				})
			}, null, "arrayChange");

			if (viewModel.perDiemAllowanceEntry().PerDiemAllowanceKey() === null) {
				viewModel.perDiemAllowanceEntry().PerDiemAllowanceKey(window.Helper.Lookup.getDefaultLookupValueSingleSelect(window.Helper.Lookup.mapLookups(viewModel.perDiemAllowances()), viewModel.perDiemAllowanceEntry().PerDiemAllowanceKey()));
			}
			viewModel.perDiemAllowanceEntry().Id() === window.Helper.String.emptyGuid()
				? window.database.add(viewModel.perDiemAllowanceEntry().innerInstance)
				: window.database.attachOrGet(viewModel.perDiemAllowanceEntry().innerInstance);
		});
};

namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.dispose = function () {
	var viewModel = this;
	window.database.detach(viewModel.perDiemAllowanceEntry().innerInstance);
};

namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.save = function () {
	var viewModel = this;
	viewModel.loading(true);

	if (viewModel.errors().length > 0) {
		viewModel.loading(false);
		viewModel.errors.showAllMessages();
		return;
	}
	return window.database.saveChanges()
		.then(function () {
			$(".modal:visible").modal("hide");
			viewModel.loading(false);
		})
		.fail(function () {
			viewModel.loading(false);
			window.swal(window.Helper.String.getTranslatedString("UnknownError"),
				window.Helper.String.getTranslatedString("Error_InternalServerError"),
				"error");
		});

};

namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.loadPerDiemAllowances = function () {
	var viewModel = this;
	var date = viewModel.perDiemAllowanceEntry().Date();
	if (!date) {
		viewModel.perDiemAllowances([]);
		return new $.Deferred().resolve().promise();
	}
	return window.Helper.Lookup
		.getLocalizedArrayMap("CrmPerDiemGermany_PerDiemAllowance",
			null,
			function (it) { return it.ValidFrom <= this.date && this.date <= it.ValidTo; },
			{ date: date }).then(function (lookups) { viewModel.perDiemAllowances(lookups.$array); });
};

namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.loadPerDiemAdjustments = function () {
	var viewModel = this;
	return window.Helper.Lookup.getLocalizedArrayMap("CrmPerDiemGermany_PerDiemAllowanceAdjustment", null,
		function (it) { return it.ValidFrom <= this.date && this.date <= it.ValidTo; },
		{ date: viewModel.perDiemAllowanceEntry().Date() })
		.then(function (adjustments) {
			viewModel.lookups.adjustments = adjustments;
		})
};

namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.loadSelectedAdjustments = function (reference) {
	var viewModel = this;
	var deferred = new $.Deferred();
	window.Helper.Lookup.getLocalized("CrmPerDiemGermany_PerDiemAllowanceAdjustment", null, function (it) { return it.Key == value && !(it.ValidFrom <= date && date <= it.ValidTo) }, { value: reference.PerDiemAllowanceAdjustmentKey(), date: viewModel.perDiemAllowanceEntry().Date()  })
		.then(function (adjustment) {
			if (adjustment.length != 0) {
				viewModel.lookups.adjustments.$array.push(adjustment[0]);
			}
			deferred.resolve();
		})
	return deferred.promise();
}

namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.filterAdjustments = function (selected) {
	var viewModel = this;

	if (!(viewModel.perDiemAllowance() == null || viewModel.perDiemAllowanceEntry().AllDay() == undefined)) {
		viewModel.perDiemAllowanceAdjustmentList.removeAll();
		viewModel.selectedAdjustments.removeAll();
		viewModel.lookups.adjustments.$array
			.filter(function (adjustment) {
				var enumValue = adjustment.AdjustmentFrom;
				return (adjustment.CountryKey == viewModel.perDiemAllowance().Key || adjustment.CountryKey == null || adjustment.CostCenterKey == '') && ((viewModel.perDiemAllowanceEntry().AllDay() && (enumValue == viewModel.adjustmentFromEnum.AllDay || enumValue == viewModel.adjustmentFromEnum.Always)) || (!viewModel.perDiemAllowanceEntry().AllDay() && (enumValue == viewModel.adjustmentFromEnum.Partial || enumValue == viewModel.adjustmentFromEnum.Always)));
			})
			.forEach(function (adjustment) {
				if (adjustment.Key != null) {
					var valueAlreadyExists = ko.utils.arrayFirst(viewModel.perDiemAllowanceEntry().AdjustmentReferences(), function (reference) {
						return reference.PerDiemAllowanceAdjustmentKey() == adjustment.Key;
					});
					if (!valueAlreadyExists) {
						const selectedItem = selected.find(element => element == adjustment.Key);
						if (adjustment.ValidFrom <= new Date() && new Date() <= adjustment.ValidTo && !selectedItem) {
							viewModel.perDiemAllowanceAdjustmentList.push(adjustment.asKoObservable())
						} else if (selectedItem) {
							var selectedAdjustment = adjustment.asKoObservable();
							viewModel.perDiemAllowanceAdjustmentList.push(selectedAdjustment);
							viewModel.selectedAdjustments.push(selectedAdjustment);
						} else {
							viewModel.perDiemAllowanceAdjustmentList.push(adjustment.asKoObservable())
						}
					} else {
						var selectedAdjustment = adjustment.asKoObservable();
						viewModel.perDiemAllowanceAdjustmentList.push(selectedAdjustment);
						viewModel.selectedAdjustments.push(selectedAdjustment);
					}
				}
			})
	}
	if (viewModel.perDiemAllowance() == null) {
		viewModel.perDiemAllowanceAdjustmentList.removeAll();
	}
};


namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.isDeduction = function (data) {
	var viewModel = this;
	var exisitingValue = ko.utils.arrayFirst(viewModel.perDiemAllowanceEntry().AdjustmentReferences(), function (reference) {
		return reference.PerDiemAllowanceAdjustmentKey() == data.Key();
	});
	if (exisitingValue) {
		return exisitingValue.AdjustmentValue() < 0;
	} else {
		return data.AdjustmentValue() < 0;
	}
};

namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.setOfflineEnum = function () {
	var viewModel = this;
	if (!window.Crm) {
		window.Crm = {};
	}
	if (!window.Crm.PerDiem) {
		window.Crm.PerDiem = {};
	}
	if (!window.Crm.PerDiem.Germany) {
		window.Crm.PerDiem.Germany = {};
	}
	if (!window.Crm.PerDiem.Germany.Model) {
		window.Crm.PerDiem.Germany.Model = {};
	}
	if (!window.Crm.PerDiem.Germany.Model.Enums) {
		window.Crm.PerDiem.Germany.Model.Enums = {};
	}
	if (!window.Crm.PerDiem.Germany.Model.Enums.AdjustmentFrom) {
		window.Crm.PerDiem.Germany.Model.Enums.AdjustmentFrom = {
			AllDay: 0,
			Partial: 1,
			Always: 2
		};
	}
};

namespace("Crm.PerDiem.Germany.ViewModels").PerDiemAllowanceEditModalViewModel.prototype.convertEnumToString = function (enumValue) {
	if (enumValue == 0) { return "AllDay" }
	if (enumValue == 1) { return "Partial" }
	if (enumValue == 2) { return "Always" }

};