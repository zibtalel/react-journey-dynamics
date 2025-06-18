namespace Crm.ErpExtension.Database
{
  using Crm.Library.Data.MigratorDotNet.Framework;

  [Migration(20160622113500)]
  public class ChangeErpTurnoverContactKeyToGuid : Migration
  {
    public override void Up()
    {
			Database.ExecuteNonQuery(@"
				IF EXISTS (SELECT * FROM sys.indexes WHERE NAME = 'IX_ContactKey_RecordType')
				BEGIN
					DROP INDEX [IX_ContactKey_RecordType] ON [CRM].[Turnover]
				END");

				Database.ExecuteNonQuery(@"	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Turnover' AND COLUMN_NAME='ContactKey' AND DATA_TYPE = 'int')
					BEGIN
						EXEC sp_rename '[CRM].[Turnover].[ContactKey]', 'ContactKeyOld', 'COLUMN'
						ALTER TABLE [CRM].[Turnover] ADD [ContactKey] uniqueidentifier NULL
						EXEC('UPDATE a SET a.[ContactKey] = b.[ContactId] FROM [CRM].[Turnover] a LEFT OUTER JOIN [CRM].[Contact] b ON a.[ContactKeyOld] = b.[ContactIdOld]')
						ALTER TABLE [CRM].[Turnover] ALTER COLUMN [ContactKey] uniqueidentifier NOT NULL
						ALTER TABLE [CRM].[Turnover] ALTER COLUMN [ContactKeyOld] int NULL
						CREATE NONCLUSTERED INDEX [IX_ContactKey_RecordType] ON [CRM].[Turnover] ([ContactKey] ASC,[RecordType] ASC)
					END");
    }
  }
}