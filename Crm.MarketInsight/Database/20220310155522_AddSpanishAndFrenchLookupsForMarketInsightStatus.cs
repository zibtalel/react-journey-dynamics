namespace Crm.Project.Database
{
	using System;
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20220310155522)]
	public class AddSpanishAndFrenchLookupsForMarketInsightStatus : Migration
	{
		public override void Up()
		{
			string tableName = "[LU].[MarketInsightStatus]";
			if (Database.TableExists(tableName))
			{

				string[] columns = { "Name", "Language", "Favorite", "SortOrder", "Color", "Value", "CreateDate", "ModifyDate", "CreateUser", "ModifyUser", "SelectableByUser", "IsActive" };
				InsertLookupValue(tableName, columns, "'Cliente', 'es', 0, 1, '#008000', 'customer', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'Posibles calificados', 'es', 0, 3, '#ffa500', 'qualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'En ventas', 'es', 0, 2, '#ffd700', 'sales', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'No calificado', 'es', 0, 5, '#b22222', 'unqualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'No disponible', 'es', 0, 4, '#ff0000', 'notavailable', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'Perdido/Fuera de línea', 'es', 0, 6, '#808080', 'lost', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");

				InsertLookupValue(tableName, columns, "'Client', 'fr', 0, 1, '#008000', 'customer', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'Potentiel qualifié', 'fr', 0, 3, '#ffa500', 'qualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'A la vente', 'fr', 0, 2, '#ffd700', 'sales', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'Non qualifié', 'fr', 0, 5, '#b22222', 'unqualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'Non disponible', 'fr', 0, 4, '#ff0000', 'notavailable', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
				InsertLookupValue(tableName, columns, "'Perdu/hors ligne', 'fr', 0, 6, '#808080', 'lost', GETUTCDATE(), GETUTCDATE(), N'Migration_20220310155522', N'Migration_20220310155522', 1 , 1");
			}
		}
		private void InsertLookupValue(string tableName, string[] columns, string values, bool hasIsActiveColumn = true)
		{
			int keyColumnIndex = Array.IndexOf(columns, "Value");
			string keyValue = values.Split(',')[keyColumnIndex].Trim(new char[] { ' ', '\'' });
			if ((int)Database.ExecuteScalar($"SELECT COUNT(*) FROM {tableName} WHERE {(hasIsActiveColumn ? "[IsActive]" : 1)} = 1 AND [Value] = '{keyValue}'") > 0)
			{
				Database.ExecuteNonQuery($"INSERT INTO {tableName} ({string.Join(", ", columns)}) VALUES ({values})");
			}
		}
	}
}