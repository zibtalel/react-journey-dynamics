namespace Crm.Configurator.BusinessRules.ConfigurationRule
{
	using System.Linq;

	using Crm.Configurator.Model;
	using Crm.Library.Validation;

	public class AffectedVariableValuesRequired : Rule<ConfigurationRule>
	{
		public override bool IsSatisfiedBy(ConfigurationRule configurationRule)
		{
			return configurationRule.AffectedVariableValues != null && configurationRule.AffectedVariableValues.Any();
		}

		protected override RuleViolation CreateRuleViolation(ConfigurationRule configurationRule)
		{
			var options = new RuleViolationOptions
			{
				Entity = configurationRule,
				PropertyName = "AffectedVariableValues",
				ErrorMessageKey = "AffectedVariableValuesRequired",
				Rule = this,
				RuleClass = RuleClass
			};

			return RuleViolation(options);
		}

		public AffectedVariableValuesRequired()
			: base(RuleClass.Required)
		{
		}
	}
}