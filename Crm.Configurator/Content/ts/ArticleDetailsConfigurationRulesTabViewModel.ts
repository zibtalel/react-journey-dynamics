///<reference path="../../../../Content/@types/index.d.ts" />
import { HelperBatch } from "../../../../Content/ts/helper/Helper.Batch";
import { namespace } from "../../../../Content/ts/namespace";

export class ArticleDetailsConfigurationRulesTabViewModel extends window.Main.ViewModels.GenericListViewModel {
	args: any

	constructor(args) {
		super("CrmConfigurator_ConfigurationRule", "Validation", "DESC", ["ConfigurationBase"]);
		this.args = args;
		this.getFilter("ConfigurationBaseId").extend({ filterOperator: "===" })(args.article().Id());
	}

	async init(): Promise<void> {
		await super.init();
	}

	initItems(items) {
		const queries = [];
		items.map(item => {
			item.affectedVariableValueArticle = ko.observable(null);
			item.variableValuesArticle = ko.observable(null);
			queries.push(
				{
					queryable: window.database.CrmArticle_Article.filter("it.Id === this.id",
						{ id: item.AffectedVariableValues()[0] }),
					method: "first",
					handler: function (article) {
						if (article) {
							item.affectedVariableValueArticle(article.asKoObservable());
						}
						return items;
					}
				},
				{
					queryable: window.database.CrmArticle_Article.filter("it.Id === this.id",
						{ id: item.VariableValues()[0] }),
					method: "first",
					handler: function (article) {
						if (article) {
							item.variableValuesArticle(article.asKoObservable());
						}
						return items;
					}
				},
			);
		})
		return HelperBatch.Execute(queries).then(function () {
			return items;
		});

	}

	async deleteConfiguration(data): Promise<void> {
		try {
			this.loading(true);
			await window.Helper.Confirm.confirmDelete()
			window.database.attachOrGet(data.innerInstance);
			window.database.remove(data);
			await window.database.saveChanges();
			this.loading(false);
		} catch (e) {
			this.loading(false);
			window.swal(window.Helper.String.getTranslatedString("Error"), (e as Error).message, "error");
		}
	}

}

namespace("Crm.Article.ViewModels").ArticleDetailsConfigurationRulesTabViewModel = ArticleDetailsConfigurationRulesTabViewModel;
