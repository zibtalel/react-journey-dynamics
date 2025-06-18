namespace Crm.Configurator.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20160815172300)]
	public class AddPurchasePriceToCrmArticleRelationship : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[CRM].[ArticleRelationship]", new Column("PurchasePrice", DbType.Currency, ColumnProperty.Null));
		}
	}
}