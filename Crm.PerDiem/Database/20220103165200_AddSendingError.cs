namespace Crm.PerDiem.Database
{
	using System.Data;
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20220103165200)]
	public class AddSendingError : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("CRM.PerDiemReport", new Column("SendingError", DbType.String, int.MaxValue, ColumnProperty.Null));
		}
	}
}