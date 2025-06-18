namespace Crm.Project.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130816122600)]
	public class CheckPermissions : Migration
	{
		public override void Up()
		{
			var sb = new StringBuilder();

			sb.AppendLine("IF NOT EXISTS(SELECT * FROM [Crm].[Permission] WHERE Name = 'CreateProject') BEGIN INSERT INTO [Crm].[Permission] (Name,PluginName) VALUES ('CreateProject','Crm.Project') END");
			sb.AppendLine("IF NOT EXISTS(SELECT * FROM [Crm].[Permission] WHERE Name = 'EditProject') BEGIN INSERT INTO [Crm].[Permission] (Name,PluginName) VALUES ('EditProject','Crm.Project') END");
			sb.AppendLine("IF NOT EXISTS(SELECT * FROM [Crm].[Permission] WHERE Name = 'DeleteProject') BEGIN INSERT INTO [Crm].[Permission] (Name,PluginName) VALUES ('DeleteProject','Crm.Project') END");
			sb.AppendLine("IF NOT EXISTS(SELECT * FROM [Crm].[Permission] WHERE Name = 'ChangeFinalizedProjectStatus') BEGIN INSERT INTO [Crm].[Permission] (Name,PluginName) VALUES ('ChangeFinalizedProjectStatus','Crm.Project') END");

			Database.ExecuteNonQuery(sb.ToString());
		}

		public override void Down()
		{
			
		}

	}
}