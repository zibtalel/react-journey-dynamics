namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20181227133000)]
	public class AddHomeAddressIdToUser : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
				ALTER TABLE [CRM].[User]
				ADD HomeAddressId UNIQUEIDENTIFIER NULL CONSTRAINT [FK_HomeAddressId] FOREIGN KEY REFERENCES [Crm].[Address]([AddressId])
			");
		}
	}
}