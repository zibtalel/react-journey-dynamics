namespace Sms.Einsatzplanung.Connector.BusinessRules.SchedulerBinary
{
	using System.Text.RegularExpressions;

	using Crm.Library.Validation;

	using Sms.Einsatzplanung.Connector.Model;

	public class FilePathIsValid : Rule<SchedulerBinary>
	{
		public FilePathIsValid()
			: base(RuleClass.Format)
		{
		}
		protected override RuleViolation CreateRuleViolation(SchedulerBinary entity)
		{
			return RuleViolation(entity, x => x.Filename, errorMessageKey: "SchedulerBinaryFilePath").SetDisplayRegion("SchedulerBinary");
		}
		public override bool IsSatisfiedBy(SchedulerBinary entity)
		{
			var dangerousPattern = new Regex(@"[<>:\\\/|?*\t\n\r]|\.\.");
			var matches = dangerousPattern.Matches(entity.Filename);
			return matches.Count == 0;
		}
	}
}
