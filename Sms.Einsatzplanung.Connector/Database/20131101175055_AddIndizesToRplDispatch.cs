namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131101175055)]
	public class AddIndizesToRplDispatch : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("CREATE NONCLUSTERED INDEX [IX_DispatchOrderKey_Type_IsActive] ON [RPL].[Dispatch]" +
															 "(" +
															 "[DispatchOrderKey] ASC," +
															 "[Type] ASC," +
															 "[IsActive] ASC" +
															 ")" +
															 "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
			Database.ExecuteNonQuery("CREATE NONCLUSTERED INDEX [IX_Type_IsActive_ResourceKey_Stop_Start] ON [RPL].[Dispatch]" +
															 "(" +
															 "[Type] ASC," +
															 "[IsActive] ASC," +
															 "[ResourceKey] ASC," +
															 "[Stop] ASC," +
															 "[Start] ASC" +
															 ")" +
															 "WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
		}
		public override void Down()
		{
		}
	}
}