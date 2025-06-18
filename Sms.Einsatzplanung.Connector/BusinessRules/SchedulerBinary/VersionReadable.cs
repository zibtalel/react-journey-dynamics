namespace Sms.Einsatzplanung.Connector.BusinessRules.SchedulerBinary
{
	using Crm.Library.Validation;

	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.Services;

	public class VersionReadable : Rule<SchedulerBinary>
	{
		private readonly ISchedulerService schedulerService;
		public VersionReadable(ISchedulerService schedulerService)
			: base(RuleClass.Format)
		{
			this.schedulerService = schedulerService;
		}
		protected override RuleViolation CreateRuleViolation(SchedulerBinary entity)
		{
			return RuleViolation(entity, x => x.Filename, errorMessageKey: "SchedulerBinaryVersion").SetDisplayRegion("SchedulerBinary");
		}
		public override bool IsSatisfiedBy(SchedulerBinary entity)
		{
			return schedulerService.ReadVersion(entity.FileInfo) != null;
		}
	}
}