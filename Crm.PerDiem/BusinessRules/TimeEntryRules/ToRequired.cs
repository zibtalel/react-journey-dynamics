namespace Crm.PerDiem.BusinessRules.TimeEntryRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.PerDiem.Model;

	public class ToRequired : RequiredRule<UserTimeEntry>
	{
		public ToRequired()
		{
			Init(x => x.To);
		}
	}
}