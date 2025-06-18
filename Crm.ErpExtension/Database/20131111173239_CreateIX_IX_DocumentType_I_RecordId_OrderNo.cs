namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131111173239)]
	public class CreateIX_IX_DocumentType_I_RecordId_OrderNo : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[ERPDocument]') AND name = N'IX_DocumentType_I_RecordId_OrderNo') " +
															 "BEGIN " +
															 "CREATE NONCLUSTERED INDEX IX_DocumentType_I_RecordId_OrderNo ON [CRM].[ERPDocument] ([DocumentType]) INCLUDE ([RecordId],[OrderNo]) " +
															 "END");
		}
	}
}