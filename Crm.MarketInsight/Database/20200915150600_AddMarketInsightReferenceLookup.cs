namespace Crm.MarketInsight.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200915150600)]
	public class AddMarketInsightReferenceLookup : Migration
	{
		public override void Up()
		{
			const string tableName = "[LU].[MarketInsightReference]";
			if (Database.TableExists(tableName))
			{
				return;
			}
			Database.AddTable(tableName,
				new Column("ReferenceOptionId", DbType.Int16, ColumnProperty.Identity),
				new Column("Name", DbType.String, 255, ColumnProperty.NotNull),
				new Column("Language", DbType.StringFixedLength, 2, ColumnProperty.NotNull),
				new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
				new Column("SortOrder", DbType.Int16, ColumnProperty.Null),
				new Column("Value", DbType.String, 255, ColumnProperty.NotNull),
				new Column("CreateDate", DbType.DateTime, ColumnProperty.Null),
				new Column("ModifyDate", DbType.DateTime, ColumnProperty.Null),
				new Column("CreateUser", DbType.String, ColumnProperty.NotNull),
				new Column("ModifyUser", DbType.String, ColumnProperty.NotNull),
				new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true)
			);
		}
	}
}