namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;
	using Crm.Project.Model;

	[Migration(20211123142800)]
	public class AddDocumentEntryEntityType : Migration
	{
		public override void Up()
		{
			var helper = new UnicoreMigrationHelper(Database);
			helper.AddOrUpdateEntityAuthDataColumn<DocumentEntry>("CRM", "DocumentEntry", "Id");
		}
	}
}
