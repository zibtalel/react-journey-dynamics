namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20140630141700)]
	public class AddProjectNoToCrmProject : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("CRM.Project", new Column("ProjectNo", DbType.String, 20, ColumnProperty.Null));
		}
	}
}