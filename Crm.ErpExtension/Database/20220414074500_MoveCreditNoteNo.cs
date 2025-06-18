namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20220414074500)]
	public class MoveCreditNoteNo : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("CreditNoteNo", DbType.String, 50, ColumnProperty.Null));
			Database.ExecuteNonQuery("Update [Crm].[ERPDocument] Set CreditNoteNo = InvoiceNo, InvoiceNo = null  WHERE DocumentType = 'CreditNote'");
		}
	}
}
