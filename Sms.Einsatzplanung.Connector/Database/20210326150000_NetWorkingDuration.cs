namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20210326150000)]
	public class NetWorkingDuration : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("ALTER TABLE RPL.Dispatch ADD NetWorkingPlanningEnabled BIT CONSTRAINT DF_NetWorkingPlanningEnabled DEFAULT 0 NOT NULL");
			Database.ExecuteNonQuery("ALTER TABLE RPL.Dispatch ADD NetWorkingDurationMinutes INT CONSTRAINT DF_NetWorkingDurationMinutes DEFAULT 0 NOT NULL");
			Database.ExecuteNonQuery("ALTER TABLE RPL.Dispatch ADD SpecialNetWorkingDays INT CONSTRAINT DF_SpecialNetWorkingDays DEFAULT 0 NOT NULL");
		}
	}
}
