namespace Crm.Project.BusinessRules.ProjectRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class DueDateRequired : RequiredRule<Project>
	{
		public DueDateRequired()
		{
			Init(p => p.DueDate);
		}
	}
}