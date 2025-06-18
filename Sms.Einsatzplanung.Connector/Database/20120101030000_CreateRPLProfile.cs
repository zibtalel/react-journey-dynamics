namespace Sms.Einsatzplanung.Connector.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20120101030000)]
	public class CreateRPLProfile : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[RPL].[Profile]"))
			{

				var stringBuilder = new StringBuilder();

				stringBuilder.AppendLine("CREATE TABLE [RPL].[Profile](");
				stringBuilder.AppendLine("[Id] [int] IDENTITY(1,1) NOT NULL,");
				stringBuilder.AppendLine("[Username] [nvarchar](256) NULL,");
				stringBuilder.AppendLine("[Name] [nvarchar](40) NULL,");
				stringBuilder.AppendLine("[DefaultProfile] [bit] NOT NULL,");
				stringBuilder.AppendLine("[LowerBound] [decimal](18, 0) NOT NULL,");
				stringBuilder.AppendLine("[UpperBound] [decimal](18, 0) NOT NULL,");
				stringBuilder.AppendLine("[PipelineGroupStorage] nvarchar(4000) NULL,");
				stringBuilder.AppendLine("[PersonGroup] nvarchar(20) NULL,");
				stringBuilder.AppendLine("[PersonDisplayName] nvarchar(20) NULL,");
				stringBuilder.AppendLine("[PersonSortBy] nvarchar(20) NULL,");
				stringBuilder.AppendLine("[CreateDate] [datetime] NULL,");
				stringBuilder.AppendLine("[ModifyDate] [datetime] NULL,");
				stringBuilder.AppendLine("[CreateUser] [nvarchar](63) NULL,");
				stringBuilder.AppendLine("[ModifyUser] [nvarchar](63) NULL");
				stringBuilder.AppendLine(") ON [PRIMARY]");

				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
		public override void Down()
		{

		}
	}
}
