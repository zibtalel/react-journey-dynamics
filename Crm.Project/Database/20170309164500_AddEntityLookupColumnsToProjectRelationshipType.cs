namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170309164500)]
	public class AddEntityLookupColumnsToProjectRelationshipType : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
IF COL_LENGTH('[LU].[ProjectContactRelationshipType]','CreateDate') IS NULL 
BEGIN
	ALTER TABLE [LU].[ProjectContactRelationshipType] ADD [CreateDate] datetime NOT NULL CONSTRAINT DF_LuProjectContactRelationshipType_CreateDate DEFAULT GETUTCDATE()
	ALTER TABLE [LU].[ProjectContactRelationshipType] DROP CONSTRAINT DF_LuProjectContactRelationshipType_CreateDate
END
IF COL_LENGTH('[LU].[ProjectContactRelationshipType]','ModifyDate') IS NULL 
BEGIN
	ALTER TABLE [LU].[ProjectContactRelationshipType] ADD [ModifyDate] datetime NOT NULL CONSTRAINT DF_LuProjectContactRelationshipType_ModifyDate DEFAULT GETUTCDATE()
	ALTER TABLE [LU].[ProjectContactRelationshipType] DROP CONSTRAINT DF_LuProjectContactRelationshipType_ModifyDate
END
IF COL_LENGTH('[LU].[ProjectContactRelationshipType]','CreateUser') IS NULL 
BEGIN
	ALTER TABLE [LU].[ProjectContactRelationshipType] ADD [CreateUser] nvarchar(100) NOT NULL CONSTRAINT DF_LuProjectContactRelationshipType_CreateUser DEFAULT 'Migration_20170309164500'
	ALTER TABLE [LU].[ProjectContactRelationshipType] DROP CONSTRAINT DF_LuProjectContactRelationshipType_CreateUser
END
IF COL_LENGTH('[LU].[ProjectContactRelationshipType]','ModifyUser') IS NULL 
BEGIN
	ALTER TABLE [LU].[ProjectContactRelationshipType] ADD [ModifyUser] nvarchar(100) NOT NULL CONSTRAINT DF_LuProjectContactRelationshipType_ModifyUser DEFAULT 'Migration_20170309164500'
	ALTER TABLE [LU].[ProjectContactRelationshipType] DROP CONSTRAINT DF_LuProjectContactRelationshipType_ModifyUser
END
IF COL_LENGTH('[LU].[ProjectContactRelationshipType]','IsActive') IS NULL 
BEGIN
	ALTER TABLE [LU].[ProjectContactRelationshipType] ADD [IsActive] bit NOT NULL CONSTRAINT DF_LuProjectContactRelationshipType_IsActive DEFAULT 1
	ALTER TABLE [LU].[ProjectContactRelationshipType] DROP CONSTRAINT DF_LuProjectContactRelationshipType_IsActive
END
			");
		}
	}
}