namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20180411180099)]
	public class RemoveSchedulerIndex : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("DROP INDEX IX_UQ_ActiveSchedulerConfig ON [SMS].[SchedulerConfig]");
			Database.ExecuteNonQuery("DROP INDEX IX_UQ_ActiveSchedulerIcon ON [SMS].[SchedulerIcon]");
			Database.ExecuteNonQuery("DROP INDEX IX_UQ_ReleasedScheduler ON [SMS].[Scheduler]");
		}
	}
}