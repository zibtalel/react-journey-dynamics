namespace Crm.Configurator.Database
{
	using System;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160815142104)]
	public class AddRelationshipTypeVariableValue : Migration
	{
		public override void Up()
		{
			if (Convert.ToInt32(Database.ExecuteScalar("SELECT COUNT(*) FROM [LU].[ArticleRelationshipType] WHERE [Language] = 'de' AND [Value] = 'VariableValue'")) == 0)
			{
				Database.ExecuteNonQuery("INSERT INTO [LU].[ArticleRelationshipType] ([Language], [Value], [Name], [InverseName], [ArticleTypes], [HasQuantity], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES ('de', 'VariableValue', 'Variablenoption', 'Option von Variable', 'Variable', 0, 0, 0, GETUTCDATE(), GETUTCDATE(), 'Setup', 'Setup', 1)");
			}
			if (Convert.ToInt32(Database.ExecuteScalar("SELECT COUNT(*) FROM [LU].[ArticleRelationshipType] WHERE [Language] = 'en' AND [Value] = 'VariableValue'")) == 0)
			{
				Database.ExecuteNonQuery("INSERT INTO [LU].[ArticleRelationshipType] ([Language], [Value], [Name], [InverseName], [ArticleTypes], [HasQuantity], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES ('en', 'VariableValue', 'Variable value', 'Option of variable', 'Variable', 0, 0, 0, GETUTCDATE(), GETUTCDATE(), 'Setup', 'Setup', 1)");
			}
		}
	}
}