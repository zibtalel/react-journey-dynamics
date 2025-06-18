namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130814111000)]
	public class AddColumnQuantityShippedToErpDocuments : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "QuantityShipped"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", "QuantityShipped", DbType.Int32, ColumnProperty.Null);
			}
		}
		public override void Down()
		{
		}
	}
}