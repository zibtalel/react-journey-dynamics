using System;
using Crm.Library.Data.MigratorDotNet.Framework;

namespace Crm.Project.Database
{
	[Migration(20210519124547)]
	public class AddSpanishLookups : Migration
	{
		public override void Up()
		{
			string tableName = "[LU].[ProjectCategory]";
			if (Database.TableExists(tableName))
			{
				string[] columns = { "Name", "Language", "Value", "Favorite", "SortOrder", "Color" };
				InsertLookupValue(tableName, columns, "'Ninguno', 'es', '100', 1, 0, '#AAAAAA'");
				InsertLookupValue(tableName, columns, "'Servicio profesional', 'es', '101', 0, 0, '#AAAAAA'");
				InsertLookupValue(tableName, columns, "'Software', 'es', '102', 0, 0, '#AAAAAA'");
				InsertLookupValue(tableName, columns, "'Hardware', 'es', '103', 0, 0, '#AAAAAA'");
			}
			tableName = "[LU].[ProjectContactRelationshipType]";
			if (Database.TableExists(tableName))
			{
				string[] columns = { "Value", "Name", "Language", "Favorite", "SortOrder" };
				InsertLookupValue(tableName, columns, "'Other', 'Otro', 'es', 1, 0");
			}
			tableName = "[LU].[ProjectLostReasonCategory]";
			if (Database.TableExists(tableName))
			{
				string[] columns = { "Name", "Language", "Value", "Favorite", "SortOrder" };
				InsertLookupValue(tableName, columns, "'Competidor', 'es', '100', 0, 0");
				InsertLookupValue(tableName, columns, "'Precio', 'es', '101', 0, 0");
				InsertLookupValue(tableName, columns, "'Características', 'es', '102', 0, 0");
			}
			tableName = "[LU].[ProjectStatus]";
			if (Database.TableExists(tableName))
			{
				string[] columns = { "Name", "Language", "Value", "Favorite", "SortOrder", "Color" };
				InsertLookupValue(tableName, columns, "'Abierto', 'es', '100', 0, 1, '#AAAAAA'");
				InsertLookupValue(tableName, columns, "'Ganado', 'es', '101', 0, 3, '#AAAAAA'");
				InsertLookupValue(tableName, columns, "'Perdido', 'es', '102', 0, 5, '#AAAAAA'");
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