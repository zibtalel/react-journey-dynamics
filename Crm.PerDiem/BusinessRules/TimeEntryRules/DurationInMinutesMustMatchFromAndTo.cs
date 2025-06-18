namespace Crm.PerDiem.BusinessRules.TimeEntryRules
{
	using Crm.Library.Validation;
	using Crm.PerDiem.Model;

	public class DurationInMinutesMustMatchFromAndTo : Rule<TimeEntry>
	{
		protected override bool IsIgnoredFor(TimeEntry timeEntry)
		{
			return timeEntry.From == null || timeEntry.To == null || timeEntry.DurationInMinutes == null;
		}

		public override bool IsSatisfiedBy(TimeEntry timeEntry)
		{
			return (int)(timeEntry.To.Value - timeEntry.From.Value).TotalMinutes == timeEntry.DurationInMinutes.Value
			       || (int)(timeEntry.To.Value - timeEntry.From.Value).TotalMinutes == 0 && timeEntry.DurationInMinutes.Value == 24 * 60;
		}

		protected override RuleViolation CreateRuleViolation(TimeEntry timeEntry)
		{
			var options = new RuleViolationOptions
			{
				Entity = timeEntry,
				PropertyName = "DurationAsString",
				ErrorMessageKey = "DurationAndFromToDoNotMatch",
				Rule = this,
				RuleClass = RuleClass
			};

			return RuleViolation(options);
		}

		public DurationInMinutesMustMatchFromAndTo()
			: base(RuleClass.Undefined)
		{
		}
	}
}