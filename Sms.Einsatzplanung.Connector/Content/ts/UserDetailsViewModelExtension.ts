///<reference path="../../../../../Crm.Web/Content/@types/index.d.ts"/>
import { namespace } from "../../../../Content/ts/namespace";

export default class UserDetailsViewModelExtension extends window.Main.ViewModels.UserDetailsViewModel {
	homeAddress = ko.observable(null);
	isAddressEmpty = ko.pureComputed(() => {
		return !this.homeAddress().CountryKey() &&
			!this.homeAddress().City() &&
			!this.homeAddress().Street() &&
			!this.homeAddress().ZipCode();
	})

	constructor() {
		super();
		this.lookups.addressTypes = { $tableName: "Main_AddressType" };
		this.lookups.countries = { $tableName: "Main_Country" };
		this.lookups.regions = { $tableName: "Main_Region" };
	}

	async init(id) {
		await super.init(id);
		const addressId = this.user().ExtensionValues().HomeAddressId();
		let address: Crm.Rest.Model.Main_Address = null;
		if (addressId) {
			address = await window.database.Main_Address.find(addressId);
		}
		this.initAddress(address);
	}

	// @ts-ignore
	initAddress = window.Main.ViewModels.AccountUserProfileViewModel.prototype.initAddress;
	// @ts-ignore
	saveHomeAddress = window.Main.ViewModels.AccountUserProfileViewModel.prototype.saveHomeAddress;
}
namespace("Main.ViewModels").UserDetailsViewModel = UserDetailsViewModelExtension;
