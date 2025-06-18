namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170224144200)]
	public class AddIndexForErpDocumentSync : Migration
	{
		public override void Up()
		{
			if ((int)Database.ExecuteScalar("SELECT COUNT(*) FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[ERPDocument]') AND name like 'IX_ERPDocument_DocumentType_ModifyDate'") == 0)
			{
				Database.ExecuteNonQuery("CREATE INDEX [IX_ERPDocument_DocumentType_ModifyDate] ON [CRM].[ERPDocument] ([DocumentType], [ModifyDate])");
			}
		}
	}
}