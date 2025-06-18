namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20141230095200)]
	public class AddMissingIndexToCrmTurnoverForContactKeyRecordType : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("[CRM].[Turnover]", "ContactKey") && Database.ColumnExists("[CRM].[Turnover]", "RecordType"))
			{
				Database.ExecuteNonQuery("IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[Turnover]') AND name = N'IX_ContactKey_RecordType') " +
															 "BEGIN " +
															 "CREATE NONCLUSTERED INDEX IX_ContactKey_RecordType " +
															 "ON [CRM].[Turnover] ([ContactKey],[RecordType]) " +
															 "END");
			}
		}

		public override void Down()
		{
		}
	}
}