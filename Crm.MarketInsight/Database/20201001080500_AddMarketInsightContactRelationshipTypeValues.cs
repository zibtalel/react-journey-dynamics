namespace Crm.MarketInsight.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20201001080500)]
	public class AddMarketInsightContactRelationshipTypeValues : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(
				@"
			SET IDENTITY_INSERT [LU].[MarketInsightContactRelationshipType] ON 

			INSERT [LU].[MarketInsightContactRelationshipType] ([MarketInsightContactRelationshipTypeId], [Value], [Name], [Language], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (1, N'Other', N'Sonstiges', N'de', 0, 0, GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080500', N'Migration_20201001080500', 1)
			INSERT [LU].[MarketInsightContactRelationshipType] ([MarketInsightContactRelationshipTypeId], [Value], [Name], [Language], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (2, N'Other', N'Other', N'en', 0, 0, GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080500', N'Migration_20201001080500', 1)
			INSERT [LU].[MarketInsightContactRelationshipType] ([MarketInsightContactRelationshipTypeId], [Value], [Name], [Language], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES (3, N'Other', N'Autres', N'fr', 0, 0, GETUTCDATE(), GETUTCDATE(), N'Migration_20201001080500', N'Migration_20201001080500', 1)

			SET IDENTITY_INSERT [LU].[MarketInsightContactRelationshipType] OFF"
			);
		}
	}
}