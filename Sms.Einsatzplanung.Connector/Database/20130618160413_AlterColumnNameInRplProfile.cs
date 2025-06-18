namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130618160413)]
	public class AlterColumnNameInRplProfile : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("RPL.Dispatch", "Name"))
			{
				Database.ExecuteNonQuery("ALTER TABLE RPL.Profile ALTER COLUMN Name nvarchar(400)");
			}
		}
		public override void Down()
		{
			
		}
	}
}
