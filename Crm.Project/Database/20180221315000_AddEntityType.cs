namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;
	using Crm.Project.Model;
	using Crm.Project.Model.Notes;
	using Crm.Project.Model.Relationships;

	[Migration(20180221315000)]
	public class AddEntityType : Migration
	{
		public override void Up()
		{
			var helper = new UnicoreMigrationHelper(Database);
			helper.AddEntityTypeAndAuthDataColumnIfNeeded<ProjectCreatedNote>("CRM", "Note");
			helper.AddEntityType<ProjectLostNote>();
			helper.AddEntityType<ProjectStatusChangedNote>();
			helper.AddEntityTypeAndAuthDataColumnIfNeeded<ProjectContactRelationship>("CRM", "ProjectContactRelationship");
			helper.AddEntityTypeAndAuthDataColumnIfNeeded<Project>("CRM", "Contact");
		}
	}
}