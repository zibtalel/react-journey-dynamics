;
(function() {
	function getOverlappingTimeEntry(id, username, date, from, to, callback) {
		if (!username || !date || !from || !to || from > to || !window.database.CrmPerDiem_UserTimeEntry || window.Crm.PerDiem.Settings.TimeEntry.AllowOverlap) {
			callback(null);
			return;
		}
		window.database.CrmPerDiem_UserTimeEntry
			.first(function(timeEntry) {
					return timeEntry.ResponsibleUser === this.username &&
						!(timeEntry.Date < this.date || timeEntry.Date > this.date) &&
						timeEntry.From < this.to &&
						timeEntry.To > this.from &&
						timeEntry.Id !== this.id;
				},
				{ id: id, username: username, date: date, from: from, to: to })
			.then(function(result) {
				callback(result);
			}).fail(function() {
				callback(null);
			});
	}

	window.OverlappingTimeEntryValidator = window._.debounce(function(val, params, callback) {
		var timeEntry = window.ko.unwrap(this);
		getOverlappingTimeEntry(timeEntry.Id(),
			window.ko.unwrap(timeEntry.ResponsibleUser || timeEntry.Username),
			timeEntry.Date(),
			timeEntry.From(),
			timeEntry.To(),
			function(overlappingTimeEntry) {
				if (!overlappingTimeEntry) {
					callback(true);
				} else {
					var message = window.Helper.String.getTranslatedString("OverlappingByTimeEntries")
						.replace("{0}", window.Globalize.formatDate(overlappingTimeEntry.From, { datetime: "medium" }))
						.replace("{1}", window.Globalize.formatDate(overlappingTimeEntry.To, { datetime: "medium" }));
					callback({ isValid: false, message: message });
				}
			});
	});
})();