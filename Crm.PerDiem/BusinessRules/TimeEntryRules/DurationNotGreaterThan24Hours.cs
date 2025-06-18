namespace Crm.PerDiem.BusinessRules.TimeEntryRules
{
	using Crm.Library.Validation;
	using Crm.PerDiem.Model;

	public class DurationInMinutesNotGreaterThan24Hours : Rule<TimeEntry>
	{
		protected override bool IsIgnoredFor(TimeEntry timeEntry)
		{
			return timeEntry.DurationInMinutes == null;
		}

		public override bool IsSatisfiedBy(TimeEntry timeEntry)
		{
			return timeEntry.DurationInMinutes <= 24 * 60;
		}

		protected override RuleViolation CreateRuleViolation(TimeEntry timeEntry)
		{
			return RuleViolation(timeEntry, t => t.DurationAsString, "Duration");
		}

		// Constructor
		public DurationInMinutesNotGreaterThan24Hours()
			: base(RuleClass.MaxValue)
		{
		}
	}
}