namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131024103400)]
	public class AddLegacyIdToCrmTurnover : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("Crm.Turnover", "LegacyId"))
				Database.ExecuteNonQuery("ALTER TABLE CRM.Turnover ADD LegacyId NVARCHAR(100)");
		}
		public override void Down()
		{
		}
	}
}