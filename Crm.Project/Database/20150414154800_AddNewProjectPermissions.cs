namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20150414154800)]
	public class AddNewProjectPermissions : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(
				"IF NOT EXISTS(SELECT * FROM [Crm].[Permission] WHERE Name = 'ChangeFinalizedProjectStatus' AND PGroup = 'Project')" +
				"BEGIN INSERT INTO [Crm].[Permission] (Name,PluginName,Status,PGroup) VALUES ('ChangeFinalizedProjectStatus', 'Crm.Project', 1, 'Project') END " 
				);
		}
	}
}