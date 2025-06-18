namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130902143500)]
	public class AlterColumnsForDecimalPlacesToErpDocument : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("[CRM].[ERPDocument]", "QuantityShipped"))
			{
				Database.ChangeColumn("[CRM].[ERPDocument]", new Column("QuantityShipped", DbType.Double, ColumnProperty.Null));
			}
			if (Database.ColumnExists("[CRM].[ERPDocument]", "Quantity"))
			{
				Database.ChangeColumn("[CRM].[ERPDocument]", new Column("Quantity", DbType.Double, ColumnProperty.Null));
			}
			if (Database.ColumnExists("[CRM].[ERPDocument]", "Total"))
			{
				Database.ChangeColumn("[CRM].[ERPDocument]", new Column("Total", DbType.Double, ColumnProperty.Null));
			}
			if (Database.ColumnExists("[CRM].[ERPDocument]", "[Total wo Taxes]"))
			{
				Database.ChangeColumn("[CRM].[ERPDocument]", new Column("[Total wo Taxes]", DbType.Double, ColumnProperty.Null));
			}
		}
		public override void Down()
		{
		}
	}
}