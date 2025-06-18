namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131016154500)]
	public class AlterAndAddCompanyErpInformations : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("Crm.Company", "ErpCreditLimit"))
				Database.ExecuteNonQuery("ALTER TABLE CRM.Company ALTER COLUMN ErpCreditLimit decimal(18,2)");

			if (Database.ColumnExists("Crm.Company", "ErpDiscount"))
				Database.ExecuteNonQuery("ALTER TABLE CRM.Company ALTER COLUMN ErpDiscount decimal(5,2)");

			if (!Database.ColumnExists("Crm.Company", "ErpOpenItemsTotal"))
				Database.ExecuteNonQuery("ALTER TABLE CRM.Company ADD ErpOpenItemsTotal decimal(18,2)");

			if (!Database.ColumnExists("Crm.Company", "ErpOutstandingOrderValue"))
				Database.ExecuteNonQuery("ALTER TABLE CRM.Company ADD ErpOutstandingOrderValue decimal(18,2)");

			if (!Database.ColumnExists("Crm.Company", "ErpOutstandingInvoiceValue"))
				Database.ExecuteNonQuery("ALTER TABLE CRM.Company ADD ErpOutstandingInvoiceValue decimal(18,2)");					
		}
		public override void Down()
		{
		}
	}
}