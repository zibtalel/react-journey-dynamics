namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20210419180000)]
	public class PublicHolidayRegion : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("ALTER TABLE CRM.[User] ADD PublicHolidayRegionKey NVARCHAR(20) NULL");
		}
	}
}
