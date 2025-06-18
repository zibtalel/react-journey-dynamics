namespace Crm.MarketInsight.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20201001073300)]
	public class AddMarkteInsightStatusValues : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(
				@"
        SET IDENTITY_INSERT [LU].[MarketInsightStatus] ON 

				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (4, N'verloren/abgeschalten', N'de', 0, 6, N'#808080', N'lost', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (5, N'lost/offline', N'en', 0, 6, N'#808080', N'lost', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (6, N'elveszett/nem elérheto', N'hu', 0, 1, N'#808080', N'lost', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (7, N'unqualifiziert', N'de', 0, 5, N'#b22222', N'unqualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (8, N'unqualified', N'en', 0, 5, N'#b22222', N'unqualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (9, N'képzetlen', N'hu', 0, 2, N'#B22222', N'unqualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (10, N'Nicht vorhanden', N'de', 0, 4, N'#ff0000', N'notavailable', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (11, N'Not available', N'en', 0, 4, N'#ff0000', N'notavailable', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (12, N'nem áll rendelkezésre', N'hu', 0, 3, N'#FF0000', N'notavailable', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (13, N'Potential qualifiziert', N'de', 0, 3, N'#ffa500', N'qualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (14, N'Potential qualified', N'en', 0, 3, N'#ffa500', N'qualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (15, N'potenciálisan képzett', N'hu', 0, 4, N'#FFA500', N'qualified', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (16, N'im Vertrieb', N'de', 0, 2, N'#ffd700', N'sales', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (17, N'at sales', N'en', 0, 2, N'#ffd700', N'sales', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (18, N'az értékesítésben', N'hu', 0, 5, N'#ffd700', N'sales', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (19, N'Kunde', N'de', 0, 1, N'#008000', N'customer', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (20, N'Customer', N'en', 0, 1, N'#008000', N'customer', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)
				INSERT [LU].[MarketInsightStatus] ([MarketInsightStatusId], [Name], [Language], [Favorite], [SortOrder], [Color], [Value], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive], [SelectableByUser]) VALUES (21, N'vevo', N'hu', 0, 6, N'#008000', N'customer', GETUTCDATE(), GETUTCDATE(), N'Migration_20201001073300', N'Migration_20201001073300', 1, 1)

				SET IDENTITY_INSERT [LU].[MarketInsightStatus] OFF"
			);
		}
	}
}