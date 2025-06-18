namespace Crm.ErpExtension.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131029130600)]
	public class _20131029130600_CreateIX_CompanyNoIndex : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[ERPDocument]"))
			{
				var stringBuilder = new StringBuilder();

				stringBuilder.AppendLine("if exists(SELECT 1 FROM sys.indexes WHERE name = 'IX_CompanyNo' AND object_id = OBJECT_ID('CRM.ERPDocument'))");
				stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX IX_CompanyNo ON [CRM].[ERPDocument] ([CompanyNo])");

				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
	}
}