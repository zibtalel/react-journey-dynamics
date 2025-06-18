namespace Crm.Configurator.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20151221143100)]
	public class AddConfigurationBaseIdToCrmOrder : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[Order]", "ConfigurationBaseId"))
			{
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[Order] ADD [ConfigurationBaseId] uniqueidentifier NULL");
				Database.ExecuteNonQuery("ALTER TABLE [CRM].[Order] WITH CHECK ADD CONSTRAINT [FK_Order_ConfigurationBaseId] FOREIGN KEY ([ConfigurationBaseId]) REFERENCES [CRM].[Contact] ([ContactId])");
			}
		}
	}
}