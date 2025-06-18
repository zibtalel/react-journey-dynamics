/// <reference path="../../../../../content/js/system/moment.js" />
/// <reference types="moment"/>

/**
 * @typedef {Object} TimeEntryType
 * @property {KnockoutObservable<Date>} From
 * @property {KnockoutObservable<Date>} To
 * @property {KnockoutObservable<Date>} Date
 * @property {KnockoutObservable<string | null>} Duration
 */

class HelperTimeEntry {
	/**
	 * @param username {string}
	 * @param date {Date}
	 * @returns {IPromise<any | null>}
	 */
	static getLatestTimeEntry(username, date) {
		if (!username) {
			return new $.Deferred().resolve(null).promise();
		}
		/** @type {Date} */
		const dateStart = window.moment(date).startOf("day").toDate();
		/** @type {Date} */
		const dateEnd = window.moment(date).endOf("day").toDate();
		return window.database.CrmPerDiem_UserTimeEntry.filter(function(x) {
				return x.ResponsibleUser === this.username &&
					x.Date >= this.dateStart &&
					x.Date <= this.dateEnd;
			},
			{ username: username, dateStart: dateStart, dateEnd: dateEnd })
			.orderByDescending(function(x) { return x.To; })
			.take(1)
			.toArray()
			.then(function(results) {
				return results.length > 0 ? results[0] : null;
			});
	}

	/**
	 * @param username {string}
	 * @param date {Date}
	 * @returns {Date | null}
	 */
	static getLatestTimeEntryToOrDefault(username, date) {
		/** @type {Date} */
		const dateStart = window.moment(date).startOf("day").toDate();
		/** @type {Date} */
		const defaultStart = window.moment(dateStart).add(window.moment.duration(window.Crm.PerDiem.Settings.TimeEntry.DefaultStart)).toDate();
		return HelperTimeEntry.getLatestTimeEntry(username, date).then(function(result) {
			return result ? result.To : defaultStart;
		});
	}

	/** @this TimeEntryType */
	static initFromAndTo(duration) {
		if (!duration) {
			return;
		}
		if (!this.From()) {
			this.From(this.Date());
		}

		/** @type {Duration} */
		const mDuration = window.moment.duration(duration);
		/** @type {Moment} */
		const mTo = window.moment(this.From()).add(mDuration);
		if (!mTo.isSame(this.To())) {
			this.To(mTo.toDate());
		}
	}

	/** @param timeEntry {TimeEntryType} */
	static updateDuration(timeEntry) {
		if (timeEntry.From() && timeEntry.To() && timeEntry.Date()) {
			/** @type {moment.Moment} */
			const mFrom = window.moment(timeEntry.From());
			/** @type {moment.Moment} */
			const mTo = window.moment(timeEntry.To());
			/** @type {moment.Duration} */
			const duration = window.moment.duration(mTo.diff(mFrom));
			if (mFrom.isValid() && mTo.isValid() && timeEntry.Duration() !== duration.toString()) {
				timeEntry.Duration(duration.toString());
			}
		} else if (timeEntry.Duration()) {
			timeEntry.Duration(null);
		}
	}

	/** @this TimeEntryType
	 *  @param to {Date} */
	static updateFromAndDuration(to) {
		if (to && this.Date()) {
			let changedDate = this.Date();
			if (to.getHours() === 0 && to.getMinutes() === 0) {
				changedDate = window.moment(changedDate).add(1, "day").toDate();
			}
			var newDate = new Date(changedDate.getUTCFullYear(), changedDate.getUTCMonth(), changedDate.getUTCDate(), to.getHours(), to.getMinutes());
			to.setYear(newDate.getFullYear());
			to.setMonth(newDate.getMonth());
			to.setDate(newDate.getDate());
			to.setHours(newDate.getHours());
			to.setMinutes(newDate.getMinutes());
			if (this.From() && to.getUTCDate() === this.From().getUTCDate() && this.From() > to) {
				this.From(to);
			}
		}
		Helper.TimeEntry.updateDuration(this);
	}

	/** @this TimeEntryType
	 * @param from {Date} */
	static updateToAndDuration(from) {
		if (from && this.Date()) {
			var newDate = new Date(this.Date().getUTCFullYear(), this.Date().getUTCMonth(), this.Date().getUTCDate(), from.getHours(), from.getMinutes());
			from.setYear(newDate.getFullYear());
			from.setMonth(newDate.getMonth());
			from.setDate(newDate.getDate());
			from.setHours(newDate.getHours());
			from.setMinutes(newDate.getMinutes());
			if (this.To() && from.getUTCDate() === this.To().getUTCDate() && from > this.To() &&
				!(this.To().getHours() === 0 && this.To().getMinutes() === 0)) {
				this.To(from);
			}
		}
		Helper.TimeEntry.updateDuration(this);
	}

	/** @this TimeEntryType
	 * @param date {Date} */
	static updateFromAndTo(date) {
		if (date) {
			if (this.From()) {
				this.From().setUTCFullYear(date.getUTCFullYear());
				this.From().setUTCMonth(date.getUTCMonth());
				this.From().setUTCDate(date.getUTCDate());
			}
			if (this.To()) {
				this.To().setUTCFullYear(date.getUTCFullYear());
				this.To().setUTCMonth(date.getUTCMonth());
				this.To().setUTCDate(date.getUTCDate());
			}
		}
	}
}

(window.Helper = window.Helper || {}).TimeEntry = HelperTimeEntry;