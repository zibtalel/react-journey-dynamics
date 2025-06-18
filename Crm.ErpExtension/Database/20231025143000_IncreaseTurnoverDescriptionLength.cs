namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20231025143000)]
	public class IncreaseTurnoverDescriptionLength : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("[CRM].[Turnover]", "ItemDescription"))
			{
				var itemDescriptionLength = (int)Database.ExecuteScalar(
					@$"SELECT CHARACTER_MAXIMUM_LENGTH 
								FROM INFORMATION_SCHEMA.COLUMNS
								WHERE TABLE_SCHEMA = 'CRM' AND
								TABLE_NAME = 'Turnover' AND
								COLUMN_NAME = 'ItemDescription'");

				if (itemDescriptionLength < 450)
					Database.ExecuteNonQuery($"ALTER TABLE [CRM].[Turnover] ALTER COLUMN [ItemDescription] NVARCHAR(450) NULL");
			}
		}
	}
}
