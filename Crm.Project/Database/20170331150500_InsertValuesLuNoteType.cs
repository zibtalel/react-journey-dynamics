namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170331150500)]
	public class InsertValuesLuNoteType : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"
INSERT INTO [LU].[NoteType]
           ([Name]
           ,[Language]
           ,[Value]
		   ,[Color]
		   ,[Icon]
           ,[Favorite]
           ,[SortOrder]
           ,[TenantKey]
           ,[CreateDate]
           ,[ModifyDate]
           ,[CreateUser]
           ,[ModifyUser]
           ,[IsActive])
     VALUES
 ('Neues Projekt',			'de', 'ProjectCreatedNote',			'#4caf50', '\f223', 0, 0, NULL, GETUTCDATE(), GETUTCDATE(), 'Migration_20170331150500', 'Migration_20170331150500', 1)
,('New Project',			'en', 'ProjectCreatedNote',			'#4caf50', '\f223', 0, 0, NULL, GETUTCDATE(), GETUTCDATE(), 'Migration_20170331150500', 'Migration_20170331150500', 1)
,('Projekt verloren',		'de', 'ProjectLostNote',			'#ff1843', '\f223', 0, 0, NULL, GETUTCDATE(), GETUTCDATE(), 'Migration_20170331150500', 'Migration_20170331150500', 1)
,('Project Lost',			'en', 'ProjectLostNote',			'#ff1843', '\f223', 0, 0, NULL, GETUTCDATE(), GETUTCDATE(), 'Migration_20170331150500', 'Migration_20170331150500', 1)
,('Projekt-Statusänderung', 'de', 'ProjectStatusChangedNote',	'#9164a6', '\f223', 0, 0, NULL, GETUTCDATE(), GETUTCDATE(), 'Migration_20170331150500', 'Migration_20170331150500', 1)
,('Project Status changed', 'en', 'ProjectStatusChangedNote',	'#9164a6', '\f223', 0, 0, NULL, GETUTCDATE(), GETUTCDATE(), 'Migration_20170331150500', 'Migration_20170331150500', 1)
			");
		}
	}
}