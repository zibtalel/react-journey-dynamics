namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20211210140500)]
	public class InsertGroupValuesToProjectStatus : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("[LU].[ProjectStatus]"))
			{
				Database.ExecuteNonQuery(@"UPDATE [LU].[ProjectStatus] SET Groups = 'Open' WHERE Value = '100'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[ProjectStatus] SET Groups = 'Won' WHERE Value = '101'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[ProjectStatus] SET Groups = 'Lost' WHERE Value = '102'");
			}
		}
	}
}
