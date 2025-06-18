namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200708102600)]
	public class InsertValuesToPotentialStatus : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(
				@"
          SET IDENTITY_INSERT [LU].[PotentialStatus] ON 

					INSERT [LU].[PotentialStatus] ([PotentialStatusId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (1, N'Neu', N'de', N'new', 0, 0, N'#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708102600', N'Migration_20200708102600', 1)
					INSERT [LU].[PotentialStatus] ([PotentialStatusId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (2, N'New', N'en', N'new', 0, 0, N'#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708102600', N'Migration_20200708102600', 1)
					INSERT [LU].[PotentialStatus] ([PotentialStatusId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (3, N'Új', N'hu', N'new', 0, 0, N'#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708102600', N'Migration_20200708102600', 1)
					INSERT [LU].[PotentialStatus] ([PotentialStatusId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (4, N'Geschlossen', N'de', N'closed', 0, 1, N'#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708102600', N'Migration_20200708102600', 1)
					INSERT [LU].[PotentialStatus] ([PotentialStatusId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (5, N'Closed', N'en', N'closed', 0, 1, N'#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708102600', N'Migration_20200708102600', 1)
					INSERT [LU].[PotentialStatus] ([PotentialStatusId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (6, N'Zárva', N'hu', N'closed', 0, 1, N'#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708102600', N'Migration_20200708102600', 1)
					INSERT [LU].[PotentialStatus] ([PotentialStatusId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (7, N'fermé', N'fr', N'closed', 0, 1, N'#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708102600', N'Migration_20200708102600', 1)
					INSERT [LU].[PotentialStatus] ([PotentialStatusId], [Name], [Language], [Value], [Favorite], [SortOrder], [Color], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (8, N'Nouveau', N'fr', N'new', 0, 0, N'#AAAAAA', GETUTCDATE(), GETUTCDATE(), N'Migration_20200708102600', N'Migration_20200708102600', 1)

					SET IDENTITY_INSERT [LU].[PotentialStatus] OFF
        ");
		}
	}
}