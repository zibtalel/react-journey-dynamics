namespace Crm.PerDiem.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20220127124500)]
	public class AddResponsibleUsersToReports : Migration
	{
		public override void Up()
		{
			var sql = "UPDATE [CRM].[PerDiemReport] SET [ResponsibleUser] = [CreateUser] WHERE [ResponsibleUser] IS NULL";
			Database.ExecuteNonQuery(sql);
		}
	}
}