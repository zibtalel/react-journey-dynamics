namespace Sms.Einsatzplanung.Connector.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20221005140500)]
	public class AddExportToSchedulerToArticle : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[CRM].[Article]",
				new Column("ExportToScheduler",
					DbType.Boolean,
					ColumnProperty.NotNull,
					false));
		}
	}
}
