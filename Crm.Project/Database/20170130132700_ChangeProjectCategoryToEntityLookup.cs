namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20170130132700)]
	public class ChangeProjectCategoryToEntityLookup : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("LU.ProjectCategory", new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"));
			Database.AddColumnIfNotExisting("LU.ProjectCategory", new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"));
			Database.AddColumnIfNotExisting("LU.ProjectCategory", new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull, "'Setup'"));
			Database.AddColumnIfNotExisting("LU.ProjectCategory", new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull, "'Setup'"));
			Database.AddColumnIfNotExisting("LU.ProjectCategory", new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true));
		}
	}
}