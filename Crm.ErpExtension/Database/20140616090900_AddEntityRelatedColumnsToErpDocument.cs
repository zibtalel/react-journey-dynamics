namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20140616090900)]
	public class AddEntityRelatedColumnsToErpDocument : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "CreateDate"))
			{
				Database.AddColumnWithDefaultValue("[CRM].[ERPDocument]", "CreateDate", "DateTime", "GETDATE()");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "ModifyDate"))
			{
				Database.AddColumnWithDefaultValue("[CRM].[ERPDocument]", "ModifyDate", "DateTime", "GETDATE()");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "CreateUser"))
			{
				Database.AddColumnWithDefaultValue("[CRM].[ERPDocument]", "CreateUser", "NVARCHAR(256)", "''");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "ModifyUser"))
			{
				Database.AddColumnWithDefaultValue("[CRM].[ERPDocument]", "ModifyUser", "NVARCHAR(256)", "''");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "IsActive"))
			{
				Database.AddColumnWithDefaultValue("[CRM].[ERPDocument]", "IsActive", "BIT", "1");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "TenantKey"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", new Column("TenantKey", DbType.Int32, ColumnProperty.Null));
			}
		}
		public override void Down()
		{
		}
	}
}