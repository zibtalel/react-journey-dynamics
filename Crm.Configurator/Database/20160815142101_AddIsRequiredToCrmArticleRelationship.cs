namespace Crm.Configurator.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20160815142101)]
	public class AddIsRequiredToCrmArticleRelationship : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[CRM].[ArticleRelationship]", new Column("IsRequired", DbType.Boolean, ColumnProperty.Null));
		}
	}
}