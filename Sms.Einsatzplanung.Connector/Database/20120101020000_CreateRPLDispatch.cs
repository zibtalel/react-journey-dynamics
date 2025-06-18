namespace Sms.Einsatzplanung.Connector.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20120101020000)]
	public class CreateRPLDispatch : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[RPL].[Dispatch]"))
			{

				var stringBuilder = new StringBuilder();

				stringBuilder.AppendLine("CREATE TABLE [RPL].[Dispatch](");
				stringBuilder.AppendLine("[Id] [int] IDENTITY(1,1) NOT NULL,");
				stringBuilder.AppendLine("[Type] [nvarchar](20) NOT NULL,");
				stringBuilder.AppendLine("[Start] [datetime] NOT NULL,");
				stringBuilder.AppendLine("[Stop] [datetime] NOT NULL,");
				stringBuilder.AppendLine("[Fix] [bit] NOT	NULL,");
				stringBuilder.AppendLine("[Person] [nvarchar](256) NOT NULL,");
				stringBuilder.AppendLine("[InternalInformation] [ntext] NULL,");
				stringBuilder.AppendLine("[DispatchOrderKey] [int] NULL,");
				stringBuilder.AppendLine("[DispatchReleased] [bit] NULL,");
				stringBuilder.AppendLine("[DispatchClosed] [bit] NULL,");
				stringBuilder.AppendLine("[DispatchTechnicianInformation] [ntext] NULL,");
				stringBuilder.AppendLine("[AbsenceTypeKey] [int] NULL,");
				stringBuilder.AppendLine("[Version] [int] NOT NULL,");
				stringBuilder.AppendLine("[SeriesInfo] [nvarchar](50) NULL,");
				stringBuilder.AppendLine("[LegacyId] [bigint] NULL,");
				stringBuilder.AppendLine("[CreateDate] [datetime] NOT NULL,");
				stringBuilder.AppendLine("[ModifyDate] [datetime] NOT NULL,");
				stringBuilder.AppendLine("[CreateUser] [nvarchar](60) NOT NULL,");
				stringBuilder.AppendLine("[ModifyUser] [nvarchar](60) NOT NULL");
				stringBuilder.AppendLine(") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]");

				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
		public override void Down()
		{

		}
	}
}
