namespace Crm.PerDiem.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20230530163400)]
	public class AddWorkingHoursPerDayToCrmUser : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[CRM].[User]", new Column("WorkingHoursPerDay", DbType.Int32, ColumnProperty.Null));
			Database.ExecuteNonQuery("ALTER TABLE [CRM].[User] ADD CONSTRAINT DF_WorkingHoursPerDay DEFAULT 8 FOR WorkingHoursPerDay;");
			Database.ExecuteNonQuery("UPDATE [CRM].[User] SET WorkingHoursPerDay = 8 WHERE WorkingHoursPerDay IS NULL;");
			Database.ExecuteNonQuery("ALTER TABLE [CRM].[User] ALTER COLUMN WorkingHoursPerDay int NOT NULL;");
		}
	}
}
