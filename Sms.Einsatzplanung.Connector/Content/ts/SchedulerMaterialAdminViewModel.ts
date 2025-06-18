///<reference path="../../../../Content/@types/index.d.ts" />
import {namespace} from "../../../../Content/ts/namespace";

export class SchedulerMaterialAdminViewModel extends window.Main.ViewModels.ViewModelBase {
	tabs = ko.observable({});
	icon = ko.observable<Sms.Einsatzplanung.Connector.Rest.Model.ObservableSmsEinsatzplanungConnector_SchedulerIcon>(null);
	config = ko.observable<Sms.Einsatzplanung.Connector.Rest.Model.ObservableSmsEinsatzplanungConnector_SchedulerConfig>(null);
	configFileName = ko.pureComputed(() => {
		if (this.config()) {
			const configDate = window.Globalize.formatDate(this.config().ModifyDate(), {raw: "yy-MM-dd_hh-mm-ss"});
			return "SchedulerConfigTransformation_" + configDate + ".zip";
		}
		return null;
	})

	async init() {
		await this.loadConfig();
		await this.loadIcon();
		window.Helper.Database.registerEventHandlers(this,
			{
				SmsEinsatzplanungConnector_SchedulerIcon: {
					afterCreate: this.reloadIcon,
					afterUpdate: this.reloadIcon,
					afterDelete: this.reloadIcon
				}
			});
		window.Helper.Database.registerEventHandlers(this,
			{
				SmsEinsatzplanungConnector_SchedulerConfig: {
					afterCreate: this.reloadConfig,
					afterUpdate: this.reloadConfig,
					afterDelete: this.reloadConfig
				}
			});
	};

	async deleteConfig() {
		this.loading(true);
		const usedCount = await window.database.SmsEinsatzplanungConnector_Scheduler.filter("it.ConfigKey === this.configId", {configId: this.config().Id()}).count();
		if (usedCount > 0) {
			this.loading(false);
			window.swal(window.Helper.String.getTranslatedString("Error"), window.Helper.String.getTranslatedString("ConfigAlreadyInUse"), "error");
			return;
		}
		window.database.remove(this.config().innerInstance);
		await window.database.saveChanges();
		this.config(null);
		this.loading(false);
	};

	async deleteIcon() {
		this.loading(true);
		const usedCount = await window.database.SmsEinsatzplanungConnector_Scheduler.filter("it.IconKey === this.iconId", {iconId: this.icon().Id()}).count();
		if (usedCount > 0) {
			this.loading(false);
			window.swal(window.Helper.String.getTranslatedString("Error"), window.Helper.String.getTranslatedString("IconAlreadyInUse"), "error");
			return;
		}
		window.database.remove(this.icon().innerInstance);
		await window.database.saveChanges();
		this.icon(null);
		this.loading(false);
	};

	async loadConfig() {
		const config = await window.database.SmsEinsatzplanungConnector_SchedulerConfig.orderByDescending("it.CreateDate").take(1).toArray();
		if (config.length > 0) {
			this.config(config[0].asKoObservable());
		} else {
			this.config(null);
		}
	};

	async loadIcon() {
		const icon = await window.database.SmsEinsatzplanungConnector_SchedulerIcon.orderByDescending("it.CreateDate").take(1).toArray();
		if (icon.length > 0) {
			this.icon(icon[0].asKoObservable());
		} else {
			this.icon(null);
		}
	};

	async reloadConfig() {
		this.loading(true);
		await this.loadConfig();
		this.loading(false);
	};

	async reloadIcon() {
		this.loading(true);
		await this.loadIcon();
		this.loading(false);
	};
}

namespace("Sms.Einsatzplanung.Connector.ViewModels").SchedulerMaterialAdminViewModel = SchedulerMaterialAdminViewModel; 