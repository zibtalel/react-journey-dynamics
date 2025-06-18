using System;
using Crm.Library.Data.MigratorDotNet.Framework;

namespace Crm.Configurator.Database
{
	[Migration(20210422133800)]
	public class AddSpanishLookups : Migration
	{
		public override void Up()
		{
			string tableName = "[LU].[ConfigurationRuleType]";
			if (Database.TableExists(tableName))
			{
				string[] columns = { "Value", "Name", "Language" };
				InsertLookupValue(tableName, columns, "'Hint', 'Pista', 'es'");
				InsertLookupValue(tableName, columns, "'Inclusion', 'Inclusión', 'es'");
				InsertLookupValue(tableName, columns, "'Restriction', 'Restricción', 'es'");
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