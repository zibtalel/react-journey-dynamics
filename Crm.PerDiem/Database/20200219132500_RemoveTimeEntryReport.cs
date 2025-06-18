namespace Crm.PerDiem.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20200219132500)]
	public class RemoveTimeEntryReport : Migration
	{
		public override void Up()
		{
			Database.RemoveForeignKeyIfExisting("SMS", "TimeEntryReport", "FK_TimeEntryReport_EntityAuthData");
			Database.RemoveTableIfExisting("[SMS].[TimeEntryReport]");
		}
	}
}