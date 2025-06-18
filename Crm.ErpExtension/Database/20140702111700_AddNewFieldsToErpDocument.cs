namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20140702111700)]
	public class AddNewFieldsToErpDocument : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "PositionNo"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", new Column("PositionNo", DbType.Int32, ColumnProperty.Null));
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "ReferenceB"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", new Column("ReferenceB", DbType.String, 50, ColumnProperty.Null));
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "DeliveryTrackingUrl"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", new Column("DeliveryTrackingUrl", DbType.String, 250, ColumnProperty.Null));
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "Remark"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", new Column("Remark", DbType.String, 4000, ColumnProperty.Null));
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "LegacyVersion"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", new Column("LegacyVersion", DbType.Int64, ColumnProperty.Null));
			}
			if (Database.ColumnExists("[CRM].[ERPDocument]", "Description"))
			{
				Database.ChangeColumn("[CRM].[ERPDocument]", new Column("Description", DbType.String, 250, ColumnProperty.Null));
			}
		}
		public override void Down()
		{
		}
	}
}