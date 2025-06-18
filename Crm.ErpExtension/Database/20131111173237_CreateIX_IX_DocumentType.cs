namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131111173237)]
	public class CreateIX_IX_DocumentType : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[ERPDocument]') AND name = N'IX_DocumentType') " +
															 "BEGIN " +
															 "CREATE NONCLUSTERED INDEX IX_DocumentType ON [CRM].[ERPDocument] ([DocumentType]) " +
															 "END");
		}
	}
}