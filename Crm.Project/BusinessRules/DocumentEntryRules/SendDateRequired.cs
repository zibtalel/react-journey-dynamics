namespace Crm.Project.BusinessRules.DocumentEntryRules {
	using Crm.Library.Validation.BaseRules;
	using Crm.Project.Model;

	public class SendDateRequired : RequiredRule<DocumentEntry> {
		public SendDateRequired() {
			Init(p => p.SendDate);
		}
	}
}