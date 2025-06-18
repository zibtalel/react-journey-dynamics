namespace Crm.Project.Database
{
	using System.Data;
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20220331150600)]
	public class AddCompetitorToCompanyType : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("LU.CompanyType") && !Database.ColumnExists("LU.CompanyType", "Competitor"))
			{
				Database.AddColumn("LU.CompanyType", new Column("Competitor", DbType.Boolean, ColumnProperty.NotNull, false));
				Database.ExecuteNonQuery("UPDATE [LU].[CompanyType] SET [Competitor] = 1 WHERE [Value] IN (SELECT [VALUE] FROM [LU].[CompanyType] WHERE [Name] = 'Competitor')");
			}
		}
	}
}
