namespace Crm.PerDiem.Database
{
	using System.Data;
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20220815110000)]
	public class AddApprovedByAndApprovedDateFieldsToPerDiemReport : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("CRM.PerDiemReport", new Column("ApprovedBy", DbType.String, ColumnProperty.Null));
			Database.AddColumnIfNotExisting("CRM.PerDiemReport", new Column("ApprovedDate", DbType.DateTime, ColumnProperty.Null));
		}
	}
}