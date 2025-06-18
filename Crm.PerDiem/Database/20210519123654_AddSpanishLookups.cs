using System;
using Crm.Library.Data.MigratorDotNet.Framework;

namespace Crm.PerDiem.Database
{
	[Migration(20210519123654)]
	public class AddSpanishLookups : Migration
	{
		public override void Up()
		{
			string tableName = "[LU].[PerDiemReportStatus]";
			if (Database.TableExists(tableName))
			{
				string[] columns = { "Value", "Name", "Language", "Favorite", "SortOrder", "SettableStatuses", "ShowInMobileClient" };
				InsertLookupValue(tableName, columns, "'Closed', 'Cerrado', 'es', 0, 2, 'Open', 1");
				InsertLookupValue(tableName, columns, "'Open', 'Abierto', 'es', 0, 0, 'Closed', 0");
				InsertLookupValue(tableName, columns, "'RequestClose', 'Solicitud de cierre', 'es', 0, 1, 'Closed', 1");
			}
			tableName = "[LU].[PerDiemReportType]";
			if (Database.TableExists(tableName))
			{
				string[] columns = { "Value", "Name", "Language", "Favorite", "SortOrder" };
				InsertLookupValue(tableName, columns, "'Custom', 'Personalizado', 'es', 0, 1");
				InsertLookupValue(tableName, columns, "'Monthly', 'Mensual', 'es', 0, 0");
				InsertLookupValue(tableName, columns, "'Weekly', 'Semanal', 'es', 0, 0");
			}
			tableName = "[SMS].[ExpenseType]";
			if (Database.TableExists(tableName))
			{
				string[] columns = { "Name", "Language", "Value", "Favorite", "SortOrder", "ValidCostCenters" };
				InsertLookupValue(tableName, columns, "'Víveres', 'es', 100, 0, 4, NULL");
				InsertLookupValue(tableName, columns, "'Alojamiento', 'es', 101, 0, 3, NULL");
				InsertLookupValue(tableName, columns, "'Envío de carga', 'es', 102, 0, 1, NULL");
				InsertLookupValue(tableName, columns, "'Transporte', 'es', 103, 0, 0, NULL");
				InsertLookupValue(tableName, columns, "'Otro', 'es', 104, 0, 2, NULL");
			}
			tableName = "[SMS].[TimeEntryType]";
			if (Database.TableExists(tableName))
			{
				string[] columns = { "Name", "Language", "Value", "Favorite", "SortOrder", "Color", "ShowInScheduler", "DecreasesCapacity", "ShowInMobileClient", "DefaultDurationInMinutes", "ValidCostCenters" };
				InsertLookupValue(tableName, columns, "'Repostaje', 'es', 100, 0, 0, '#AAAAAA', 0, 0, 1, NULL, NULL");
				InsertLookupValue(tableName, columns, "'Viaje', 'es', 101, 0, 0, '#AAAAAA', 0, 0, 1, NULL, NULL");
				InsertLookupValue(tableName, columns, "'Soporte de ventas', 'es', 102, 0, 0, '#AAAAAA', 0, 0, 1, NULL, NULL");
				InsertLookupValue(tableName, columns, "'Tiempo libre (oficina, envíos, stock, coche)', 'es', 103, 0, 0, '#AAAAAA', 0, 0, 1, NULL, NULL");
				InsertLookupValue(tableName, columns, "'Tiempo de preparación', 'es', 104, 0, 0, '#AAAAAA', 0, 0, 1, NULL, NULL");
				InsertLookupValue(tableName, columns, "'Otro', 'es', 105, 0, 0, '#AAAAAA', 0, 0, 1, NULL, NULL");
				InsertLookupValue(tableName, columns, "'Vacaciones', 'es', 106, 0, 0, '#CDDC39', 1, 1, 0, NULL, NULL");
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