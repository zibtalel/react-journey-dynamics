namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20232508035200)]
	public class ChangedContributionMarginColumnToDecimal : Migration
	{
		public override void Up()
		{
			var tableName = $"[CRM].Project";
			if (Database.TableExists($"{tableName}"))
			{
				Database.ExecuteNonQuery(@$"
							ALTER TABLE {tableName} DROP COLUMN WeightedContributionMargin
							ALTER TABLE {tableName} ALTER COLUMN ContributionMargin DECIMAL (19, 2); 
							ALTER TABLE {tableName}
							ADD WeightedContributionMargin AS ((coalesce([ContributionMargin],(0))*coalesce([Rating],(0)))*(0.2))
						");
			}
		}
	}
}
