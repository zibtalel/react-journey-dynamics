class HelperProjectOrder {
	static getProjects(items) {
		const idMap = items.reduce(function (map, item) {
				item.Project = window.ko.observable();
				const projectId = window.ko.unwrap(item.ExtensionValues().ProjectId);
				if (projectId) {
					if (!Array.isArray(map[projectId])) {
						map[projectId] = [];
					}
					map[projectId].push(item);
				}
				return map;
			},
			{});
		return window.database.CrmProject_Project
			.filter("it.Id in this.idList", {idList: Object.keys(idMap)})
			.toArray()
			.then(function (projects) {
				projects.forEach(function (project) {
					const items = idMap[project.Id];
					items.forEach(function (item) {
						item.Project(project.asKoObservable());
					});
				});
				return items;
			});
	}
}
(window.Helper = window.Helper || {}).ProjectOrder = HelperProjectOrder;