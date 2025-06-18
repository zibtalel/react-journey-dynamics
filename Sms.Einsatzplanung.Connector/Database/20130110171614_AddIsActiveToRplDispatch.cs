namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130110171614)]
	public class AddIsActiveToRplDispatch : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("RPL.Dispatch", "IsActive"))
			{
				Database.ExecuteNonQuery("ALTER TABLE [RPL].Dispatch ADD IsActive BIT NULL");
				Database.ExecuteNonQuery("UPDATE RPL.Dispatch SET IsActive=1");
			}
		}
		public override void Down()
		{
			
		}
	}
}
