namespace Crm.Project.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20130919103619)]
	public class AddFolderKeyToSmsInstallation : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("CRM.Project", new Column("FolderKey", DbType.Int32, ColumnProperty.Null));
		}
	}
}