namespace Crm.Services
{
	using System.Collections.Generic;

	using Crm.Library.Model;
	using Crm.Library.Validation;
	using Crm.Services.Interfaces;

	public class DischargedLoginValidator : ILoginValidator
	{
		public virtual IEnumerable<RuleViolation> GetRuleViolations(User user)
		{
			if (user != null && user.Discharged)
			{
				yield return new RuleViolation("AccountInactive");
			}
		}
	}
}
