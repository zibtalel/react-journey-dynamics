namespace Crm.Project.BusinessRules.DocumentEntryRules {
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class PersonKeyRequired : RequiredRule<DocumentEntry> {
		public PersonKeyRequired() {
			Init(p => p.PersonKey);
		}
	}
}