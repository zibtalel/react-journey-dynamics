(function($data) {
	$data.Queryable.prototype.specialFunctions.orderByProjectStatus = {
		"oData": function (urlSearchParams, data) {
			urlSearchParams.append("orderByProjectStatus", data.keys);
		},
		"webSql": function (query, data) {
			var orderBy = data.keys.map(function (x) { return "it.StatusKey === " + x; })
				.reduce(function (ob, x) { return x + (ob ? " || " + ob : ""); }, "");
			query = query.orderByDescending(orderBy);
			return query;
		}
	};
})($data);