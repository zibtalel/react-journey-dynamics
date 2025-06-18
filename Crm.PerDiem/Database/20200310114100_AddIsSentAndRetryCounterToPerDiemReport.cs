namespace Crm.PerDiem.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20200310114100)]
	public class AddIsSentAndRetryCounterToPerDiemReport : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("CRM.PerDiemReport", new Column("IsSent", DbType.Boolean, ColumnProperty.NotNull, false));
			Database.AddColumnIfNotExisting("CRM.PerDiemReport", new Column("RetryCounter", DbType.Int32, ColumnProperty.NotNull, 0));
			Database.ExecuteNonQuery("UPDATE CRM.PerDiemReport SET IsSent = 1");
		}
	}
}