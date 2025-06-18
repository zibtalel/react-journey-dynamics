namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20180820144600)]
	public class ChangeNewIdToNewSequentialId : Migration
	{
		public override void Up()
		{
			Database.DropDefault("RPL", "Dispatch", "InternalId");
			Database.ExecuteNonQuery("ALTER TABLE [RPL].[Dispatch] ADD CONSTRAINT [DF_Dispatch_InternalId] DEFAULT (newsequentialid()) FOR [InternalId]");
			
			Database.ExecuteNonQuery("ALTER TABLE [SMS].[Scheduler] ADD CONSTRAINT [DF_Scheduler_SchedulerId] DEFAULT (newsequentialid()) FOR [SchedulerId]");
			Database.ExecuteNonQuery("ALTER TABLE [SMS].[SchedulerConfig] ADD CONSTRAINT [DF_Scheduler_SchedulerConfigId] DEFAULT (newsequentialid()) FOR [SchedulerConfigId]");
			Database.ExecuteNonQuery("ALTER TABLE [SMS].[SchedulerIcon] ADD CONSTRAINT [DF_Scheduler_SchedulerIconId] DEFAULT (newsequentialid()) FOR [SchedulerIconId]");
		}
	}
}