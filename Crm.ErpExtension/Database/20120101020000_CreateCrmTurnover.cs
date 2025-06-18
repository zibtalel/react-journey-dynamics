namespace Crm.ErpExtension.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20120101020000)]
	public class CreateCrmTurnover : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[Turnover]"))
			{

				var stringBuilder = new StringBuilder();

				stringBuilder.AppendLine("CREATE TABLE [CRM].[Turnover](");
				stringBuilder.AppendLine("[EntryKey] [int] IDENTITY(1,1) NOT NULL,");
				stringBuilder.AppendLine("[RecordType] [nvarchar](20) NULL,");
				stringBuilder.AppendLine("[Datafield1] [nvarchar](20) NULL,");
				stringBuilder.AppendLine("[Datafield2] [nvarchar](20) NULL,");
				stringBuilder.AppendLine("[Datafield3] [nvarchar](50) NULL,");
				stringBuilder.AppendLine("[Datafield4] [nvarchar](50) NULL,");
				stringBuilder.AppendLine("[Month1] [float] NULL,");
				stringBuilder.AppendLine("[Month2] [float] NULL,");
				stringBuilder.AppendLine("[Month3] [float] NULL,");
				stringBuilder.AppendLine("[Month4] [float] NULL,");
				stringBuilder.AppendLine("[Month5] [float] NULL,");
				stringBuilder.AppendLine("[Month6] [float] NULL,");
				stringBuilder.AppendLine("[Month7] [float] NULL,");
				stringBuilder.AppendLine("[Month8] [float] NULL,");
				stringBuilder.AppendLine("[Month9] [float] NULL,");
				stringBuilder.AppendLine("[Month10] [float] NULL,");
				stringBuilder.AppendLine("[Month11] [float] NULL,");
				stringBuilder.AppendLine("[Month12] [float] NULL,");
				stringBuilder.AppendLine("[CurrentYear] [float] NULL,");
				stringBuilder.AppendLine("[PreviousYear] [float] NULL,");
				stringBuilder.AppendLine("[PrePreviousYear] [float] NULL,");
				stringBuilder.AppendLine("PRIMARY KEY CLUSTERED");
				stringBuilder.AppendLine("(");
				stringBuilder.AppendLine("[EntryKey] ASC");
				stringBuilder.AppendLine(")	");
				stringBuilder.AppendLine("WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");
				stringBuilder.AppendLine(") ON [PRIMARY]");

				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
		public override void Down()
		{

		}
	}
}
