namespace Sms.Einsatzplanung.Connector.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20120101010000)]
	public class CreateRPLSchema : Migration
	{
		public override void Up()
		{
			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("IF NOT EXISTS(select * from sys.schemas");
			stringBuilder.AppendLine("WHERE NAME = 'RPL')");
			stringBuilder.AppendLine("BEGIN");
			stringBuilder.AppendLine("EXECUTE sp_executesql N'CREATE SCHEMA [RPL] AUTHORIZATION [dbo]'");
			stringBuilder.AppendLine("END");

			Database.ExecuteNonQuery(stringBuilder.ToString());
		}
		public override void Down()
		{

		}
	}
}
