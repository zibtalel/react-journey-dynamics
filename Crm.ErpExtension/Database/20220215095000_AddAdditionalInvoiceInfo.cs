namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20220215095000)]
	public class AddAdditionalInvoiceInfo : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("CRM.ERPDocument", "OutstandingBalance"))
			{
				Database.AddColumn("CRM.ERPDocument", "OutstandingBalance", DbType.Decimal, ColumnProperty.Null);
			}
			if (!Database.ColumnExists("CRM.ERPDocument", "DueDate"))
			{
				Database.AddColumn("CRM.ERPDocument", "DueDate", DbType.DateTime, ColumnProperty.Null);
			}
			if (!Database.ColumnExists("CRM.ERPDocument", "DunningLevel"))
			{
				Database.AddColumn("CRM.ERPDocument", "DunningLevel", DbType.Int16, ColumnProperty.Null);
			}
		}
	}
}