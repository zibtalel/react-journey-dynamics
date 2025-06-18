namespace Sms.Einsatzplanung.Connector.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20120101050000)]
	public class CreateRPLProfilePipelineFilter : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[RPL].[ProfilePipelineFilter]"))
			{

				var stringBuilder = new StringBuilder();

				stringBuilder.AppendLine("CREATE TABLE [RPL].[ProfilePipelineFilter](");
				stringBuilder.AppendLine("[Id] [int] IDENTITY(1,1) NOT NULL,");
				stringBuilder.AppendLine("[PropertyName] [nvarchar](50) NULL,");
				stringBuilder.AppendLine("[PropertyKey] [nvarchar](40) NULL,");
				stringBuilder.AppendLine("[ProfileKey] [int] NOT NULL");
				stringBuilder.AppendLine(") ON [PRIMARY]");

				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
		public override void Down()
		{

		}
	}
}
