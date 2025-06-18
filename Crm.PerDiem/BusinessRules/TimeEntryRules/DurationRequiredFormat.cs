namespace Crm.PerDiem.BusinessRules.TimeEntryRules
{
	using System.Text.RegularExpressions;

	using Crm.Library.Extensions;
	using Crm.Library.Validation;
	using Crm.PerDiem.Model;

	public abstract class BaseDurationRequiredFormat<T> : Rule<T> where T : TimeEntry
	{
		protected override bool IsIgnoredFor(T timeEntry)
		{
			return timeEntry.DurationAsString.IsNullOrEmpty() || timeEntry.FromAsString.IsNotNullOrEmpty() && timeEntry.ToAsString.IsNotNullOrEmpty();
		}

		public override bool IsSatisfiedBy(T timeEntry)
		{
			var timeRegex = new Regex(@"^((?<Hours>[01]\d|2[0-3]|[0-9]h?)(:?.?␣?(?<Minutes>[0-5]\d{1})m?)?)$");
			return timeRegex.IsMatch(timeEntry.DurationAsString);
		}

		protected override RuleViolation CreateRuleViolation(T timeEntry)
		{
			return RuleViolation(timeEntry, t => t.DurationAsString, "Duration", "TimeAsStringRequiredFormatText");
		}

		public BaseDurationRequiredFormat()
			: base(RuleClass.Format)
		{
		}
	}
	public class DurationRequiredFormat : BaseDurationRequiredFormat<UserTimeEntry>
	{
	}
}
