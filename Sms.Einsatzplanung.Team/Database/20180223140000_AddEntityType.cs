namespace Sms.Einsatzplanung.Team.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;

	using Sms.Einsatzplanung.Team.Model;

	[Migration(20180223140000)]
	public class AddEntityType : Migration
	{
		public override void Up()
		{
			var helper = new UnicoreMigrationHelper(Database);
			helper.AddEntityTypeAndAuthDataColumnIfNeeded<TeamDispatchUser>("SMS", "TeamDispatchUser");
		}
	}
}