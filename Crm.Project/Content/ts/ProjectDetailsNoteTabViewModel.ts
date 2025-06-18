///<reference path="../@types/index.d.ts" />
import {namespace} from "../../../../Content/ts/namespace";

export class ProjectDetailsNotesTabViewModel extends window.Main.ViewModels.ContactDetailsNotesTabViewModel {
	constructor(parentViewModel: any) {
		super();
		this.contactId(parentViewModel.project().Id());
		this.contactType("Project");
		this.minDate(parentViewModel.project().CreateDate());
		this.plugin("Crm.Project");
	}
}

namespace("Crm.Project.ViewModels").ProjectDetailsNotesTabViewModel = ProjectDetailsNotesTabViewModel;