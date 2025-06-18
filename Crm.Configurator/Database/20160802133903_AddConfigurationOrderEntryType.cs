namespace Crm.Configurator.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160802133903)]
	public class AddConfigurationOrderEntryType : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("INSERT INTO [LU].[OrderEntryType] ([Value], [Name], [Language], [CreateUser], [ModifyUser]) VALUES ('Configuration', 'Konfiguration', 'de', 'Setup', 'Setup')");
			Database.ExecuteNonQuery("INSERT INTO [LU].[OrderEntryType] ([Value], [Name], [Language], [CreateUser], [ModifyUser]) VALUES ('Configuration', 'Konfiguration', 'en', 'Setup', 'Setup')");
			Database.ExecuteNonQuery("UPDATE [CRM].[Order] SET [OrderEntryType] = 'Configuration' WHERE [ConfigurationBaseId] IS NOT NULL");
	}
  }
}