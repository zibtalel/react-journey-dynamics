namespace Sms.Einsatzplanung.Connector.BusinessRules.SchedulerBinary
{
	using System;
	using System.IO.Compression;

	using Crm.Library.Validation;

	using Sms.Einsatzplanung.Connector.Model;

	public class FileIsZip : Rule<SchedulerBinary>
	{
		public FileIsZip()
			: base(RuleClass.Format)
		{
		}
		protected override RuleViolation CreateRuleViolation(SchedulerBinary entity)
		{
			return RuleViolation(entity, x => x.Filename, errorMessageKey: "SchedulerBinaryZip").SetDisplayRegion("SchedulerBinary");
		}
		public override bool IsSatisfiedBy(SchedulerBinary entity)
		{
			try
			{
				using (ZipFile.OpenRead(entity.FileInfo.FullName))
				{
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}