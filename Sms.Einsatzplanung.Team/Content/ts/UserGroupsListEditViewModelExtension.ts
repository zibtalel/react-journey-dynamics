import type {Select2AutoCompleter} from "../../../../Content/@types";
import {namespace} from "../../../../Content/ts/namespace";

export class UserGroupListEditViewModelExtension extends window.Main.ViewModels.UserGroupListEditViewModel {

	MainResourceId = ko.observable<string>(null)

	mainResourceSelect2autocompleter: () => Select2AutoCompleter = () => {
		return {
			data: this.MainResourceId,
			autocompleteOptions: {
				mapDisplayObject: window.Helper.User.mapForSelect2Display,
				table: "Main_User",
				customFilter: (query, term = "") => {
					return query.filter("it.Id in this.ids && (it.LastName.toLowerCase().contains(this.term) || it.FirstName.toLowerCase().contains(this.term))", {
						ids: this.Members(),
						term
					});
				}
			}
		};
	}

	async init(id: string): Promise<void> {
		await super.init(id);
		this.Members.subscribe(() => {
			if (!this.Members().includes(this.MainResourceId())) {
				this.MainResourceId(null);
			}
		});
		const resourceId = ko.unwrap(this.userGroup().ExtensionValues as any).MainResourceId();
		if (this.Members().includes(resourceId)) {
			this.MainResourceId(resourceId);
		}
	}

	async updateEntity(): Promise<void> {
		this.userGroup().ExtensionValues().MainResourceId(this.MainResourceId());
		await window.database.saveChanges();
		await super.updateEntity();
	}
}

namespace("Main.ViewModels").UserGroupListEditViewModel = UserGroupListEditViewModelExtension;
