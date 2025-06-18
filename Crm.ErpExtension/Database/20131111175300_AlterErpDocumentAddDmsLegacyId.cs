namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131111175300)]
	public class AlterErpDocumentAddDmsLegacyId : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("[CRM].[ERPDocument]") && !Database.ColumnExists("[CRM].[ERPDocument]", "DmsLegacyId"))
			{
				Database.ExecuteNonQuery("ALTER TABLE CRM.ERPDocument ADD DmsLegacyId NVARCHAR(100)");
			}
		}
	}
}