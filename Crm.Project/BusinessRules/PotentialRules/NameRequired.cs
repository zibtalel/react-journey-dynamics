namespace Crm.Project.BusinessRules.PotentialRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class NameRequired : RequiredRule<Potential>
	{
		public NameRequired()
		{
			Init(p => p.Name);
		}
	}
}