namespace Crm.Configurator.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20160815142100)]
	public class AddIsDefaultToCrmArticleRelationship : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[CRM].[ArticleRelationship]", new Column("IsDefault", DbType.Boolean, ColumnProperty.Null));
		}
	}
}