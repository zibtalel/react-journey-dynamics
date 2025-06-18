namespace Crm.PerDiem.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20200219132200)]
	public class MigrateTimeEntryReportToPerDiemReport : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("[CRM].[PerDiemReport]") && Database.TableExists("[SMS].[TimeEntryReport]"))
			{
				Database.ExecuteNonQuery(
					"INSERT INTO [CRM].[PerDiemReport] " +
					"([PerDiemReportId], [Status], [From], [To], [CreateUser], [ModifyUser], [CreateDate], [IsActive]) " +
					"SELECT " +
					"t.[Id], t.[Status], t.[From], t.[To], t.[CreateUser], t.[ModifyUser], t.[CreateDate], t.[IsActive] " +
					"FROM [SMS].[TimeEntryReport] t " +
					"LEFT OUTER JOIN [CRM].[PerDiemReport] p ON t.[Id] = p.[PerDiemReportId] WHERE p.[PerDiemReportId] IS NULL");

				Database.AddColumn("[SMS].[TimeEntry]", new Column("PerDiemReportId", DbType.Guid, ColumnProperty.Null));
				Database.AddForeignKey("FK_TimeEntry_PerDiemReport", "SMS.TimeEntry", "PerDiemReportId", "CRM.PerDiemReport", "PerDiemReportId");
				Database.ExecuteNonQuery("UPDATE [SMS].[TimeEntry] SET [PerDiemReportId] = [TimeEntryReportId] WHERE [TimeEntryReportId] IS NOT NULL");
				Database.RemoveForeignKeyIfExisting("SMS", "TimeEntry", "FK_TimeEntry_TimeEntryReport");
				Database.RemoveColumn("[SMS].[TimeEntry]", "TimeEntryReportId");
			}
		}
	}
}