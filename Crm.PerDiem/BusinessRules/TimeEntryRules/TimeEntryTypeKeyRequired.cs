namespace Crm.PerDiem.BusinessRules.TimeEntryRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.PerDiem.Model;

	public class TimeEntryTypeKeyRequired : RequiredRule<UserTimeEntry>
	{
		public TimeEntryTypeKeyRequired()
		{
			Init(x => x.TimeEntryTypeKey);
		}
	}
}