namespace Crm.PerDiem.BusinessRules.TimeEntryRules
{
	using Crm.Library.Validation;
	using Crm.Library.Validation.BaseRules;
	using Crm.PerDiem.Model;

	public class ToGreaterThanFrom : OrderRule<TimeEntry>
	{
		protected override bool IsIgnoredFor(TimeEntry timeEntry)
		{
			return !timeEntry.From.HasValue
			       || !timeEntry.To.HasValue
			       || (timeEntry.DurationInMinutes ?? 0) == 0
			       || timeEntry.DurationInMinutes.HasValue && timeEntry.DurationInMinutes.Value == 24 * 60;
		}

		protected override RuleViolation CreateRuleViolation(TimeEntry timeEntry)
		{
			// TODO: Replace magic strings by Lambda expressions, compare From and To as TimeSpan elements
			var options = new RuleViolationOptions
			{
				Entity = timeEntry,
				PropertyName = "ToAsString",
				PropertyNameReplacementKey = "To",
				OtherPropertyName = "FromAsString",
				OtherPropertyNameReplacementKey = "From",
				RuleClass = RuleClass
			};
			return RuleViolation(options);
		}
		// Conctructor
		public ToGreaterThanFrom()
		{
			Init(t => t.To, t => t.From, ValueOrder.FirstValueGreater);
		}
	}
}