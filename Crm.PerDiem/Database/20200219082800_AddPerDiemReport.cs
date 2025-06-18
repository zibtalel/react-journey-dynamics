namespace Crm.PerDiem.Database
{
	using System;
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;
	using Crm.PerDiem.Model;

	[Migration(20200219082800)]
	public class AddPerDiemReport : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[PerDiemReport]"))
			{
				Database.AddTable(
					"[CRM].[PerDiemReport]",
					new Column("PerDiemReportId", DbType.Guid, ColumnProperty.PrimaryKey, "NEWSEQUENTIALID()"),
					new Column("Status", DbType.String, ColumnProperty.NotNull),
					new Column("[From]", DbType.DateTime, ColumnProperty.NotNull),
					new Column("[To]", DbType.DateTime, ColumnProperty.NotNull),
					new Column("CreateUser", DbType.String, ColumnProperty.NotNull, "'Setup'"),
					new Column("ModifyUser", DbType.String, ColumnProperty.NotNull, "'Setup'"),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true));

				var helper = new UnicoreMigrationHelper(Database);
				helper.AddEntityTypeAndAuthDataColumnIfNeeded<PerDiemReport>("CRM", "PerDiemReport");

				Database.AddColumn("[SMS].[TimeEntry]", new Column("PerDiemReportId", DbType.Guid, ColumnProperty.Null));
				Database.AddForeignKey("FK_TimeEntry_PerDiemReport", "SMS.TimeEntry", "PerDiemReportId", "CRM.PerDiemReport", "PerDiemReportId");
			}

			if (!Database.TableExists("[LU].[PerDiemReportStatus]"))
			{
				Database.AddTable(
					"[LU].[PerDiemReportStatus]",
					new Column("PerDiemReportStatusId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
					new Column("Value", DbType.String, ColumnProperty.Null),
					new Column("Name", DbType.String, Int32.MaxValue, ColumnProperty.NotNull),
					new Column("Language", DbType.String, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
					new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull, 0),
					new Column("SettableStatuses", DbType.String, ColumnProperty.Null),
					new Column("ShowInMobileClient", DbType.Boolean, ColumnProperty.NotNull, true),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("CreateUser", DbType.String, ColumnProperty.NotNull, "'Setup'"),
					new Column("ModifyUser", DbType.String, ColumnProperty.NotNull, "'Setup'"),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true)
				);
				Database.ExecuteNonQuery("INSERT INTO [LU].[PerDiemReportStatus] " +
				                         "([Value], [Name], [Language], [Favorite], [SortOrder], [SettableStatuses], [ShowInMobileClient], [IsActive]) " +
				                         "SELECT [Value], [Name], [Language], [Favorite], [SortOrder], [SettableStatuses], [ShowInMobileClient], [IsActive] FROM [LU].[ExpenseReportStatus] " +
				                         "UNION " +
				                         "SELECT [Value], [Name], [Language], [Favorite], [SortOrder], [SettableStatuses], [ShowInMobileClient], [IsActive] FROM [LU].[TimeEntryReportStatus]");

				Database.RemoveTableIfExisting("[LU].[ExpenseReportStatus]");
				Database.RemoveTableIfExisting("[LU].[TimeEntryReportStatus]");
			}

			if (!Database.TableExists("[LU].[PerDiemReportType]"))
			{
				Database.AddTable(
					"[LU].[PerDiemReportType]",
					new Column("PerDiemReportTypeId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
					new Column("Value", DbType.String, ColumnProperty.Null),
					new Column("Name", DbType.String, Int32.MaxValue, ColumnProperty.NotNull),
					new Column("Language", DbType.String, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
					new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull, 0),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("CreateUser", DbType.String, ColumnProperty.NotNull, "'Setup'"),
					new Column("ModifyUser", DbType.String, ColumnProperty.NotNull, "'Setup'"),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true)
				);
				Database.ExecuteNonQuery("INSERT INTO [LU].[PerDiemReportType] " +
				                         "([Value], [Name], [Language], [Favorite], [SortOrder], [IsActive]) " +
				                         "SELECT [Value], [Name], [Language], [Favorite], [SortOrder], [IsActive] FROM [LU].[ExpenseReportType] " +
				                         "UNION " +
				                         "SELECT [Value], [Name], [Language], [Favorite], [SortOrder], [IsActive] FROM [LU].[TimeEntryReportType]");

				Database.RemoveTableIfExisting("[LU].[ExpenseReportType]");
				Database.RemoveTableIfExisting("[LU].[TimeEntryReportType]");
			}
		}
	}
}