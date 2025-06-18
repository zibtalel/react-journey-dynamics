$(document).ready(function () {

	var flowcharts = $('code');
	if (flowcharts.length > 0) {
		flowcharts.each(function (index) {
			var code = $(this)[0].textContent;
			var lines = code.split(/\n/);
			if (lines[0] === "flowchart-source") {
				var identifier = "canvas_" + index;
				var chart = window.flowchart.parse(code);
				var div = document.createElement("div");
				div.setAttribute("id", identifier);
				div.setAttribute("class", "flowchart-out");
				$(div).insertAfter($(this));
				chart.drawSVG(div, {
					'x': 0,
					'y': 0,
					'line-width': 1,
					'font-family': 'Open Sans',
					'line-length': 20,
					'text-margin': 10,
					'font-size': 10,
					'font-color': 'black',
					'line-color': 'black',
					'element-color': 'black',
					'fill': 'white',
					'yes-text': 'Ja',
					'no-text': 'Nein',
					'arrow-end': 'block',
					'scale': 1
				});

				$(this).hide();
			}
		});
	}

});