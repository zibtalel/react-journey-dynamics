namespace Crm.ProjectOrders.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160623174800)]
	public class ChangeCrmOrderToGuid : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Project' AND COLUMN_NAME='PreferredOfferId' AND DATA_TYPE like '%int')
				BEGIN
					EXEC sp_rename '[CRM].[Project].[PreferredOfferId]', 'PreferredOfferIdOld', 'COLUMN'
					ALTER TABLE [CRM].[Project] ADD [PreferredOfferId] uniqueidentifier NULL
					EXEC('UPDATE a SET a.[PreferredOfferId] = b.[OrderId] FROM [CRM].[Project] a LEFT OUTER JOIN [CRM].[Order] b ON a.[PreferredOfferIdOld] = b.[OrderIdOld]')
					ALTER TABLE [CRM].[Project] ADD CONSTRAINT [FK_Project_PreferredOfferId] FOREIGN KEY ([PreferredOfferId]) REFERENCES [CRM].[Order] ([OrderId])
				END");

		}
	}
}