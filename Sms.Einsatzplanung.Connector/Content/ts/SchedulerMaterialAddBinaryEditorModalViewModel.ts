///<reference path="../../../../Content/@types/index.d.ts" />
import {namespace} from "../../../../Content/ts/namespace";

export class SchedulerMaterialAddBinaryEditorModalViewModel extends window.Main.ViewModels.ViewModelBase {
	schedulerBinary = ko.observable<Sms.Einsatzplanung.Connector.Rest.Model.ObservableSmsEinsatzplanungConnector_SchedulerBinary>(null);

	dispose() {
		window.database.detach(this.schedulerBinary().innerInstance);
	}

	init() {
		const schedulerBinary = window.database.SmsEinsatzplanungConnector_SchedulerBinary.defaultType.create();
		window.database.add(schedulerBinary);
		this.schedulerBinary(schedulerBinary.asKoObservable());
		this.schedulerBinary().Length.extend({
			validation: {
				validator: function (val) {
					return val && val > 0;
				},
				message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("File"))
			}
		});
	};

	async save() {
		const errors = window.ko.validation.group([this.schedulerBinary], {deep: true});
		if (errors().length > 0) {
			errors.showAllMessages();
			return;
		}

		this.loading(true);
		try {
			await window.database.saveChanges();
		}
		catch (e) {
			this.loading(false);
			window.swal({
				title: window.Helper.String.getTranslatedString("Error"),
				text: e as string,
				type: "error"
			});
			return;
		}
		$(".modal:visible").modal("hide");
	};
}

namespace("Sms.Einsatzplanung.Connector.ViewModels").SchedulerMaterialAddBinaryEditorModalViewModel = SchedulerMaterialAddBinaryEditorModalViewModel;
