namespace Crm.MarketInsight.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200915145500)]
	public class AddMarketInsightStatusLookup : Migration
	{
		public override void Up()
		{
			const string tableName = "[LU].[MarketInsightStatus]";
			if (Database.TableExists(tableName))
			{
				return;
			}
			Database.AddTable(tableName,
				new Column("MarketInsightStatusId", DbType.Int32, ColumnProperty.Identity),
				new Column("Name", DbType.String, 255, ColumnProperty.NotNull),
				new Column("Language", DbType.StringFixedLength, 2, ColumnProperty.NotNull),
				new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
				new Column("SortOrder", DbType.Int16, ColumnProperty.Null),
				new Column("Color", DbType.String, 20, ColumnProperty.Null),
				new Column("Value", DbType.String, 255, ColumnProperty.NotNull),
				new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull),
				new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull),
				new Column("CreateUser", DbType.String, ColumnProperty.NotNull),
				new Column("ModifyUser", DbType.String, ColumnProperty.NotNull),
				new Column("SelectableByUser", DbType.Boolean, ColumnProperty.NotNull),
				new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true)
			);
		}
	}
}