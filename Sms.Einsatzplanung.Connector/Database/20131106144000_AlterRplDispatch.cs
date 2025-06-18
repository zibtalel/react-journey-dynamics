namespace Sms.Einsatzplanung.Connector.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131106144000)]
	public class AlterRplDispatch : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("RPL.Dispatch", "Type"))
			{
				Database.ChangeColumn("RPL.Dispatch", new Column("Type", DbType.String, 50, ColumnProperty.NotNull));
			}
		}
		public override void Down()
		{
		}
	}
}