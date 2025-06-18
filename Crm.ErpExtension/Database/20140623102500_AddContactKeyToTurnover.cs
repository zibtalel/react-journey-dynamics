namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20140623102500)]
	public class AddContactKeyToTurnover : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[Turnover]", "ContactKey"))
			{
				Database.AddColumn("[CRM].[Turnover]", new Column("ContactKey", DbType.Int32, ColumnProperty.Null));
			}
		}
		public override void Down()
		{
		}
	}
}