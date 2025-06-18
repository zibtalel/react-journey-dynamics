namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20141119131700)]
	public class AddUniqueConstraintToProjectNo : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
				IF EXISTS (SELECT * FROM sys.indexes WHERE NAME= 'UC_Project_ProjectNo')
				BEGIN
					DROP INDEX [UC_Project_ProjectNo] ON [CRM].[Project]
				END
					CREATE UNIQUE NONCLUSTERED INDEX UC_Project_ProjectNo ON CRM.Project(ProjectNo)
					WHERE ([ProjectNo] IS NOT NULL)");
		}
	}
}