namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20130917112527)]
	public class AddColumnsPaymentConditionToCrmProject : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("CRM.Project", new Column("PaymentCondition", DbType.String, 20));
		}

		public override void Down()
		{

		}

	}
}