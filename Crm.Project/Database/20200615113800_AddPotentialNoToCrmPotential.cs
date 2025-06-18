namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20200615113800)]
	public class AddPotentialNoToCrmPotential : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("CRM.Potential", new Column("PotentialNo", DbType.String, 20, ColumnProperty.Null));
		}
	}
}