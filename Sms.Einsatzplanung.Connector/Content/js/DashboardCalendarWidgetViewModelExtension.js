import { findIana } from 'windows-iana';

(function() {
	const dashboardCalendarWidgetViewModel = window.Main.ViewModels.DashboardCalendarWidgetViewModel;
	window.Main.ViewModels.DashboardCalendarWidgetViewModel = function() {
		const viewModel = this;
		dashboardCalendarWidgetViewModel.apply(this, arguments);

		if(window.database.CrmPerDiem_TimeEntryType) {
			viewModel.lookups.timeEntryTypes = {$tableName: "CrmPerDiem_TimeEntryType"};
		}

		if(window.database.SmsEinsatzplanungConnector_AbsenceDispatch && window.Sms.Einsatzplanung.Connector.Settings.DashboardCalendar.ShowAbsencesInCalendar) {
			viewModel.absenceFilterOption = {
				Value: window.database.SmsEinsatzplanungConnector_AbsenceDispatch.collectionName,
				Caption: window.Helper.String.getTranslatedString("SmsEinsatzplanungConnector_AbsenceDispatch"),
				Tooltip: window.Helper.String.getTranslatedString("AbsenceDispatchesTooltip"),
			};
			viewModel.filterOptions.push(viewModel.absenceFilterOption);
			viewModel.selectedFilters.subscribe(function(changes) {
				if(changes.some(function(change) {
					return change.status === viewModel.changeStatus.added && change.moved === undefined && change.value.Value === viewModel.absenceFilterOption.Value
				})) {
					viewModel.loading(true);
					viewModel.getAbsenceTimelineEvents().then(function(results) {
						viewModel.timelineEvents(viewModel.timelineEvents().filter(function(event) {
							return event.innerInstance.constructor.name !== viewModel.absenceFilterOption.Value
						}));
						viewModel.timelineEvents(viewModel.timelineEvents().concat(results));
						viewModel.loading(false);
					});
				}
				if(changes.some(function(change) {
					return change.status === viewModel.changeStatus.deleted && change.moved === undefined && change.value.Value === viewModel.absenceFilterOption.Value
				})) {
					viewModel.timelineEvents(viewModel.timelineEvents().filter(function(event) {
						return event.innerInstance.constructor.name !== viewModel.absenceFilterOption.Value
					}));
				}
			}, null, "arrayChange");
		}
	};
	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype = dashboardCalendarWidgetViewModel.prototype;

	const getTimelineEvent = window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getTimelineEvent;
	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getTimelineEvent = function(it) {
		const viewModel = this;
		if(window.database.SmsEinsatzplanungConnector_AbsenceDispatch
			&& it.innerInstance instanceof window.database.SmsEinsatzplanungConnector_AbsenceDispatch.SmsEinsatzplanungConnector_AbsenceDispatch) {
			const timelineEvent = viewModel.getTimelineEventForAbsence.apply(this, arguments);
			if(timelineEvent) {
				return timelineEvent;
			} else {
				return {};
			}
		} else {
			return getTimelineEvent.apply(this, arguments);
		}
	};

	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getAbsenceTimelineEvents = function() {
		const viewModel = this;
		if(window.database.SmsEinsatzplanungConnector_AbsenceDispatch && viewModel.currentUser()) {
			return window.database.SmsEinsatzplanungConnector_AbsenceDispatch
				.filter(function(it) {
						return it.Person === this.currentUser &&
							it.Start >= this.start &&
							it.Stop <= this.end
					},
					{
						currentUser: viewModel.currentUser(),
						start: viewModel.timelineStart(),
						end: viewModel.timelineEnd()
					})
				.take(viewModel.maxResults())
				.toArray()
				.then(function(results) {
					return results.map(function(x) {
						return x.asKoObservable();
					});
				});
		} else {
			return new $.Deferred().resolve([]).promise();
		}
	};

	/**
	 * The problem with absences is that they come directly from the scheduler (RPL.Dispatch).
	 * In the scheduler the data is stored with the local time, not in UTC. 
	 * That's why we need to convert this data before using it in the field application.
	 * Converting the value in the mapping is not possible, so we do it in the client.
	 * 
	 * @param absence
	 * @returns {{allDay: boolean, backgroundColor: (*|string), start: Date, end: Date, title: *}}
	 */
	window.Main.ViewModels.DashboardCalendarWidgetViewModel.prototype.getTimelineEventForAbsence = function(absence) {
		const viewModel = this;

		// https://stackoverflow.com/a/68593283
		const getOffset = (timeZone = 'UTC', date = new Date()) => {
			const utcDate = new Date(date.toLocaleString('en-US', { timeZone: 'UTC' }));
			const tzDate = new Date(date.toLocaleString('en-US', { timeZone }));
			return (tzDate.getTime() - utcDate.getTime()) / 6e4;
		}
		
		// start and end time is in local time of the scheduler
		const startTime = new Date(absence.Start());
		const endTime = new Date(absence.Stop());
		const absenceType = viewModel.lookups.timeEntryTypes[Number.parseInt(absence.AbsenceTypeKey())];
		
		// convert between windows and iana timezones
		const timezoneFromAppSettings = window.Sms.Einsatzplanung.Connector.Settings.Export.TimeZoneId;
		const ianaTimezoneIdentifier = findIana(timezoneFromAppSettings);
		const offsetStart = getOffset(ianaTimezoneIdentifier[0], startTime);
		const offsetStop = getOffset(ianaTimezoneIdentifier[0], endTime);
		
		return {
			title: absenceType.Value ? absenceType.Value : window.Helper.String.getTranslatedString("SmsEinsatzplanungConnector_AbsenceDispatch"),
			entityType : absenceType.Value ? null : window.Helper.String.getTranslatedString("SmsEinsatzplanungConnector_AbsenceDispatch"),
			start: new Date(startTime.getTime() - offsetStart * 60 * 1000),
			end: new Date(endTime.getTime() - offsetStop * 60 * 1000),
			allDay: false,
			backgroundColor: absenceType.Color ? absenceType.Color : '#0cf993'
		};
	};
})();