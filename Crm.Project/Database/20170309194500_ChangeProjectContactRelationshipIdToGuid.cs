namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170309194500)]
	public class ChangeProjectContactRelationshipIdToGuid : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='ProjectContactRelationship' AND COLUMN_NAME='ProjectContactRelationshipId' AND DATA_TYPE = 'int')
BEGIN
	DECLARE @pkName AS NVARCHAR(MAX)
	SET @pkName = (SELECT name FROM sys.objects WHERE type = 'PK' AND parent_object_id = object_id('CRM.ProjectContactRelationship'))
	EXEC('ALTER TABLE [CRM].[ProjectContactRelationship] DROP CONSTRAINT ' + @pkName)
	EXEC sp_rename 'CRM.ProjectContactRelationship.ProjectContactRelationshipId', 'ProjectContactRelationshipIdOld', 'COLUMN'; 
	
	ALTER TABLE [CRM].[ProjectContactRelationship] ADD [ProjectContactRelationshipId] uniqueidentifier NOT NULL DEFAULT(newid())
	ALTER TABLE [CRM].[ProjectContactRelationship] ADD CONSTRAINT PK_ProjectContactRelationship PRIMARY KEY (ProjectContactRelationshipId)
	UPDATE [CRM].[ProjectContactRelationship] SET ModifyDate = GETUTCDATE(), ModifyUser = 'Migration_20170309194500'
END
		");
		}
	}
}