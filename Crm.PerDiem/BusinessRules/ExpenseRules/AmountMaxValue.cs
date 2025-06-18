namespace Crm.PerDiem.BusinessRules.ExpenseRules
{
	using Crm.Library.Validation;
	using Crm.PerDiem.Model;

	[Rule]
	public class AmountMaxValue : Rule<Expense>
	{
		public override bool IsSatisfiedBy(Expense expense)
		{
			return !expense.Amount.HasValue || expense.Amount.Value < 10000000;
		}

		protected override RuleViolation CreateRuleViolation(Expense expense)
		{
			return RuleViolation(expense, c => c.Amount);
		}

		public AmountMaxValue()
			: base(RuleClass.MaxValue)
		{
		}
	}
}