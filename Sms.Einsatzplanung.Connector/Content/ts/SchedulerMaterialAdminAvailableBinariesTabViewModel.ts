///<reference path="../../../../Content/@types/index.d.ts" />
import {namespace} from "../../../../Content/ts/namespace";

export class SchedulerMaterialAdminAvailableBinariesTabViewModel extends window.Main.ViewModels.GenericListViewModel {

	constructor() {
		super("SmsEinsatzplanungConnector_SchedulerBinary", "LastWriteTimeUtc", "DESC");
		this.infiniteScroll(true);
	}

	deleteBinary(entity) {
		this.loading(true);
		window.database.remove(entity.innerInstance);
		window.database.saveChanges();
	};

	async createPackage(entity) {
		this.loading(true);
		const messages = await window.database.SmsEinsatzplanungConnector_SchedulerBinary.CreatePackage(entity.Filename()).toArray();
		this.loading(false);
		$(".tab-nav a[href='#tab-scheduler-management']").tab("show");
		if (messages.length) {
			window.swal({
				title: window.Helper.String.getTranslatedString("Success"),
				text: messages.join("\n\n"),
				type: "success"
			});
		}
	};
}

namespace("Sms.Einsatzplanung.Connector.ViewModels").SchedulerMaterialAdminAvailableBinariesTabViewModel = SchedulerMaterialAdminAvailableBinariesTabViewModel;