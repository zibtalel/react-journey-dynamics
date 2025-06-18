namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160622103300)]
	public class AddContactKeyFkToCrmErpDocument : Migration
	{
		public override void Up()
		{
			if ((int)Database.ExecuteScalar("SELECT COUNT(*) FROM sys.foreign_keys WHERE name = 'FK_ErpDocument_Contact'") == 0)
            {
                Database.ExecuteNonQuery(@"
				IF EXISTS (SELECT * FROM sys.indexes WHERE NAME = 'IX_ErpDocument_ContactKey')
				BEGIN
					DROP INDEX [IX_ErpDocument_ContactKey] ON [CRM].[ErpDocument]
				END");

                Database.ExecuteNonQuery(@"	IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='ErpDocument' AND COLUMN_NAME='ContactKey' AND DATA_TYPE = 'int')
					BEGIN
						EXEC sp_rename '[CRM].[ErpDocument].[ContactKey]', 'ContactKeyOld', 'COLUMN'
						ALTER TABLE [CRM].[ErpDocument] ADD [ContactKey] uniqueidentifier NULL
						EXEC('UPDATE a SET a.[ContactKey] = b.[ContactId] FROM [CRM].[ErpDocument] a LEFT OUTER JOIN [CRM].[Contact] b ON a.[ContactKeyOld] = b.[ContactIdOld]')
						ALTER TABLE [CRM].[ErpDocument] ALTER COLUMN [ContactKeyOld] int NULL
						CREATE NONCLUSTERED INDEX [IX_ErpDocument_ContactKey] ON [CRM].[ERPDocument]([ContactKey] ASC)
					END");
                Database.ExecuteNonQuery("UPDATE e SET e.[ContactKey] = NULL FROM [CRM].[ErpDocument] e LEFT OUTER JOIN [CRM].[Contact] c on e.[ContactKey] = c.[ContactId] WHERE c.[ContactId] IS NULL");
				//Database.AddForeignKey("FK_ErpDocument_Contact", "[CRM].[ErpDocument]", "ContactKey", "[CRM].[Contact]", "ContactId", ForeignKeyConstraint.SetNull);
			}
		}
	}
}