namespace Crm.PerDiem.BusinessRules.ExpenseRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.PerDiem.Model;

	public class DescriptionMaxLength : MaxLengthRule<UserExpense>
	{
		// Constructor
		public DescriptionMaxLength()
		{
			Init(e => e.Description, 500);
		}
	}
}