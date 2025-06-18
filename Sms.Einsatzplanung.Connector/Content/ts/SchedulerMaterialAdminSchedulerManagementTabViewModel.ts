///<reference path="../../../../Content/@types/index.d.ts" />
import {namespace} from "../../../../Content/ts/namespace";

export class SchedulerMaterialAdminSchedulerManagementTabViewModel extends window.Main.ViewModels.GenericListViewModel {
	releasedScheduler = ko.observable<Sms.Einsatzplanung.Connector.Rest.Model.ObservableSmsEinsatzplanungConnector_Scheduler>(null);

	constructor() {
		super("SmsEinsatzplanungConnector_Scheduler",
			["ClickOnceVersion", "ModifyDate", "VersionString"],
			["DESC", "DESC", "DESC"],
			["Icon"]);
		this.infiniteScroll(true);
	}

	async init() {
		const releasedScheduler = await window.database.SmsEinsatzplanungConnector_Scheduler
			.include("Config")
			.include("Icon")
			.filter("it.IsReleased === true")
			.take(1)
			.toArray();
		if (releasedScheduler.length > 0) {
			this.releasedScheduler(releasedScheduler[0].asKoObservable());
		}
		await super.init();
	};

	async deletePackage(pack) {
		const viewModel = this;
		viewModel.loading(true);
		const entity = window.Helper.Database.getDatabaseEntity(pack);
		window.database.remove(entity);
		return window.database.saveChanges().then(function () {
			viewModel.loading(false);
		});
	};

	async releasePackage(schedulerToRelease) {
		const viewModel = this;
		viewModel.loading(true);
		if (viewModel.releasedScheduler() != null) {
			window.database.attach(viewModel.releasedScheduler().innerInstance);
			viewModel.releasedScheduler().IsReleased(false);
		}
		window.database.attach(schedulerToRelease.innerInstance);
		schedulerToRelease.IsReleased(true);
		return window.database.saveChanges().then(function () {
			viewModel.releasedScheduler(schedulerToRelease)
			viewModel.loading(false);
		});
	};

	async deleteScheduler(Scheduler) {
		const viewModel = this;
		window.Helper.Confirm.confirmDelete().done(function () {
			viewModel.loading(true);
			window.database.remove(Scheduler);
			return window.database.saveChanges();
		});
	};

	isReleasable(clickOnceVersion) {
		const viewModel = this;
		return !viewModel.releasedScheduler() || viewModel.releasedScheduler().ClickOnceVersion() < clickOnceVersion;
	};
}

namespace("Sms.Einsatzplanung.Connector.ViewModels").SchedulerMaterialAdminSchedulerManagementTabViewModel = SchedulerMaterialAdminSchedulerManagementTabViewModel;
