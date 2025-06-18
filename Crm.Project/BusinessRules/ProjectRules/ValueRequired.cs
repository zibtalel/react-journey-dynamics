namespace Crm.Project.BusinessRules.ProjectRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class ValueRequired : RequiredRule<Project>
	{
		public override bool IsSatisfiedBy(Project project)
		{
			return project.Value != null && project.Value.Value >= 0;
		}

		protected override bool IsIgnoredFor(Project project)
		{
			return base.IsIgnoredFor(project);
		}

		// Constrcutor
		public ValueRequired()
		{
			Init(p => p.Value);
		}
	}
}