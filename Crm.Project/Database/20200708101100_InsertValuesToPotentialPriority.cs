namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200708101100)]
	public class InsertValuesToPotentialPriority : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(
				@" 
            SET IDENTITY_INSERT [LU].[PotentialPriority] ON 
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (1, N'20%', N'en', N'prio1', 0, 1, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (2, N'20%', N'de', N'prio1', 0, 1, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (3, N'20%', N'fr', N'prio1', 0, 1, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (4, N'20%', N'hu', N'prio1', 0, 1, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (5, N'40%', N'en', N'prio2', 0, 2, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (6, N'40%', N'de', N'prio2', 0, 2, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (7, N'40%', N'fr', N'prio2', 0, 2, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (8, N'40%', N'hu', N'prio2', 0, 2, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (9, N'60%', N'en', N'prio3', 0, 3, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (10, N'60%', N'de', N'prio3', 0, 3, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (11, N'60%', N'fr', N'prio3', 0, 3, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (12, N'60%', N'hu', N'prio3', 0, 3, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (13, N'80%', N'en', N'prio4', 0, 4, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (14, N'80%', N'de', N'prio4', 0, 4, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (15, N'80%', N'fr', N'prio4', 0, 4, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (16, N'80%', N'hu', N'prio4', 0, 4, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (17, N'100%', N'en', N'prio5', 0, 5, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (18, N'100%', N'de', N'prio5', 0, 5, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (19, N'100%', N'fr', N'prio5', 0, 5, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						INSERT [LU].[PotentialPriority] ([PotentialPriorityId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (20, N'100%', N'hu', N'prio5', 0, 5, N'#aaaaaa', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708101100', N'Migration_20200708101100', 1)
						SET IDENTITY_INSERT [LU].[PotentialPriority] OFF
        ");
		}
	}
}