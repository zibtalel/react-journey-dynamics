namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20150413140500)]
	public class AddFolderFkToCrmProject : Migration
	{
		public override void Up()
		{
			if ((int)Database.ExecuteScalar("SELECT COUNT(*) FROM sys.foreign_keys WHERE name = 'FK_Project_Folder'") == 0)
			{
				Database.ExecuteNonQuery("UPDATE p SET p.[FolderKey] = NULL FROM [CRM].[Project] p LEFT OUTER JOIN [CRM].[Contact] c ON p.[FolderKey] = c.[ContactId] WHERE c.[ContactId] IS NULL");
				Database.AddForeignKey("FK_Project_Folder", "[CRM].[Project]", "FolderKey", "[CRM].[Contact]", "ContactId", ForeignKeyConstraint.SetNull);
			}
		}
	}
}