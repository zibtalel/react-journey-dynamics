namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20211210135200)]
	public class AddColumnGroupToProjectStatus : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("LU.ProjectStatus") && !Database.ColumnExists("LU.ProjectStatus", "Groups")) 
			{
				Database.ExecuteNonQuery("ALTER TABLE LU.ProjectStatus ADD Groups NVARCHAR(250)");
			}
		}
	}
}
