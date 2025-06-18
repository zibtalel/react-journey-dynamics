namespace Crm.Project.BusinessRules.ProjectRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class SourceTypeKeyRequired : RequiredRule<Project>
	{
		public SourceTypeKeyRequired()
		{
			Init(p => p.SourceTypeKey);
		}
	}
}