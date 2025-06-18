///<reference path="../../../../Content/@types/index.d.ts" />
import { namespace } from "../../../../Content/ts/namespace";

export class ConfigurationEditModalViewModel extends window.Main.ViewModels.ViewModelBase {

	configurationRule = ko.observable<Crm.Configurator.Rest.Model.ObservableCrmConfigurator_ConfigurationRule>(null);
	args;
	ruleExist = ko.observable<boolean>(false);
	existingConfiguration: any = null;

	constructor(args) {
		super();
		this.args = args;
	}

	async init(id): Promise<void> {
		this.existingConfiguration = await window.database.CrmConfigurator_ConfigurationBase.include("ConfigurationRules")
			.select("it.ConfigurationRules")
			.find(this.args.article().Id());
		const configurationRuleData = window.database.CrmConfigurator_ConfigurationRule.defaultType.create();
		configurationRuleData.ConfigurationBaseId = id;
		this.configurationRule(configurationRuleData.asKoObservable());

		this.configurationRule().AffectedVariableValues.extend({
			required: {
				message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Article")),
				params: true
			}
		});
		this.configurationRule().VariableValues.extend({
			required: {
				message: window.Helper.String.getTranslatedString("RuleViolation.Required").replace("{0}", window.Helper.String.getTranslatedString("Article")),
				params: true
			}
		});

		window.database.add(this.configurationRule());
	}
	isRuleExisting(){
		for (let configuration of this.existingConfiguration) {
			const affectedVariableValuesExisting = configuration.AffectedVariableValues.some(x => this.configurationRule().AffectedVariableValues().includes(x));
			const variableValuesExisting = configuration.VariableValues.some(x => this.configurationRule().VariableValues().includes(x));

			if (variableValuesExisting && affectedVariableValuesExisting && configuration.Validation === this.configurationRule().Validation()) {
				this.ruleExist(true);
			} else {
				this.ruleExist(false);
			}
		}
	}

	async save(): Promise<void> {
		this.loading(true);
		let errors = window.ko.validation.group(this.configurationRule);
		await errors.awaitValidation();
		if (errors().length > 0) {
			this.loading(false);
			errors.showAllMessages();
			return;
		}
		this.isRuleExisting()
		if (this.ruleExist()) {
			this.loading(false);
			return;
		}

		try {
			await window.database.saveChanges();
			this.loading(false);
			$(".modal:visible").modal("hide");
		} catch (e) {
			this.loading(false);
			window.swal(window.Helper.String.getTranslatedString("Error"), (e as Error).message, "error");
		}
	}
}

namespace("Crm.Configurator.ViewModels").ConfigurationEditModalViewModel = ConfigurationEditModalViewModel
