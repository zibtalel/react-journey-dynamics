namespace Crm.PerDiem.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;
	using Crm.PerDiem.Model;

	[Migration(20190614112400)]
	public class AddEntityAuthData : Migration
	{
		public override void Up()
		{
			var helper = new UnicoreMigrationHelper(Database);
			helper.AddEntityTypeAndAuthDataColumnIfNeeded<UserExpense>("SMS", "Expense");
			helper.AddEntityTypeAndAuthDataColumnIfNeeded<UserTimeEntry>("SMS", "TimeEntry");
		}
	}
}
