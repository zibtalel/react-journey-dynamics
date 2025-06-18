namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20200616083300)]
	public class AddPotentialKeyToProject : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[CRM].[Project]", new Column("PotentialKey", DbType.Guid, ColumnProperty.Null));
		}
	}
}