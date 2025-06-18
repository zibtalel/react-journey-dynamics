namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20161230172200)]
	public class AddEntityColumnsToLuProjectLostReasonCategory : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("LU.ProjectLostReasonCategory", new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"));
			Database.AddColumnIfNotExisting("LU.ProjectLostReasonCategory", new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"));
			Database.AddColumnIfNotExisting("LU.ProjectLostReasonCategory", new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull, "'Setup'"));
			Database.AddColumnIfNotExisting("LU.ProjectLostReasonCategory", new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull, "'Setup'"));
			Database.AddColumnIfNotExisting("LU.ProjectLostReasonCategory", new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true));
		}
	}
}