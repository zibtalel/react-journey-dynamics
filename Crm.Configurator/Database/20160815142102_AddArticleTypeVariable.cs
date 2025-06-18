namespace Crm.Configurator.Database
{
	using System;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160815142102)]
	public class AddArticleTypeVariable : Migration
	{
		public override void Up()
		{
			if (Convert.ToInt32(Database.ExecuteScalar("SELECT COUNT(*) FROM [LU].[ArticleType] WHERE [Language] = 'de' AND [Value] = 'Variable'")) == 0)
			{
				Database.ExecuteNonQuery("INSERT INTO [LU].[ArticleType] ([Name], [Language], [Value]) VALUES ('Variable', 'de', 'Variable')");
			}
			if (Convert.ToInt32(Database.ExecuteScalar("SELECT COUNT(*) FROM [LU].[ArticleType] WHERE [Language] = 'en' AND [Value] = 'Variable'")) == 0)
			{
				Database.ExecuteNonQuery("INSERT INTO [LU].[ArticleType] ([Name], [Language], [Value]) VALUES ('Variable', 'en', 'Variable')");
			}
		}
	}
}