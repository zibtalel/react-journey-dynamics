namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20170217111300)]
	public class AddEntityColumnsToCrmErpDocument : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[CRM].[ErpDocument]", new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"));
			Database.AddColumnIfNotExisting("[CRM].[ErpDocument]", new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"));
			Database.AddColumnIfNotExisting("[CRM].[ErpDocument]", new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull, "'Setup'"));
			Database.AddColumnIfNotExisting("[CRM].[ErpDocument]", new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull, "'Setup'"));
			Database.AddColumnIfNotExisting("[CRM].[ErpDocument]", new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true));
		}
	}
}