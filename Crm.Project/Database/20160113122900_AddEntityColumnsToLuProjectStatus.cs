namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20160113122900)]
	public class AddEntityColumnsToLuProjectStatus : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("LU.ProjectStatus", new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"));
			Database.AddColumnIfNotExisting("LU.ProjectStatus", new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"));
			Database.AddColumnIfNotExisting("LU.ProjectStatus", new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull, "'Setup'"));
			Database.AddColumnIfNotExisting("LU.ProjectStatus", new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull, "'Setup'"));
			Database.AddColumnIfNotExisting("LU.ProjectStatus", new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true));
		}
	}
}