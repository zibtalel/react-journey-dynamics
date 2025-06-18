namespace Crm.Project.BusinessRules.ProjectRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class NameMaxLength : MaxLengthRule<Project>
	{
		public NameMaxLength()
		{
			Init(c => c.Name, 450);
		}
	}
}