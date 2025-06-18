namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20220310155011)]
	public class UpdateMarketInisightStatusLookup : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("[LU].[MarketInsightStatus]"))
			{
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Lost/Offline' WHERE Value = 'lost' AND Language = 'en'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Unqualified' WHERE Value = 'unqualified' AND Language = 'en'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'At sales' WHERE Value = 'sales' AND Language = 'en'");


				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Elveszett/Nem elérheto' WHERE Value = 'lost' AND Language = 'hu'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Képzetlen' WHERE Value = 'unqualified' AND Language = 'hu'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Nem áll rendelkezésre' WHERE Value = 'notavailable' AND Language = 'hu'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Potenciálisan képzett' WHERE Value = 'qualified' AND Language = 'hu'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Az értékesítésben' WHERE Value = 'sales' AND Language = 'hu'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Vevo' WHERE Value = 'customer' AND Language = 'hu'");

				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Verloren/Abgeschalten' WHERE Value = 'lost' AND Language = 'de'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Unqualifiziert' WHERE Value = 'unqualified' AND Language = 'de'");
				Database.ExecuteNonQuery(@"UPDATE [LU].[MarketInsightStatus] SET Name = 'Im Vertrieb' WHERE Value = 'sales' AND Language = 'de'");
			}
		}
	}
}