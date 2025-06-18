namespace Crm.PerDiem.BusinessRules.TimeEntryRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.PerDiem.Model;

	public class FromRequired : RequiredRule<UserTimeEntry>
	{
		public FromRequired()
		{
			Init(x => x.From);
		}
	}
}