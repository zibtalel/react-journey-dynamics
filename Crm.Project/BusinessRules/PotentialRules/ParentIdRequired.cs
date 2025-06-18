namespace Crm.Project.BusinessRules.PotentialRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class ParentIdRequired : RequiredRule<Potential>
	{
		public ParentIdRequired()
		{
			Init(p => p.ParentId, "ParentName");
		}
	}
}