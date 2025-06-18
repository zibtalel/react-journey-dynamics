namespace Crm.ProjectOrders.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20141118110100)]
	public class AddProjectNoToCrmProject : Migration
	{
		public override void Up()
		{
			var hasContactIdChangedToGuid = (int)Database.ExecuteScalar("SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'Crm' AND TABLE_NAME = 'Contact' AND COLUMN_NAME = 'ContactId' and DATA_TYPE = 'uniqueidentifier'") == 1;
			Database.AddColumnIfNotExisting("[CRM].[Order]", new Column("ProjectKey", hasContactIdChangedToGuid ? DbType.Guid : DbType.Int32, ColumnProperty.Null));
		}
	}
}