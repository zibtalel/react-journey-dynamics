namespace Crm.PerDiem.BusinessRules.ExpenseRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.PerDiem.Model;

	public class AmountRequired : RequiredRule<Expense>
	{
		// Constructor
		public AmountRequired()
		{
			Init(e => e.Amount);
		}
		protected override bool IsIgnoredFor(Expense entity)
		{
			return entity.ActualType != typeof(UserExpense);
		}
	}
}