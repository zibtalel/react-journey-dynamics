namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160603093800)]
	public class ChangeRplDispatchOrderKeyToGuid : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@" IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'FK_Dispatch_DispatchOrder')
					BEGIN
						ALTER TABLE [RPL].[Dispatch] DROP CONSTRAINT [FK_Dispatch_DispatchOrder]
					END");

			Database.ExecuteNonQuery(@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='RPL' AND TABLE_NAME='Dispatch' AND COLUMN_NAME='DispatchOrderKey' AND DATA_TYPE = 'int') AND EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Contact' AND COLUMN_NAME='ContactId' AND DATA_TYPE = 'uniqueidentifier')
				BEGIN
					EXEC sp_rename '[RPL].[Dispatch].[DispatchOrderKey]', 'DispatchOrderKeyOld', 'COLUMN'
					ALTER TABLE [RPL].[Dispatch] ADD [DispatchOrderKey] uniqueidentifier NULL
					EXEC('UPDATE a SET a.[DispatchOrderKey] = (SELECT TOP 1 b.[ContactId] FROM [CRM].[Contact] b WHERE a.[DispatchOrderKeyOld] = b.[ContactIdOld]) FROM [RPL].[Dispatch] a')
					ALTER TABLE [RPL].[Dispatch] ALTER COLUMN [DispatchOrderKey] uniqueidentifier NOT NULL
					ALTER TABLE [RPL].[Dispatch] ALTER COLUMN [DispatchOrderKeyOld] int NULL
					ALTER TABLE [RPL].[Dispatch] ADD CONSTRAINT [FK_Dispatch_DispatchOrder] FOREIGN KEY([DispatchOrderKey]) REFERENCES [CRM].[Contact] ([ContactId])
				END");
		}
	}
}