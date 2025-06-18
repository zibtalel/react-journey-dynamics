namespace Crm.Project.Database
{
	using System.Collections.Generic;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130311091000)]
	public class AlterCrmPermissionForProjects : Migration
	{
		public override void Up()
		{
			var permissions = new List<string> { "CreateProject", "EditProject", "DeleteProject", "ChangeFinalizedProjectStatus" };
			foreach (string permission in permissions)
			{
				Database.ExecuteNonQuery("IF EXISTS(SELECT * FROM [CRM].[Permission] WHERE Name = '" + permission + "') UPDATE [CRM].[Permission] SET PluginName = 'Crm.Project' WHERE Name = '" + permission + "' ELSE INSERT INTO [CRM].[Permission] (Name, PluginName) VALUES ('" + permission + "', 'Crm.Project')");
			}
		}
		public override void Down()
		{
			Database.ExecuteNonQuery("UPDATE [CRM].[Permission] SET PluginName = NULL WHERE PluginName = 'Crm.Project'");
		}
	}
}