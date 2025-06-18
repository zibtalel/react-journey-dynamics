namespace Crm.Configurator.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20160815172301)]
	public class AddSalesPriceToCrmArticleRelationship : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[CRM].[ArticleRelationship]", new Column("SalesPrice", DbType.Currency, ColumnProperty.Null));
		}
	}
}