namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20180820144400)]
	public class ChangeNewIdToNewSequentialId : Migration
	{
		public override void Up()
		{
			Database.DropDefault("CRM", "ProjectContactRelationship", "ProjectContactRelationshipId");
			Database.ExecuteNonQuery("ALTER TABLE [CRM].[ProjectContactRelationship] ADD CONSTRAINT [DF_ProjectContactRelationship_ProjectContactRelationshipId] DEFAULT (newsequentialid()) FOR [ProjectContactRelationshipId]");
		}
	}
}