namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20230217130000)]
	public class MoveInvoiceValuesToCorrectColumn : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
				UPDATE [CRM].[ERPDocument] 
				SET CreditNoteNo = InvoiceNo, 
				InvoiceNo = NULL, 
				ModifyDate = GETUTCDATE(), 
				ModifyUser = 'Migration_20230217130000' 
				WHERE DocumentType = 'CreditNote' 
				AND CreditNoteNo IS NULL 
				");

			Database.ExecuteNonQuery(@"
				UPDATE [CRM].[ERPDocument] 
				SET CreditNoteDate = InvoiceDate, 
				InvoiceDate = NULL, 
				ModifyDate = GETUTCDATE(), 
				ModifyUser = 'Migration_20230217130000' 
				WHERE DocumentType = 'CreditNote' 
				AND CreditNoteDate IS NULL 
				");
		}
	}
}