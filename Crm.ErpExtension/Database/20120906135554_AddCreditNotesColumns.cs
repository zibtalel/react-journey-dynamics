namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20120906135554)]
	public class AddCreditNotesColumns : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "CreditNoteNo"))
			{
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[ERPDocument] ADD CreditNoteNo [nvarchar](50) NULL");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "CreditNoteDate"))
			{
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[ERPDocument] ADD CreditNoteDate [datetime] NULL");
			}
		}
		public override void Down()
		{
			if (Database.ColumnExists("[CRM].[ERPDocument]", "CreditNoteNo"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "CreditNoteNo");
			}
			if (Database.ColumnExists("[CRM].[ERPDocument]", "CreditNoteDate"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "CreditNoteDate");
			}
		}
	}
}