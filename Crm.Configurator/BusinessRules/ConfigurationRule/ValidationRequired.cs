namespace Crm.Configurator.BusinessRules.ConfigurationRule
{
	using Crm.Configurator.Model;
	using Crm.Library.Validation.BaseRules;

	public class ValidationRequired : RequiredRule<ConfigurationRule>
	{
		public ValidationRequired()
		{
			Init(x => x.Validation, propertyNameReplacementKey: "ConfigurationRuleType");
		}
	}
}