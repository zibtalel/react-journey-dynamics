namespace Crm.Configurator.Database
{
	using System;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160815142103)]
	public class AddArticleTypeConfigurationBase : Migration
	{
		public override void Up()
		{
			if (Convert.ToInt32(Database.ExecuteScalar("SELECT COUNT(*) FROM [LU].[ArticleType] WHERE [Language] = 'de' AND [Value] = 'ConfigurationBase'")) == 0)
			{
				Database.ExecuteNonQuery("INSERT INTO [LU].[ArticleType] ([Name], [Language], [Value]) VALUES ('Konfigurationsbasis', 'de', 'ConfigurationBase')");
			}
			if (Convert.ToInt32(Database.ExecuteScalar("SELECT COUNT(*) FROM [LU].[ArticleType] WHERE [Language] = 'en' AND [Value] = 'ConfigurationBase'")) == 0)
			{
				Database.ExecuteNonQuery("INSERT INTO [LU].[ArticleType] ([Name], [Language], [Value]) VALUES ('Configuration base', 'en', 'ConfigurationBase')");
			}
		}
	}
}