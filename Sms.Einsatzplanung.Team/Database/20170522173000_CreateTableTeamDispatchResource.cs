namespace Sms.Einsatzplanung.Team.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20170522173000)]
	public class CreateTableTeamDispatchResources : Migration
	{
		public override void Up()
		{

			if (!Database.TableExists("[RPL].[TeamDispatchResource]"))
			{
				Database.ExecuteNonQuery(@"
					CREATE TABLE [RPL].[TeamDispatchResource](
						[TeamDispatchResourceId] [uniqueidentifier] NOT NULL,
						[DispatchId] [int] NULL,
						[ResourceId] [nvarchar](120) NOT NULL,
						[IsMainResource] [bit] NOT NULL,
						[CreateDate] [datetime] NOT NULL,
						[CreateUser] [nvarchar](256) NULL,
						[ModifyDate] [datetime] NOT NULL,
						[ModifyUser] [nvarchar](256) NULL,
						CONSTRAINT [PK_ServiceOrderDispatch] PRIMARY KEY CLUSTERED ([TeamDispatchResourceId])
					)
				");
				Database.ExecuteNonQuery(@"ALTER TABLE [RPL].[TeamDispatchResource] ADD DEFAULT (newsequentialid()) FOR [TeamDispatchResourceId]");
				Database.ExecuteNonQuery(@"ALTER TABLE [RPL].[Dispatch] ADD CONSTRAINT [PK_RPL_Dispatch] PRIMARY KEY([Id])");
				Database.ExecuteNonQuery(@"ALTER TABLE [RPL].[TeamDispatchResource]	WITH CHECK ADD CONSTRAINT[FK_TeamDispatchResource_DispatchId] FOREIGN KEY([DispatchId]) REFERENCES [RPL].[Dispatch] ([Id])");
			}
			Database.AddColumnIfNotExisting("[RPL].[Dispatch]", new Column("TeamId", DbType.Int32, ColumnProperty.Null));
		}

		public override void Down()
		{
			// nothing
		}
	}
}