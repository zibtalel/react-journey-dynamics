namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20121203170206)]
	public class AlterRplDispatchChangeDispatchAbsenceTypeKeyToString : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("RPL.Dispatch", "AbsenceTypeKey")) Database.ExecuteNonQuery("ALTER TABLE [RPL].[Dispatch] ALTER COLUMN AbsenceTypeKey NVARCHAR(20) NULL");
		}
		public override void Down()
		{
			
		}
	}
}
