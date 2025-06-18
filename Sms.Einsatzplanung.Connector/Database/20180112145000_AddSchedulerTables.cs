namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20180112145000)]
	public class AddTableScheduler : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
				CREATE TABLE [SMS].[SchedulerConfig]
				(
					[SchedulerConfigId] [uniqueidentifier] NOT NULL,
					[IsActive] [bit] NOT NULL,
					[CreateDate] [datetime] NOT NULL,
					[ModifyDate] [datetime] NOT NULL,
					[CreateUser] [nvarchar](60) NOT NULL,
					[ModifyUser] [nvarchar](60) NOT NULL,
					[TenantKey] [int] NULL,
					[Config] [varbinary](max) NULL,
					CONSTRAINT [PK_SchedulerConfig] PRIMARY KEY CLUSTERED ([SchedulerConfigId]),
				)
				ALTER TABLE [SMS].[SchedulerConfig] ADD CONSTRAINT [DF_SchedulerConfig_IsActive] DEFAULT 1 FOR [IsActive]
				CREATE UNIQUE INDEX [IX_UQ_ActiveSchedulerConfig] ON [SMS].[SchedulerConfig] ([TenantKey], [IsActive]) WHERE [IsActive] = 1
			");
			Database.ExecuteNonQuery(@"
				CREATE TABLE [SMS].[SchedulerIcon]
				(
					[SchedulerIconId] [uniqueidentifier] NOT NULL,
					[IsActive] [bit] NOT NULL,
					[CreateDate] [datetime] NOT NULL,
					[ModifyDate] [datetime] NOT NULL,
					[CreateUser] [nvarchar](60) NOT NULL,
					[ModifyUser] [nvarchar](60) NOT NULL,
					[TenantKey] [int] NULL,
					[Icon] [varbinary](max) NULL,
					CONSTRAINT [PK_SchedulerIcon] PRIMARY KEY CLUSTERED ([SchedulerIconId]),
				)
				ALTER TABLE [SMS].[SchedulerIcon] ADD CONSTRAINT [DF_SchedulerIcon_IsActive] DEFAULT 1 FOR [IsActive]
				CREATE UNIQUE INDEX [IX_UQ_ActiveSchedulerIcon] ON [SMS].[SchedulerIcon] ([TenantKey], [IsActive]) WHERE [IsActive] = 1
			");
			Database.ExecuteNonQuery(@"
				CREATE TABLE [SMS].[Scheduler]
				(
					[SchedulerId] [uniqueidentifier] NOT NULL,
					[IsActive] [bit] NOT NULL,
					[CreateDate] [datetime] NOT NULL,
					[ModifyDate] [datetime] NOT NULL,
					[CreateUser] [nvarchar](60) NOT NULL,
					[ModifyUser] [nvarchar](60) NOT NULL,
					[TenantKey] [int] NULL,
					[Version] [nvarchar](max) NOT NULL,
					[VersionOffset] [nvarchar](max) NULL,
					[IsReleased] [bit] NOT NULL,
					[SchedulerIconKey] [uniqueidentifier] NULL,
					[SchedulerConfigKey] [uniqueidentifier] NULL,
					[Warnings] [nvarchar](max) NULL,
					CONSTRAINT [PK_Scheduler] PRIMARY KEY CLUSTERED ([SchedulerId]),
					CONSTRAINT [FK_Scheduler_SchedulerIcon] FOREIGN KEY ([SchedulerIconKey]) REFERENCES [SMS].[SchedulerIcon] ([SchedulerIconId]),
					CONSTRAINT [FK_Scheduler_SchedulerConfig] FOREIGN KEY ([SchedulerConfigKey]) REFERENCES [SMS].[SchedulerConfig] ([SchedulerConfigId]),
				)
				ALTER TABLE [SMS].[Scheduler] ADD CONSTRAINT [DF_Scheduler_IsActive] DEFAULT 1 FOR [IsActive]
				ALTER TABLE [SMS].[Scheduler] ADD CONSTRAINT [DF_Scheduler_IsReleased] DEFAULT 0 FOR [IsReleased]
				CREATE UNIQUE INDEX [IX_UQ_ReleasedScheduler] ON [SMS].[Scheduler] ([TenantKey], [IsReleased], [IsActive]) WHERE [IsReleased] = 1
			");
		}
	}
}