namespace Crm.Service.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160623141000)]
	public class ChangeServiceOrderDispatchToGuid : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@" IF EXISTS (SELECT * FROM sys.indexes WHERE NAME = 'IX_LegacyId_Type')
					BEGIN
						DROP INDEX [IX_LegacyId_Type] ON [RPL].[Dispatch]
					END

				IF EXISTS (SELECT * FROM sys.indexes WHERE NAME = 'IX_LegacyId')
					BEGIN
						DROP INDEX [IX_LegacyId] ON [RPL].[Dispatch]
					END");

			Database.ExecuteNonQuery(@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='RPL' AND TABLE_NAME='Dispatch' AND COLUMN_NAME='LegacyId' AND DATA_TYPE like '%int')
				BEGIN
					EXEC sp_rename '[RPL].[Dispatch].[LegacyId]', 'LegacyIdOld', 'COLUMN'
					ALTER TABLE [RPL].[Dispatch] ADD [LegacyId] uniqueidentifier NULL
					EXEC('UPDATE a SET a.[LegacyId] = (SELECT b.[DispatchId] FROM [SMS].[ServiceOrderDispatch] b WHERE a.[LegacyIdOld] = b.[DispatchIdOld]) FROM [RPL].[Dispatch] a')
					EXEC('UPDATE a SET a.[LegacyId] = b.[DispatchId] FROM [RPL].[Dispatch] a LEFT OUTER JOIN [SMS].[ServiceOrderDispatch] b ON a.[LegacyIdOld] = b.[DispatchIdOld]')
					ALTER TABLE [RPL].[Dispatch] ALTER COLUMN [LegacyIdOld] BIGINT NULL
					CREATE NONCLUSTERED INDEX [IX_LegacyId_Type] ON [RPL].[Dispatch] ([LegacyId] ASC,	[Type] ASC)
					CREATE NONCLUSTERED INDEX [IX_LegacyId] ON [RPL].[Dispatch] ([LegacyId] ASC)
				END");
		}
	}
}