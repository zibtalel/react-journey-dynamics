namespace Crm.ProjectOrders.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20151211115400)]
	public class UpdateCrmOrderProjectKeyToGuid : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Order' AND COLUMN_NAME='ProjectKey' AND DATA_TYPE = 'int')
				BEGIN
					EXEC sp_rename '[CRM].[Order].[ProjectKey]', 'ProjectKeyOld', 'COLUMN'
					ALTER TABLE [CRM].[Order] ADD [ProjectKey] uniqueidentifier NULL
					EXEC('UPDATE a SET a.[ProjectKey] = (SELECT TOP 1 b.[ContactId] FROM [CRM].[Contact] b WHERE a.[ProjectKeyOld] = b.[ContactIdOld]) FROM [CRM].[Order] a')
					ALTER TABLE [CRM].[Order] ADD CONSTRAINT [FK_Order_Contact] FOREIGN KEY([ProjectKey]) REFERENCES [CRM].[Contact] ([ContactId])
				END");
		}
	}
}