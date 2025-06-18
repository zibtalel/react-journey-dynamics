namespace Crm.PerDiem.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20200219132100)]
	public class RemoveExpenseReport : Migration
	{
		public override void Up()
		{
			Database.RemoveForeignKeyIfExisting("SMS", "ExpenseReport", "FK_ExpenseReport_EntityAuthData");
			Database.RemoveTableIfExisting("[SMS].[ExpenseReport]");
		}
	}
}