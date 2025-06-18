namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131106171115)]
	public class AddIndexIXLegacyIdToRplDispatch : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[RPL].[Dispatch]') AND name = N'IX_LegacyId') " +
			                         "BEGIN " +
															 "CREATE NONCLUSTERED INDEX [IX_LegacyId] ON [RPL].[Dispatch] " +
			                         "([LegacyId] ASC) " +
			                         "END");
		}

		public override void Down()
		{
		}
	}
}