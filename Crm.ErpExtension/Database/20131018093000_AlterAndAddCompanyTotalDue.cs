namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131018093000)]
	public class AlterAndAddCompanyTotalDue : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("Crm.Company", "ErpOpenItemsDue"))
				Database.ExecuteNonQuery("ALTER TABLE CRM.Company ADD ErpOpenItemsDue decimal(18,2)");
		}
		public override void Down()
		{
		}
	}
}