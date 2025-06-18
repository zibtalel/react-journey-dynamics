namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20140623101400)]
	public class AddContactKeyToErpDocument : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "ContactKey"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", new Column("ContactKey", DbType.Int32, ColumnProperty.Null));
			}
		}
		public override void Down()
		{
		}
	}
}