namespace Crm.Configurator.BusinessRules.ConfigurationRule
{
	using System.Linq;

	using Crm.Configurator.Model;
	using Crm.Library.Validation;

	public class VariableValuesRequired : Rule<ConfigurationRule>
	{
		public override bool IsSatisfiedBy(ConfigurationRule configurationRule)
		{
			return configurationRule.VariableValues != null && configurationRule.VariableValues.Any();
		}

		protected override RuleViolation CreateRuleViolation(ConfigurationRule configurationRule)
		{
			var options = new RuleViolationOptions
			{
				Entity = configurationRule,
				PropertyName = "VariableValues",
				ErrorMessageKey = "VariableValuesRequired",
				Rule = this,
				RuleClass = RuleClass
			};

			return RuleViolation(options);
		}

		public VariableValuesRequired()
			: base(RuleClass.Required)
		{
		}
	}
}