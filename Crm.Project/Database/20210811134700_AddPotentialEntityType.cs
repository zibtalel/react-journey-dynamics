namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;
	using Crm.Project.Model;
	using Crm.Project.Model.Relationships;

	[Migration(20210811134700)]
	public class AddPotentialEntityType : Migration
	{
		public override void Up()
		{
			var helper = new UnicoreMigrationHelper(Database);
			helper.AddOrUpdateEntityAuthDataColumn<Potential>("CRM", "Contact");
			helper.AddOrUpdateEntityAuthDataColumn<PotentialContactRelationship>("CRM", "PotentialContactRelationship");
		}
	}
}