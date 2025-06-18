///<reference path="../../../../../Content/@types/index.d.ts" />
///<reference path="../../@types/index.d.ts" />
export class HelperPotential {
	static getQueries(productFamilyId, parentId, item, queries) {
		queries.push(
			{
				queryable: window.database.CrmProject_Potential.CountOfPotentialsByStatus(productFamilyId, parentId),
				method: "toArray",
				handler: function (values) {
					let dict = {};
					values.forEach(el => {
						dict[el.Status] = el;
					})
					item.countOfPotentialsByStatus(dict);
				}
			},
		)
		return queries;
	}

	static getName(potential): string {
		potential = window.ko.unwrap(potential || {});
		const potentialNo = window.ko.unwrap(potential.PotentialNo);
		const name = window.ko.unwrap(potential.Name);
		return [potentialNo, name].filter(function (x) { return x; }).join(" - ");
	}

	static getSelect2Filter(query, term, parentId) {
		if (term) {
			query = query.filter(function (it) {
				return it.Name.toLowerCase().contains(this.term);
			},
				{ term: term });
		}
		return query.filter("it.Parent.Id == parentId", { parentId: parentId });
	}

	/** @returns {{item, id: string, text: string}} */
	static mapDisplayNameForSelect2(potential) {
		return {
			id: potential.Id,
			text: HelperPotential.getName(potential),
			item: potential
		};
	}
}

export function InitializeOfflineQueriesPotential() {
	document.addEventListener("DatabaseInitialized", function () {
		if (window.database.storageProvider.name === "webSql") {
			if (!window.database.CrmProject_Potential) {
				return;
			}
			if (window.database.CrmProject_Potential.CountOfPotentialsByStatus) {
				throw "CrmProject_Potential.CountOfPotentialsByStatus must be undefined at this point";
			}
			window.database.CrmProject_Potential.CountOfPotentialsByStatus = function (productFamilyKey, parentId) {
				return window.database.CrmProject_Potential
					.filter("it.MasterProductFamilyKey === this.productFamilyKey && it.ParentId === this.parentId",
						{ productFamilyKey: ko.unwrap(productFamilyKey), parentId: ko.unwrap(parentId) })
					.map(function (it) {
						return {
							TotalCount: (it.Id as unknown as $data.Queryable<string>).count(),
							Status: it.StatusKey,
						}
					})
					.groupBy("it.StatusKey")
			}
		}
	})
}
