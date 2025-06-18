namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200617153200)]
	public class AddPKToRplDispatchTracking : Migration
	{
		public override void Up()
		{
			var query =
				@"IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE [type] = 'PK' AND [parent_object_id] = OBJECT_ID('RPL.DispatchTracking'))
				BEGIN
					ALTER TABLE [RPL].[DispatchTracking] ADD  CONSTRAINT [PK_RPLDispatchTracking] PRIMARY KEY CLUSTERED
					([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
				END;";

			Database.ExecuteNonQuery(query);
		}
	}
}