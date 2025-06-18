namespace Crm.PerDiem.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20230612170000)]
	public class RemoveEmptyPerDiemReports : Migration
	{
		public override void Up()
		{
			StringBuilder query = new StringBuilder();
			bool hasAllowanceTable = Database.TableExists("[CRM].[PerDiemAllowanceEntry]");
			bool hasTimePostingTable = Database.TableExists("[SMS].[ServiceOrderTimePostings]");

			query.AppendLine("UPDATE [CRM].[PerDiemReport]");
			query.AppendLine("SET IsActive = 0, ModifyUser = 'Migration_20230612170000'");
			query.AppendLine("WHERE PerDiemReportId NOT IN (SELECT Distinct [PerDiemReportId] FROM [SMS].[Expense] WHERE [PerDiemReportId] IS NOT NULL");
			query.AppendLine("UNION");
			query.AppendLine("SELECT DISTINCT [PerDiemReportId] FROM [SMS].[TimeEntry] WHERE [PerDiemReportId] IS NOT NULL");
			if (hasAllowanceTable)
			{
				query.AppendLine("UNION");
				query.AppendLine("SELECT DISTINCT [PerDiemReportId] FROM [CRM].[PerDiemAllowanceEntry] WHERE [PerDiemReportId] IS NOT NULL");
			}

			if (hasTimePostingTable)
			{
				query.AppendLine("UNION");
				query.AppendLine("SELECT DISTINCT [PerDiemReportId] FROM [SMS].[ServiceOrderTimePostings] WHERE [PerDiemReportId] IS NOT NULL");
			}

			query.Append(")");
			query.AppendLine("AND IsActive = 1");
			Database.ExecuteNonQuery(query.ToString());
		}
	}
}