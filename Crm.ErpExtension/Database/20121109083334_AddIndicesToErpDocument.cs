namespace Crm.ErpExtension.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20121109083334)]
	public class AddIndicesToErpDocument : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[ERPDocument]"))
			{
				return;
			}

			var stringBuilder = new StringBuilder();

			stringBuilder.AppendLine("IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[ERPDocument]') AND name = N'IX_CompanyNo_RecordType_CreditNoteDate')");
			stringBuilder.AppendLine("DROP INDEX [IX_CompanyNo_RecordType_CreditNoteDate] ON [CRM].[ERPDocument] WITH ( ONLINE = OFF )");
			stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_CreditNoteDate] ON [CRM].[ERPDocument] ([CompanyNo] ASC,	[RecordType] ASC,	[CreditNoteDate] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");

			stringBuilder.AppendLine("IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[ERPDocument]') AND name = N'IX_CompanyNo_RecordType_DeliveryNoteDate')");
			stringBuilder.AppendLine("DROP INDEX [IX_CompanyNo_RecordType_DeliveryNoteDate] ON [CRM].[ERPDocument] WITH ( ONLINE = OFF )");
			stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_DeliveryNoteDate] ON [CRM].[ERPDocument] ([CompanyNo] ASC,	[RecordType] ASC,	[DeliveryNoteDate] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");

			stringBuilder.AppendLine("IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[ERPDocument]') AND name = N'IX_CompanyNo_RecordType_[InvoiceDate')");
			stringBuilder.AppendLine("DROP INDEX [IX_CompanyNo_RecordType_[InvoiceDate] ON [CRM].[ERPDocument] WITH ( ONLINE = OFF )");
			stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_[InvoiceDate] ON [CRM].[ERPDocument] ([CompanyNo] ASC,	[RecordType] ASC,	[InvoiceDate] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");

			stringBuilder.AppendLine("IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[ERPDocument]') AND name = N'IX_CompanyNo_RecordType_OrderConfirmationDate')");
			stringBuilder.AppendLine("DROP INDEX [IX_CompanyNo_RecordType_OrderConfirmationDate] ON [CRM].[ERPDocument] WITH ( ONLINE = OFF )");
			stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_OrderConfirmationDate] ON [CRM].[ERPDocument] ([CompanyNo] ASC,	[RecordType] ASC,	[OrderConfirmationDate] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");

			stringBuilder.AppendLine("IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[CRM].[ERPDocument]') AND name = N'IX_CompanyNo_RecordType_QuoteDate')");
			stringBuilder.AppendLine("DROP INDEX [IX_CompanyNo_RecordType_QuoteDate] ON [CRM].[ERPDocument] WITH ( ONLINE = OFF )");
			stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_QuoteDate] ON [CRM].[ERPDocument] ([CompanyNo] ASC,	[RecordType] ASC,	[QuoteDate] ASC) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]");

			Database.ExecuteNonQuery(stringBuilder.ToString());
		}
		public override void Down()
		{

		}
	}
}
