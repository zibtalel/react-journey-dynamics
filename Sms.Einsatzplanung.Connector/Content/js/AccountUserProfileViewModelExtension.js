;
(function(ko, Helper, $data) {
	var accountUserProfileViewModel = window.Main.ViewModels.AccountUserProfileViewModel;
	window.Main.ViewModels.AccountUserProfileViewModel = function() {
		var viewModel = this;
		accountUserProfileViewModel.apply(viewModel, arguments);
		viewModel.homeAddress = ko.observable(null);
		viewModel.isAddressEmpty = ko.pureComputed(function () {
			return !viewModel.homeAddress().CountryKey() &&
				!viewModel.homeAddress().City() &&
				!viewModel.homeAddress().Street() &&
				!viewModel.homeAddress().ZipCode();
		});
		viewModel.lookups.addressTypes = {$tableName: "Main_AddressType"};
		viewModel.lookups.countries = {$tableName: "Main_Country"};
		viewModel.lookups.regions = {$tableName: "Main_Region"};
	};
	window.Main.ViewModels.AccountUserProfileViewModel.prototype = accountUserProfileViewModel.prototype;
	var init = window.Main.ViewModels.AccountUserProfileViewModel.prototype.init;
	window.Main.ViewModels.AccountUserProfileViewModel.prototype.init = function() {
		var viewModel = this;
		return init.apply(viewModel, arguments).then(function() {
			if (viewModel.user().ExtensionValues().HomeAddressId()) {
				return window.database.Main_Address.find(viewModel.user().ExtensionValues().HomeAddressId());
			} else {
				return null;
			}
		}).then(function(address) {
			viewModel.initAddress(address);
		});
	};
	window.Main.ViewModels.AccountUserProfileViewModel.prototype.initAddress = function(address) {
		var viewModel = this;
		if (!address) {
			address = window.database.Main_Address.Main_Address.create();
			address.AddressTypeKey = "none";
			if (viewModel.user().DomainId) {
				address.DomainId = viewModel.user().DomainId();
			}
		}
		viewModel.homeAddress(address.asKoObservable());
		viewModel.homeAddress().City.extend({
			required: {
				message: Helper.String.getTranslatedString("RuleViolation.Required")
					.replace("{0}", Helper.String.getTranslatedString("City")),
				onlyIf: function() {
					return !viewModel.isAddressEmpty();
				},
				params: true
			}
		});
		viewModel.homeAddress().CountryKey.extend({
			required: {
				message: window.Helper.String.getTranslatedString("RuleViolation.Required")
					.replace("{0}", window.Helper.String.getTranslatedString("Country")),
				onlyIf: function() {
					return !viewModel.isAddressEmpty();
				},
				params: true
			}
		});
		viewModel.homeAddress().ZipCode.extend({
			required: {
				message: window.Helper.String.getTranslatedString("RuleViolation.Required")
					.replace("{0}", window.Helper.String.getTranslatedString("ZipCode")),
				onlyIf: function() {
					return !viewModel.isAddressEmpty();
				},
				params: true
			}
		});
	};
	window.Main.ViewModels.AccountUserProfileViewModel.prototype.saveHomeAddress = function() {
		var viewModel = this;
		viewModel.loading(true);
		window.database.attachOrGet(viewModel.user().innerInstance);
		if (viewModel.isAddressEmpty()) {
			viewModel.user().ExtensionValues().HomeAddressId(null);
			if (viewModel.homeAddress().Id() !== Helper.String.emptyGuid()) {
				window.database.remove(viewModel.homeAddress().innerInstance);
				window.Helper.Database.registerDependency(viewModel.homeAddress(), viewModel.user());
				viewModel.initAddress(null);
			}
		} else {
			viewModel.homeAddress().Id() === Helper.String.emptyGuid()
				? window.database.add(viewModel.homeAddress().innerInstance)
				: window.database.attachOrGet(viewModel.homeAddress().innerInstance);
			window.Helper.Database.registerDependency(viewModel.user(), viewModel.homeAddress());
			viewModel.homeAddress().ModifyDate(new Date());
			viewModel.homeAddress().Name1(viewModel.user().FirstName());
			viewModel.homeAddress().Name2(viewModel.user().LastName());
			viewModel.user().ExtensionValues().HomeAddressId(viewModel.homeAddress().Id());
		}
		return window.database.saveChanges().then(function(){
			viewModel.loading(false);
		});
	};
})(window.ko, window.Helper, window.$data);;