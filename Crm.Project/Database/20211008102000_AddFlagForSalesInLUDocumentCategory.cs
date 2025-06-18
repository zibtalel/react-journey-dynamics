namespace Crm.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;
	using System.Data;

	[Migration(20211008102000)]
	public class AddFlagForSalesInLUDocumentCategory : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("LU.DocumentCategory", new Column("SalesRelated", DbType.Boolean, ColumnProperty.NotNull, false));
		}
	}
}
