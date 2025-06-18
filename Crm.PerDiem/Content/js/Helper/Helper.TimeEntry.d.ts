/**
 * @typedef {Object} TimeEntryType
 * @property {KnockoutObservable<Date>} From
 * @property {KnockoutObservable<Date>} To
 * @property {KnockoutObservable<Date>} Date
 * @property {KnockoutObservable<string | null>} Duration
 */
declare class HelperTimeEntry {
    /**
     * @param username {string}
     * @param date {Date}
     * @returns {IPromise<any | null>}
     */
    static getLatestTimeEntry(username: string, date: Date): IPromise<any>;
    /**
     * @param username {string}
     * @param date {Date}
     * @returns {Date | null}
     */
    static getLatestTimeEntryToOrDefault(username: string, date: Date): Date | null;
    /** @this TimeEntryType */
    static initFromAndTo(this: TimeEntryType, duration: any): void;
    /** @param timeEntry {TimeEntryType} */
    static updateDuration(timeEntry: TimeEntryType): void;
    /** @this TimeEntryType
     *  @param to {Date} */
    static updateFromAndDuration(this: TimeEntryType, to: Date): void;
    /** @this TimeEntryType
     * @param from {Date} */
    static updateToAndDuration(this: TimeEntryType, from: Date): void;
    /** @this TimeEntryType
     * @param date {Date} */
    static updateFromAndTo(this: TimeEntryType, date: Date): void;
}
type TimeEntryType = {
    From: KnockoutObservable<Date>;
    To: KnockoutObservable<Date>;
    Date: KnockoutObservable<Date>;
    Duration: KnockoutObservable<string | null>;
};
