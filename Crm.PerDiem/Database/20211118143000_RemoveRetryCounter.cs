namespace Crm.PerDiem.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20211118143000)]
	public class RemoveRetryCounter : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("UPDATE CRM.PerDiemReport SET IsSent = 1 WHERE RetryCounter = 3");
			Database.RemoveColumnIfEmpty("CRM.PerDiemReport", "RetryCounter", 0);
		}
	}
}
