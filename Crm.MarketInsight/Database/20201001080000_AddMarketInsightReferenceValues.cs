namespace Crm.MarketInsight.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20201001080000)]
	public class AddMarketInsightReferenceValues : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(
				@"
        SET IDENTITY_INSERT [LU].[MarketInsightReference] ON 

				INSERT [LU].[MarketInsightReference] ([ReferenceOptionId], [Name], [Language], [Favorite], [SortOrder], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (1, N'Ja, auf der Homepage und eignet sich besonders für einen Referenzbesuch', N'de', 0, 1, N'suitable', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080000', N'Migration_20201001080000', 1)
				INSERT [LU].[MarketInsightReference] ([ReferenceOptionId], [Name], [Language], [Favorite], [SortOrder], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (2, N'Yes, on homepage and suitable for reference visit', N'en', 0, 1, N'suitable', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080000', N'Migration_20201001080000', 1)
				INSERT [LU].[MarketInsightReference] ([ReferenceOptionId], [Name], [Language], [Favorite], [SortOrder], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (3, N'alkalmas', N'hu', 0, 1, N'suitable', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080000', N'Migration_20201001080000', 1)
				INSERT [LU].[MarketInsightReference] ([ReferenceOptionId], [Name], [Language], [Favorite], [SortOrder], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (4, N'Nein', N'de', 0, 3, N'no', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080000', N'Migration_20201001080000', 1)
				INSERT [LU].[MarketInsightReference] ([ReferenceOptionId], [Name], [Language], [Favorite], [SortOrder], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (5, N'No', N'en', 0, 3, N'no', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080000', N'Migration_20201001080000', 1)
				INSERT [LU].[MarketInsightReference] ([ReferenceOptionId], [Name], [Language], [Favorite], [SortOrder], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (6, N'alkalmatlan', N'hu', 0, 2, N'unsuitable', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080000', N'Migration_20201001080000', 0)
				INSERT [LU].[MarketInsightReference] ([ReferenceOptionId], [Name], [Language], [Favorite], [SortOrder], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (7, N'Ja, auf der Homepage', N'de', 0, 2, N'confirmed', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080000', N'Migration_20201001080000', 1)
				INSERT [LU].[MarketInsightReference] ([ReferenceOptionId], [Name], [Language], [Favorite], [SortOrder], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (8, N'Yes, on homepage', N'en', 0, 2, N'confirmed', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080000', N'Migration_20201001080000', 1)
				INSERT [LU].[MarketInsightReference] ([ReferenceOptionId], [Name], [Language], [Favorite], [SortOrder], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (9, N'Igen, a honlapon', N'hu', 0, 2, N'confirmed', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080000', N'Migration_20201001080000', 1)

				SET IDENTITY_INSERT [LU].[MarketInsightReference] OFF"
			);
		}
	}
}