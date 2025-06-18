namespace Crm.ErpExtension.Database
{
	using System.Data;
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20140623090600)]
	public class AlterColumnRecordTypeOfErpDocument : Migration
	{
		public override void Up()
		{
			if (Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_[InvoiceDate"))
			{
				Database.ExecuteNonQuery("DROP INDEX [IX_CompanyNo_RecordType_[InvoiceDate] ON [CRM].[ERPDocument]");
			}
			if (Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_CreditNoteDate"))
			{
				Database.ExecuteNonQuery("DROP INDEX [IX_CompanyNo_RecordType_CreditNoteDate] ON [CRM].[ERPDocument]");
			}
			if (Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_DeliveryNoteDate"))
			{
				Database.ExecuteNonQuery("DROP INDEX [IX_CompanyNo_RecordType_DeliveryNoteDate] ON [CRM].[ERPDocument]");
			}
			if (Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_OrderConfirmationDate"))
			{
				Database.ExecuteNonQuery("DROP INDEX [IX_CompanyNo_RecordType_OrderConfirmationDate] ON [CRM].[ERPDocument]");
			}
			if (Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_QuoteDate"))
			{
				Database.ExecuteNonQuery("DROP INDEX [IX_CompanyNo_RecordType_QuoteDate] ON [CRM].[ERPDocument]");
			}

			if (Database.ColumnExists("[CRM].[ERPDocument]", "RecordType"))
			{
				Database.ChangeColumn("[CRM].[ERPDocument]", new Column("RecordType", DbType.String, 50, ColumnProperty.Null));
			}

			if (!Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_CreditNoteDate"))
			{
				var stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_CreditNoteDate] ON [CRM].[ERPDocument]");
				stringBuilder.AppendLine("(");
				stringBuilder.AppendLine("	[CompanyNo] ASC,");
				stringBuilder.AppendLine("	[RecordType] ASC,");
				stringBuilder.AppendLine("	[CreditNoteDate] ASC");
				stringBuilder.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
			if (!Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_DeliveryNoteDate"))
			{
				var stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_DeliveryNoteDate] ON [CRM].[ERPDocument]");
				stringBuilder.AppendLine("(");
				stringBuilder.AppendLine("	[CompanyNo] ASC,");
				stringBuilder.AppendLine("	[RecordType] ASC,");
				stringBuilder.AppendLine("	[DeliveryNoteDate] ASC");
				stringBuilder.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
			if (!Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_InvoiceDate"))
			{
				var stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_InvoiceDate] ON [CRM].[ERPDocument]");
				stringBuilder.AppendLine("(");
				stringBuilder.AppendLine("	[CompanyNo] ASC,");
				stringBuilder.AppendLine("	[RecordType] ASC,");
				stringBuilder.AppendLine("	[InvoiceDate] ASC");
				stringBuilder.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
			if (!Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_OrderConfirmationDate"))
			{
				var stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_OrderConfirmationDate] ON [CRM].[ERPDocument]");
				stringBuilder.AppendLine("(");
				stringBuilder.AppendLine("	[CompanyNo] ASC,");
				stringBuilder.AppendLine("	[RecordType] ASC,");
				stringBuilder.AppendLine("	[OrderConfirmationDate] ASC");
				stringBuilder.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
			if (!Database.IndexExists("[CRM].[ERPDocument]", "IX_CompanyNo_RecordType_QuoteDate"))
			{
				var stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("CREATE NONCLUSTERED INDEX [IX_CompanyNo_RecordType_QuoteDate] ON [CRM].[ERPDocument]");
				stringBuilder.AppendLine("(");
				stringBuilder.AppendLine("	[CompanyNo] ASC,");
				stringBuilder.AppendLine("	[RecordType] ASC,");
				stringBuilder.AppendLine("	[QuoteDate] ASC");
				stringBuilder.AppendLine(")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
		public override void Down()
		{
		}
	}
}