namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20230626095400)]
	public class QuantityUnitKeyLength : Migration
	{
		public override void Up()
		{
			const string schema = "CRM";
			const string table = "ERPDocument";
			const string column = "QuantityUnit";

			var length = (int)Database.ExecuteScalar(
				$@"	SELECT CHARACTER_MAXIMUM_LENGTH
						FROM INFORMATION_SCHEMA.COLUMNS
						WHERE TABLE_SCHEMA = '{schema}' AND
							TABLE_NAME = '{table}' AND
							COLUMN_NAME = '{column}'");
			if (length < 20)
			{
				Database.ExecuteNonQuery($"ALTER TABLE [{schema}].[{table}] ALTER COLUMN [{column}] NVARCHAR(20)");
			}
		}
	}
}
