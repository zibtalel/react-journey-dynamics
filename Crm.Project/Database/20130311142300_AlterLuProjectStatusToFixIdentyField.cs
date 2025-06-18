namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130311142300)]
	public class AlterLuProjectStatusToFixIdentyField : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("[LU].[ProjectStatus]", "ProjectCategoriyId"))
			{
				Database.ExecuteNonQuery("EXEC sp_rename @objname = '[LU].[ProjectStatus].ProjectCategoriyId', @newname = 'ProjectStatusId', @objtype = 'COLUMN'");
			}
		}
		public override void Down()
		{
			if (Database.ColumnExists("[LU].[ProjectStatus]", "ProjectStatusId"))
			{
				Database.ExecuteNonQuery("EXEC sp_rename @objname = '[LU].[ProjectStatus].ProjectStatusId', @newname = 'ProjectCategoriyId', @objtype = 'COLUMN'");
			}
		}
	}
}