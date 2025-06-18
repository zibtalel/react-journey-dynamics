namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20140123142700)]
	public class ChangeProjectValueColumnToDecimal : Migration
	{
		public override void Up()
		{
			Database.RemoveColumn("CRM.Project", "WeightedValue");
			Database.ChangeColumn("CRM.Project", new Column("Value", DbType.Decimal, 2, ColumnProperty.Null));
			Database.ExecuteNonQuery("ALTER TABLE CRM.Project ADD [WeightedValue] AS ((COALESCE([Value],0)*COALESCE([Rating],(0)))*(0.2))");
		}
	}
}