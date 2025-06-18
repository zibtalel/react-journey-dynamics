namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20170310150500)]
	public class DropRequirementFromErpTurnover : Migration
	{
		public override void Up()
		{
			Database.RemoveColumnIfExisting("CRM.Turnover", "Requirement");
		}
	}
}