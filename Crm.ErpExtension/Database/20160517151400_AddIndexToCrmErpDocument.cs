namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160517151400)]
	public class AddIndexToCrmErpDocument : Migration
	{
		public override void Up()
		{
			const string query = @"IF NOT EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_ErpDocument_ContactKey')" +
													 "CREATE NONCLUSTERED INDEX [IX_ErpDocument_ContactKey] ON [CRM].[ERPDocument] ([ContactKey])";

			Database.ExecuteNonQuery(query);
		}
	}
}