namespace Crm.PerDiem.BusinessRules.TimeEntryRules
{
	using System;

	using Crm.Library.Extensions;
	using Crm.Library.Validation;
	using Crm.PerDiem.Model;

	public abstract class BaseDurationOrFromAndToRequired<T> : Rule<T> where T : TimeEntry
	{
		public override bool IsSatisfiedBy(T timeEntry)
		{
			return timeEntry.DurationInMinutes.HasValue && timeEntry.DurationInMinutes.Value > 0 || timeEntry.FromAsString.IsNotNullOrEmpty() && timeEntry.ToAsString.IsNotNullOrEmpty();
		}

		protected override RuleViolation CreateRuleViolation(T timeEntry)
		{
			var isFromEmpty = timeEntry.FromAsString.IsNullOrEmpty();
			var isToEmpty = timeEntry.ToAsString.IsNullOrEmpty();

			if (isFromEmpty && isToEmpty)
			{
				return RuleViolation(timeEntry, t => t.DurationAsString, "Duration");
			}

			if (isToEmpty)
			{
				return RuleViolation(timeEntry, t => t.ToAsString, "To");
			}

			if (isFromEmpty)
			{
				return RuleViolation(timeEntry, t => t.FromAsString, "From");
			}

			throw new ApplicationException("You should never reach this code.");
		}

		public BaseDurationOrFromAndToRequired()
			: base(RuleClass.Required)
		{
		}
	}
	public class DurationOrFromAndToRequired : BaseDurationOrFromAndToRequired<UserTimeEntry>
	{
	}
}
