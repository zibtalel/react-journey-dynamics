namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20140618144000)]
	public class CreateTableDispatchTracking : Migration
	{
		public override void Up()
		{

			if (!Database.TableExists("[RPL].[DispatchTracking]"))
			{
				Database.ExecuteNonQuery(@"
					CREATE TABLE [RPL].[DispatchTracking](
						[Id] [int] IDENTITY(1,1) NOT NULL,
						[DispatchId] [int] NULL,
						[Start] [datetime] NULL,
						[Stop] [datetime] NULL,
						[Username] [nvarchar](120) NULL,
						[ModifyDate] [datetime] NULL,
						[ResourceName] [nvarchar](120) NULL
					)
				");
			}
		}

		public override void Down()
		{
			// nothing
		}
	}
}