namespace Crm.Project.BusinessRules.ProjectRules
{
	using Crm.Library.Validation;
	using Crm.Project.Model;

	public class ValueNotNegative : Rule<Project>
	{
		public override bool IsSatisfiedBy(Project project)
		{
			return project.Value == null || project.Value.Value >= 0;
		}

		protected override RuleViolation CreateRuleViolation(Project project)
		{
			return RuleViolation(project, p => p.Value);
		}

		// Constructor
		public ValueNotNegative()
			: base(RuleClass.NotNegative)
		{
		}
	}
}