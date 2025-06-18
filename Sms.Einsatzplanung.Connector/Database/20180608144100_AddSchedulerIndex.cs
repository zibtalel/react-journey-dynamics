namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20180608144100)]
	public class AddSchedulerIndex : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("CREATE UNIQUE INDEX [IX_UQ_ReleasedScheduler] ON [SMS].[Scheduler] ([IsReleased], [IsActive]) WHERE [IsReleased] = 1 AND [IsActive] = 1");
		}
	}
}