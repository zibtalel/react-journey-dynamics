namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131127165600)]
	public class AddColumnRemainingQuantityToErpDocument : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "RemainingQuantity"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", "RemainingQuantity", DbType.Double, ColumnProperty.Null);
			}
		}
		public override void Down()
		{
		}
	}
}