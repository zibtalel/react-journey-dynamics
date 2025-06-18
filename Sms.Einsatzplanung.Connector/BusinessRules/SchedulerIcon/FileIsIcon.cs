namespace Sms.Einsatzplanung.Connector.BusinessRules.SchedulerIcon
{
	using Crm.Library.Validation;

	using Sms.Einsatzplanung.Connector.Model;
	using Sms.Einsatzplanung.Connector.Services;

	public class FileIsIcon : Rule<SchedulerIcon>
	{
		private readonly ISchedulerService schedulerService;
		public FileIsIcon(ISchedulerService schedulerService)
			: base(RuleClass.Format)
		{
			this.schedulerService = schedulerService;
		}
		protected override RuleViolation CreateRuleViolation(SchedulerIcon entity)
		{
			return RuleViolation(entity, x => x.Icon, errorMessageKey: "SchedulerIcon").SetDisplayRegion("SchedulerIcon");
		}
		public override bool IsSatisfiedBy(SchedulerIcon entity)
		{
			return schedulerService.ValidateIcon(entity.Icon);
		}
	}
}
