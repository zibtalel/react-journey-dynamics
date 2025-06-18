namespace Crm.PerDiem.Model
{
	public abstract class Expense : PerDiemEntry
	{
		public virtual decimal? Amount { get; set; }
		public virtual string CurrencyKey { get; set; }
	}
}