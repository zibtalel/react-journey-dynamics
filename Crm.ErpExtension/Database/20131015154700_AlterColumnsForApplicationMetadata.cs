namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131015154700)]
	public class AlterColumnsForApplicationMetadata : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("Crm.ErpDocument", "ApplicationId"))
				Database.ExecuteNonQuery("ALTER TABLE Crm.ErpDocument ADD ApplicationId NVARCHAR(50) NULL");

			if (!Database.ColumnExists("Crm.ErpDocument", "FilterCondition"))
				Database.ExecuteNonQuery("ALTER TABLE Crm.ErpDocument ADD FilterCondition NVARCHAR(50) NULL");

			if (!Database.ColumnExists("Crm.ErpDocument", "FilterElement"))
				Database.ExecuteNonQuery("ALTER TABLE Crm.ErpDocument ADD FilterElement NVARCHAR(50) NULL");
		}
		public override void Down()
		{
		}
	}
}