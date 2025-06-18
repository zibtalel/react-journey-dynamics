namespace Crm.PerDiem.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20220126105600)]
	public class AlterReportResponsibleUserNotNull : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("ALTER TABLE [CRM].[PerDiemReport] ALTER COLUMN ResponsibleUser nvarchar(255) NOT NULL");
		}
	}
}