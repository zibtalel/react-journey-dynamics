namespace Crm.MarketInsight.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200915151100)]
	public class AddTableLuMarketInsightContactRelationshipType : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[LU].[MarketInsightContactRelationshipType]"))
			{
				Database.AddTable(
					"LU.MarketInsightContactRelationshipType",
					new Column("MarketInsightContactRelationshipTypeId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
					new Column("Value", DbType.String, 50, ColumnProperty.NotNull),
					new Column("Name", DbType.String, ColumnProperty.NotNull),
					new Column("Language", DbType.String, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull),
					new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull),
					new Column("CreateUser", DbType.String, ColumnProperty.NotNull),
					new Column("ModifyUser", DbType.String, ColumnProperty.NotNull),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull));
			}
		}
	}
}