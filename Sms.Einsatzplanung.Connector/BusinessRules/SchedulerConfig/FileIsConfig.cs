namespace Sms.Einsatzplanung.Connector.BusinessRules.SchedulerConfig
{
	using System.IO;
	using Crm.Library.Validation;
	using Sms.Einsatzplanung.Connector.Services;
	using Sms.Einsatzplanung.Connector.ViewModels;

	public class FileIsConfig : Rule<SchedulerConfigViewModel>
	{
		private readonly ISchedulerService schedulerService;
		public FileIsConfig(ISchedulerService schedulerService)
			: base(RuleClass.Format)
		{
			this.schedulerService = schedulerService;
		}
		protected override RuleViolation CreateRuleViolation(SchedulerConfigViewModel config)
		{
			var tempDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), $"Scheduler_{Path.GetRandomFileName()}"));
			try
			{
				tempDir.Create();
				if (!schedulerService.ExtractConfig(config.FileInfo, tempDir, config.Type, out var errorMessageKey))
				{
					return RuleViolation(config, x => x.FileInfo, errorMessageKey: errorMessageKey).SetDisplayRegion("SchedulerConfig");
				}
			}
			finally
			{
				if (tempDir.Exists)
				{
					tempDir.Delete(true);
				}
			}

			return null;
		}
		public override bool IsSatisfiedBy(SchedulerConfigViewModel config)
		{
			var tempDir = new DirectoryInfo(Path.Combine(Path.GetTempPath(), $"Scheduler_{Path.GetRandomFileName()}"));
			try
			{
				tempDir.Create();
				schedulerService.ExtractConfig(config.FileInfo, tempDir, config.Type, out _);
				return true;
			}
			catch
			{
				/* ignore */
			}
			finally
			{
				if (tempDir.Exists)
				{
					tempDir.Delete(true);
				}
			}

			return false;
		}
	}
}