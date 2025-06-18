namespace Sms.Einsatzplanung.Connector.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20121127172738)]
	public class AddTemplateKeyToProfile : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("RPL.Profile", "TemplateKey"))
				Database.AddColumn("RPL.Profile", "TemplateKey", DbType.Int32);
		}
		public override void Down()
		{
			
		}
	}
}
