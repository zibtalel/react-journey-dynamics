;
(function (ko, Helper) {
	let loadProjects = function (items) {
		let queries = [];
		items.forEach(function (item) {
			item.Project = ko.observable(null);
			if (item.ExtensionValues().ProjectId()) {
				queries.push({
					queryable: window.database.CrmProject_Project.filter(function (it) {
							return it.Id === this.projectId;
						},
						{projectId: item.ExtensionValues().ProjectId()}),
					method: "first",
					handler: function (result) {
						item.Project(result ? result.asKoObservable() : null);
					}
				});
			}
		});
		return Helper.Batch.Execute(queries);
	};

	let orderListInitItems = window.Crm.Order.ViewModels.OrderListIndexViewModel.prototype.initItems;
	window.Crm.Order.ViewModels.OrderListIndexViewModel.prototype.initItems = function (items) {
		return orderListInitItems.apply(this, arguments).then(function () {
			return loadProjects(items);
		}).then(function () {
			return items;
		});
	}

	let offerListInitItems = window.Crm.Order.ViewModels.OfferListIndexViewModel.prototype.initItems;
	window.Crm.Order.ViewModels.OfferListIndexViewModel.prototype.initItems = function (items) {
		return offerListInitItems.apply(this, arguments).then(function () {
			return loadProjects(items);
		}).then(function () {
			return items;
		});
	}
})(window.ko, window.Helper);