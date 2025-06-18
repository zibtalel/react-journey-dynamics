namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20150413140499)]
	public class ChangeCrmProjectFolderKeyToGuid : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Project' AND COLUMN_NAME='FolderKey' AND DATA_TYPE = 'int') AND EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Contact' AND COLUMN_NAME='ContactId' AND DATA_TYPE = 'uniqueidentifier')
				BEGIN
					EXEC sp_rename '[CRM].[Project].[FolderKey]', 'FolderKeyOld', 'COLUMN'
					ALTER TABLE [CRM].[Project] ADD [FolderKey] uniqueidentifier NULL
					EXEC('UPDATE a SET a.[FolderKey] = b.[ContactId] FROM [CRM].[Project] a LEFT OUTER JOIN [CRM].[Contact] b ON a.[FolderKeyOld] = b.[ContactIdOld]')
				END");
		}
	}
}