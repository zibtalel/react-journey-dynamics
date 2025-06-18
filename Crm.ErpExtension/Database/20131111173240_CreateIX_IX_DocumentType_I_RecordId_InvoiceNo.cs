namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131111173240)]
	public class CreateIX_IX_DocumentType_I_RecordId_InvoiceNo : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[ERPDocument]') AND name = N'IX_DocumentType_I_RecordId_InvoiceNo') " +
															 "BEGIN " +
															 "CREATE NONCLUSTERED INDEX IX_DocumentType_I_RecordId_InvoiceNo ON [CRM].[ERPDocument] ([DocumentType]) INCLUDE ([RecordId],[InvoiceNo]) " +
															 "END");
		}
	}
}