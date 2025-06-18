namespace Crm.Project.BusinessRules.ProjectRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class NameRequired : RequiredRule<Project>
	{
		public NameRequired()
		{
			Init(p => p.Name);
		}
	}
}