namespace Crm.Project.BusinessRules.ProjectRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class ParentIdRequired : RequiredRule<Project>
	{
		public ParentIdRequired()
		{
			Init(p => p.ParentId, "ParentName");
		}
	}
}