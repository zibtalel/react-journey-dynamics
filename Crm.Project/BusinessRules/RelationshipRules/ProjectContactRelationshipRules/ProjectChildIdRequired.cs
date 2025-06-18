namespace Crm.Project.BusinessRules.RelationshipRules.ProjectContactRelationshipRules
{
	using Crm.Library.Validation;
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model.Relationships;

	[Rule]
	public class ChildIdRequired : RequiredRule<ProjectContactRelationship>
	{
		public ChildIdRequired()
		{
			Init(r => r.ChildId, "Contact");
		}
	}
}