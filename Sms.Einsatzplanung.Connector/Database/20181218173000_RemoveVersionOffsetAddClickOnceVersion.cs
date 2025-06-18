namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20181218173000)]
	public class RemoveVersionOffsetAddClickOnceVersion : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
				ALTER TABLE [SMS].[Scheduler] DROP COLUMN [VersionOffset]
				ALTER TABLE [SMS].[Scheduler] ADD [ClickOnceVersion] [INT] NOT NULL CONSTRAINT [DF_SchedulerClickOnceVersion] DEFAULT 0
				ALTER TABLE [SMS].[Scheduler] DROP CONSTRAINT [DF_SchedulerClickOnceVersion]
			");
			Database.ExecuteNonQuery(@"
				CREATE TABLE [SMS].[SchedulerClickOnceVersion]
				(
					[Id] [BIT] NOT NULL CONSTRAINT [DF_SchedulerClickOnceVersionId] DEFAULT '0',
					[Version] [INT] NOT NULL,
					[CreateDate] [DATETIME] NOT NULL,
					[ModifyDate] [DATETIME] NOT NULL,
					[CreateUser] [NVARCHAR](60) NOT NULL,
					[ModifyUser] [NVARCHAR](60) NOT NULL,
					CONSTRAINT [PK_SchedulerClickOnceVersion] PRIMARY KEY ([Id]),
					CONSTRAINT [CK_SchedulerClickOnceVersionId] CHECK ([Id] = '0')
				)
				INSERT [SMS].[SchedulerClickOnceVersion] ([Version], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser]) VALUES (0, GETUTCDATE(), GETUTCDATE(), 'system', 'system')
			");
		}
	}
}