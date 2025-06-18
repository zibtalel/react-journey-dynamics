namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130311143000)]
	public class AlterLuProjectCategoryToFixIdentyField : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("[LU].[ProjectCategory]", "ProjectCategoriyId"))
			{
				Database.ExecuteNonQuery("EXEC sp_rename @objname = '[LU].[ProjectCategory].ProjectCategoriyId', @newname = 'ProjectCategoryId', @objtype = 'COLUMN'");
			}
		}
		public override void Down()
		{
			if (Database.ColumnExists("[LU].[ProjectCategory]", "ProjectCategoryId"))
			{
				Database.ExecuteNonQuery("EXEC sp_rename @objname = '[LU].[ProjectCategory].ProjectCategoryId', @newname = 'ProjectCategoriyId', @objtype = 'COLUMN'");
			}
		}
	}
}