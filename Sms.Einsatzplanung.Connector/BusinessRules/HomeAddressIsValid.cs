namespace Sms.Einsatzplanung.Connector.BusinessRules
{
	using System.Linq;

	using Crm.Library.Model;
	using Crm.Library.Services.Interfaces;
	using Crm.Library.Validation;

	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.Services;

	public class HomeAddressIsValid : Rule<User>
	{
		private readonly HomeAddressPluginUserSaveExtension homeAddressPluginUserSaveExtension;
		private readonly IRuleValidationService ruleValidationService;
		public HomeAddressIsValid(HomeAddressPluginUserSaveExtension homeAddressPluginUserSaveExtension, IRuleValidationService ruleValidationService)
			: base(RuleClass.Undefined)
		{
			this.homeAddressPluginUserSaveExtension = homeAddressPluginUserSaveExtension;
			this.ruleValidationService = ruleValidationService;
		}
		protected override RuleViolation CreateRuleViolation(User entity)
		{
			var address = homeAddressPluginUserSaveExtension.GetHomeAddress(entity.GetExtension<UserExtension>(), true);
			return ruleValidationService.GetRuleViolations(address).FirstOrDefault()?.SetDisplayRegion("HomeAddress");

		}
		public override bool IsSatisfiedBy(User entity)
		{
			return CreateRuleViolation(entity) == null;
		}
	}
}