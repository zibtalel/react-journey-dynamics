namespace Crm.PerDiem.BusinessRules.ExpenseRules
{
	using Crm.Library.Validation.BaseRules;
	using Crm.PerDiem.Model;

	public class AmountNotNegative : NotNegativeRule<Expense>
	{
		// Constructor
		public AmountNotNegative()
		{
			Init(e => e.Amount);
		}
	}
}