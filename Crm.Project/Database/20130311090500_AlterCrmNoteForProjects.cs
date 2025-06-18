namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130311090500)]
	public class AlterCrmNoteForProjects : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("UPDATE [CRM].[Note] SET Plugin = 'Crm.Project' WHERE NoteType IN ('ProjectCreated', 'ProjectLost', 'ProjectStatusChanged')");
		}
		public override void Down()
		{
			Database.ExecuteNonQuery("UPDATE [CRM].[Note] SET Plugin = NULL WHERE Plugin = 'Crm.Project'");
		}
	}
}