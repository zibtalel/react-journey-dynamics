namespace Crm.ErpExtension.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20120101010000)]
	public class CreateCrmErpDocument : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[ERPDocument]"))
			{

				var stringBuilder = new StringBuilder();

				stringBuilder.AppendLine("CREATE TABLE [CRM].[ERPDocument] (");
				stringBuilder.AppendLine("DocumentType nvarchar(50) not null,");
				stringBuilder.AppendLine("DocumentState nvarchar(50) null,");
				stringBuilder.AppendLine("DocumentServiceType nvarchar(50) null,");
				stringBuilder.AppendLine("DocumentServiceState nvarchar(50) null,");
				stringBuilder.AppendLine("CompanyNo nvarchar(50) not null,");
				stringBuilder.AppendLine("RecordId int primary key,");
				stringBuilder.AppendLine("RecordType int not null,");
				stringBuilder.AppendLine("OrderNo nvarchar(50) not null,");
				stringBuilder.AppendLine("QuoteNo nvarchar(50) null,");
				stringBuilder.AppendLine("QuoteDate datetime null,");
				stringBuilder.AppendLine("RequestNo nvarchar(50) null,");
				stringBuilder.AppendLine("RequestDate datetime null,");
				stringBuilder.AppendLine("OrderConfirmationNo nvarchar(50) null,");
				stringBuilder.AppendLine("OrderConfirmationDate datetime null,");
				stringBuilder.AppendLine("DeliverNoteNo nvarchar(50) null,");
				stringBuilder.AppendLine("DeliveryNoteDate datetime null,");
				stringBuilder.AppendLine("InvoiceNo nvarchar(50) null,");
				stringBuilder.AppendLine("InvoiceDate datetime null,");
				stringBuilder.AppendLine("DocumentDate11 datetime null,");
				stringBuilder.AppendLine("ItemNo nvarchar(50) null,");
				stringBuilder.AppendLine("Total decimal(18, 0) null,");
				stringBuilder.AppendLine("[Total wo Taxes] decimal (18, 0) null,");
				stringBuilder.AppendLine("Currency nvarchar(50) null,");
				stringBuilder.AppendLine("[State] int null,");
				stringBuilder.AppendLine("Commission nvarchar(200) null,");
				stringBuilder.AppendLine("DueDate datetime null,");
				stringBuilder.AppendLine("DeviceNo nvarchar(50) null,");
				stringBuilder.AppendLine("Quantity int null,");
				stringBuilder.AppendLine("QuantityUnit nvarchar(10) null,");
				stringBuilder.AppendLine("[Description] nvarchar(50) null");
				stringBuilder.AppendLine(");");

				Database.ExecuteNonQuery(stringBuilder.ToString());
			}
		}
		public override void Down()
		{

		}
	}
}
