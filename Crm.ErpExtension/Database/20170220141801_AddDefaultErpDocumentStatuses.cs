namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170220141801)]
	public class AddDefaultErpDocumentStatuses : Migration
	{
		public override void Up()
		{
			if ((int)Database.ExecuteScalar("SELECT COUNT(*) FROM [LU].[ErpDocumentStatus]") == 0)
			{
				InsertLookupValue("Closed", "Closed", "#4CAF50", "en", "0", "0");
				InsertLookupValue("Closed", "Geschlossen", "#4CAF50", "de", "0", "0");
				InsertLookupValue("Open", "Open", "#FF9800", "en", "0", "0");
				InsertLookupValue("Open", "Offen", "#FF9800", "de", "0", "0");
			}
		}
		private void InsertLookupValue(string value, string name, string color, string language, string favorite, string sortOrder)
		{
			Database.Insert("[LU].[ErpDocumentStatus]",
				new[] { "Value", "Name", "Color", "Language", "Favorite", "SortOrder" },
				new[] { value, name, color, language, favorite, sortOrder });
		}
	}
}