namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;
    using Crm.MarketInsight.Model.Relationships;
    using Crm.MarketInsight.Model;

	[Migration(20211123141000)]
	public class AddAuthDataToMarketInsightContactRelationShip : Migration
	{
		public override void Up()
		{
			var helper = new UnicoreMigrationHelper(Database);
			helper.AddOrUpdateEntityAuthDataColumn<MarketInsight>("CRM", "Contact");
			helper.AddOrUpdateEntityAuthDataColumn<MarketInsightContactRelationship>("CRM", "MarketInsightContactRelationship");
		}
	}
}