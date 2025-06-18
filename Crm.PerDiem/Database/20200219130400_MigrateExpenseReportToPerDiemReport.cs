namespace Crm.PerDiem.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20200219130400)]
	public class MigrateExpenseReportToPerDiemReport : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("[CRM].[PerDiemReport]") && Database.TableExists("[SMS].[ExpenseReport]"))
			{
				Database.ExecuteNonQuery(
					"INSERT INTO [CRM].[PerDiemReport] " +
					"([PerDiemReportId], [Status], [From], [To], [CreateUser], [ModifyUser], [CreateDate], [IsActive]) " +
					"SELECT " +
					"[Id], [Status], [From], [To], [CreateUser], [ModifyUser], [CreateDate], [IsActive] " +
					"FROM [SMS].[ExpenseReport] WHERE [Status] IS NOT NULL");

				Database.AddColumn("[SMS].[Expense]", new Column("PerDiemReportId", DbType.Guid, ColumnProperty.Null));
				Database.AddForeignKey("FK_Expense_PerDiemReport", "SMS.Expense", "PerDiemReportId", "CRM.PerDiemReport", "PerDiemReportId");
				Database.ExecuteNonQuery("UPDATE [SMS].[Expense] SET [PerDiemReportId] = [ExpenseReportId] WHERE [ExpenseReportId] IS NOT NULL");
				Database.RemoveForeignKeyIfExisting("SMS", "Expense", "FK_Expense_ExpenseReport");
				Database.RemoveColumn("[SMS].[Expense]", "ExpenseReportId");
			}
		}
	}
}