namespace Sms.Einsatzplanung.Connector.BusinessRules.SchedulerBinary
{
	using System;
	using System.IO;
	using System.Linq;

	using Crm.Library.Validation;

	using Sms.Einsatzplanung.Connector.Model;

	public class FileNameIsValid : Rule<SchedulerBinary>
	{
		public FileNameIsValid()
			: base(RuleClass.Format)
		{
		}
		protected override RuleViolation CreateRuleViolation(SchedulerBinary entity)
		{
			return RuleViolation(entity, x => x.Filename, errorMessageKey: "SchedulerBinaryFileName").SetDisplayRegion("SchedulerBinary");
		}
		public override bool IsSatisfiedBy(SchedulerBinary entity)
		{
			var nameWithoutExtension = Path.GetFileNameWithoutExtension(entity.Filename);
			string[] dangerousNames = { "CON", "PRN", "AUX", "CLOCK$", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9" };
			
			return !dangerousNames.Any(n => nameWithoutExtension != null && nameWithoutExtension.Equals(n, StringComparison.OrdinalIgnoreCase));
		}
	}
}
