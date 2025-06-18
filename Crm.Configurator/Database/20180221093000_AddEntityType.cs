namespace Crm.Configurator.Database
{
	using Crm.Configurator.Model;
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;

	[Migration(20180221093000)]
	public class AddEntityType : Migration
	{
		public override void Up()
		{
			var helper = new UnicoreMigrationHelper(Database);
			helper.AddEntityTypeAndAuthDataColumnIfNeeded<ConfigurationBase>("CRM", "Contact");
			helper.AddEntityTypeAndAuthDataColumnIfNeeded<ConfigurationRule>("CRM", "ConfigurationRule");
			helper.AddEntityTypeAndAuthDataColumnIfNeeded<Variable>("CRM", "Contact");
		}
		public override void Down()
		{
		}
	}
}