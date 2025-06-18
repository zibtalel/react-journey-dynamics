const generateCssTask = require("../../../gulp-core/gulptaskCore").generateCssTask;

module.exports = function () {
	// Original location : .\Plugins\Main.VideoCall
	const cssFiles = [
		"Content/css/style.css"
	];

	generateCssTask(__filename, cssFiles);

};
