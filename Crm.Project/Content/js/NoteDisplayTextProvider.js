(function(Helper) {
	window.Main.ViewModels.NoteViewModel.registerDisplayTextProvider("ProjectCreatedNote", function(note) {
		var text = Helper.String.getTranslatedString("ProjectCreated");
		return new $.Deferred().resolve(text).promise();
	});
	window.Main.ViewModels.NoteViewModel.registerDisplayTextProvider("ProjectStatusChangedNote", function(note) {
		var key = note.Text;
		return Helper.Lookup.getLocalizedArrayMap("CrmProject_ProjectStatus").then(function(map) {
			if (map && map[key]) {
				return Helper.String.getTranslatedString("ProjectStatusSet").replace("{0}", map[key].Value);
			}
			return key;
		});
	});
	window.Main.ViewModels.NoteViewModel.registerDisplayTextProvider("ProjectLostNote", function (note) {
		var noteTexts = note.Text.split(";|;");
		if (note.Text.indexOf(";|;") === -1 && noteTexts.length !== 3) {
			return new $.Deferred().resolve(Helper.String.getTranslatedString("ProjectLost") + '\n' + note.Text).promise();
		}
		var displayText = "";
		displayText += Helper.String.getTranslatedString("ProjectLost");
		displayText += noteTexts[0] === "" ? "" : '\n' + Helper.String.getTranslatedString("Category") + ': ' + noteTexts[0];
		displayText += noteTexts[1] === "" ? "" : '\n' + noteTexts[1] + " " + Helper.String.getTranslatedString("Competitor");
		displayText += noteTexts[2] === "" ? "" : '\n' + Helper.String.getTranslatedString("Remark") + ': ' + noteTexts[2];
		return new $.Deferred().resolve(displayText).promise();
	});
})(window.Helper = window.Helper || {});