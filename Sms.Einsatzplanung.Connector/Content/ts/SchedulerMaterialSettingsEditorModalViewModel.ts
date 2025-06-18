///<reference path="../../../../Content/@types/index.d.ts" />
import {namespace} from "../../../../Content/ts/namespace";

export class SchedulerMaterialSettingsEditorModalViewModel extends window.Main.ViewModels.ViewModelBase {
	config = ko.observable(null);
	icon = ko.observable(null);
	type = ko.observable(null);
	types = ko.observableArray([]);

	async init() {
		const viewModel = this;
		viewModel.config({
			Content: ko.observable(null),
			ContentType: ko.observable(null),
			Filename: ko.observable(null),
			Length: ko.observable(0),
		});
		viewModel.icon({
			Content: ko.observable(null),
			ContentType: ko.observable(null),
			Filename: ko.observable(null),
			Length: ko.observable(0),
		});
		viewModel.types([
			{Text: window.Helper.String.getTranslatedString("PleaseSelect"), Value: null},
			{Text: "Debug", Value: "Debug"},
			{Text: "Release", Value: "Release"}
		]);
		return null;
	}

	async save() {
		const viewModel = this;
		viewModel.loading(true);

		if (viewModel.icon().Content()) {
			const icon = window.database.SmsEinsatzplanungConnector_SchedulerIcon.defaultType.create({
				Icon: viewModel.icon().Content()
			});
			window.database.add(icon);
		}

		if (viewModel.config().Content()) {
			const config = window.database.SmsEinsatzplanungConnector_SchedulerConfig.defaultType.create({
				Config: viewModel.config().Content(),
				Type: viewModel.type()
			});
			window.database.add(config);
		}

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
		viewModel.loading(false);
		$(".modal:visible").modal("hide");
	}
}

namespace("Sms.Einsatzplanung.Connector.ViewModels").SchedulerMaterialSettingsEditorModalViewModel = SchedulerMaterialSettingsEditorModalViewModel;