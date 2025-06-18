namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20150413141059)]
	public class ChangeCrmProjectContactRelationshipToGuid : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'FK_ProjectContactRelationship_Project')
				BEGIN
					ALTER TABLE [CRM].[ProjectContactRelationship] DROP CONSTRAINT [FK_ProjectContactRelationship_Project]
				END

			IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'FK_ProjectContactRelationship_Contact')
				BEGIN
					ALTER TABLE[CRM].[ProjectContactRelationship] DROP CONSTRAINT[FK_ProjectContactRelationship_Contact]
				END");

			Database.ExecuteNonQuery(@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='ProjectContactRelationship' AND COLUMN_NAME='ProjectKey' AND DATA_TYPE = 'int') AND EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Contact' AND COLUMN_NAME='ContactId' AND DATA_TYPE = 'uniqueidentifier')
				BEGIN
					EXEC sp_rename '[CRM].[ProjectContactRelationship].[ProjectKey]', 'ProjectKeyOld', 'COLUMN'
					ALTER TABLE [CRM].[ProjectContactRelationship] ADD [ProjectKey] uniqueidentifier NULL
					EXEC('UPDATE a SET a.[ProjectKey] = b.[ProjectId] FROM [CRM].[ProjectContactRelationship] a LEFT OUTER JOIN [CRM].[Project] b ON a.[ProjectKeyOld] = b.[ContactIdOld]')
					ALTER TABLE [CRM].[ProjectContactRelationship] ALTER COLUMN [ProjectKey] uniqueidentifier NOT NULL
					ALTER TABLE [CRM].[ProjectContactRelationship] ALTER COLUMN [ProjectKeyOld] int NULL
					ALTER TABLE [CRM].[ProjectContactRelationship] ADD CONSTRAINT [FK_ProjectContactRelationship_Project] FOREIGN KEY([ProjectKey]) REFERENCES [CRM].[Contact] ([ContactId])
				END");

							Database.ExecuteNonQuery(@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='ProjectContactRelationship' AND COLUMN_NAME='ContactKey' AND DATA_TYPE = 'int') AND EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Contact' AND COLUMN_NAME='ContactId' AND DATA_TYPE = 'uniqueidentifier')
				BEGIN
					EXEC sp_rename '[CRM].[ProjectContactRelationship].[ContactKey]', 'ContactKeyOld', 'COLUMN'
					ALTER TABLE [CRM].[ProjectContactRelationship] ADD [ContactKey] uniqueidentifier NULL
					EXEC('UPDATE a SET a.[ContactKey] = (SELECT TOP 1 b.[ContactId] FROM [CRM].[Contact] b WHERE a.[ContactKeyOld] = b.[ContactIdOld]) FROM [CRM].[ProjectContactRelationship] a')
					EXEC('UPDATE a SET a.[ContactKey] = b.[ContactId] FROM [CRM].[ProjectContactRelationship] a LEFT OUTER JOIN [CRM].[Contact] b ON a.[ProjectKeyOld] = b.[ContactIdOld]')
					ALTER TABLE [CRM].[ProjectContactRelationship] ALTER COLUMN [ContactKey] uniqueidentifier NOT NULL
					ALTER TABLE [CRM].[ProjectContactRelationship] ALTER COLUMN [ContactKeyOld] int NULL
					ALTER TABLE [CRM].[ProjectContactRelationship] ADD CONSTRAINT [FK_ProjectContactRelationship_Contact] FOREIGN KEY([ContactKey]) REFERENCES [CRM].[Contact] ([ContactId])
				END");
		}
	}
}