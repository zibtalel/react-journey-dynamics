namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160523140600)]
	public class AddIndexToCrmProject : Migration
	{
		public override void Up()
		{
			const string query = @"IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_Project_FolderKey')" +
													 "CREATE NONCLUSTERED INDEX [IX_Project_FolderKey] ON [CRM].[Project]([FolderKey] ASC)";

			Database.ExecuteNonQuery(query);
		}
	}
}