///<reference path="../@types/index.d.ts" />
import {namespace} from "../../../../Content/ts/namespace";

export class PotentialDetailsNotesTabViewModel extends window.Main.ViewModels.ContactDetailsNotesTabViewModel {
	constructor(parentViewModel: any) {
		super();
		this.contactId(parentViewModel.potential().Id());
		this.contactType("Potential");
		this.minDate(parentViewModel.potential().CreateDate());
		this.plugin("Crm.Project");
	}
}

namespace("Crm.Project.ViewModels").PotentialDetailsNotesTabViewModel = PotentialDetailsNotesTabViewModel;