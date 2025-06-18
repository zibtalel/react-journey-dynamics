namespace Crm.Configurator.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20151222153100)]
	public class AddShowInConfiguratorToArticleGroup : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[LU].[ArticleGroup]", new Column("ShowInConfigurator", DbType.Boolean, ColumnProperty.NotNull, false));
		}
	}
}