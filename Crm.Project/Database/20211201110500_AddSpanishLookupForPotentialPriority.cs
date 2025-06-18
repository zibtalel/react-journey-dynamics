using System;
using Crm.Library.Data.MigratorDotNet.Framework;

namespace Crm.Project.Database
{
	[Migration(20211201110500)]
	public class AddSpanishLookupsPotentialPriority : Migration
	{
		public override void Up()
		{
			string tableName = "[LU].[PotentialPriority]";
			if (Database.TableExists(tableName))
			{

				string[] columns = { "Name", "Language", "Value", "Favorite", "SortOrder", "Color","CreateDate", "ModifyDate", "CreateUser", "ModifyUser", "IsActive" };
				InsertLookupValue(tableName, columns, "'20% (Contacto personal)', 'es', 'prio1', 0, 1, '#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20211201110500', 1");
				InsertLookupValue(tableName, columns, "'40% (Informacion enviada)', 'es', 'prio2', 0, 2, '#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20211201110500', 1");
				InsertLookupValue(tableName, columns, "'60% (Interés sustancial)', 'es', 'prio3', 0, 3, '#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20211201110500', 1");
				InsertLookupValue(tableName, columns, "'80% (Proyecto presupuestado)', 'es', 'prio4', 0, 4, '#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20211201110500', 1");
				InsertLookupValue(tableName, columns, "'100% (Listo para el traspaso)', 'es', 'prio5', 0, 5, '#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20211201110500', 1");
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