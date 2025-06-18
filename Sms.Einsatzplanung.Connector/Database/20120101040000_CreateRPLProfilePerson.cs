namespace Sms.Einsatzplanung.Connector.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20120101040000)]
	public class CreateRPLProfilePerson : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[RPL].[ProfilePerson]"))
			{

				var stringBuilder = new StringBuilder();

				stringBuilder.AppendLine("CREATE TABLE [RPL].[ProfilePerson](");
				stringBuilder.AppendLine("[Id] [int] IDENTITY(1,1) NOT NULL,");
				stringBuilder.AppendLine("[ProfileKey] [int] NULL,");
				stringBuilder.AppendLine("[UserName] [nvarchar](256) NULL,");
				stringBuilder.AppendLine("PRIMARY KEY NONCLUSTERED ");
				stringBuilder.AppendLine("(");
				stringBuilder.AppendLine("[Id] ASC");
				stringBuilder.AppendLine(")WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");
				stringBuilder.AppendLine(") ON [PRIMARY]");

				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
		public override void Down()
		{

		}
	}
}
