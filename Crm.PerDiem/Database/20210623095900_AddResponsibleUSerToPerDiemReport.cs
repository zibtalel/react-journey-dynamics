namespace Crm.PerDiem.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20210623095900)]
	public class AddResponsibleUSerToPerDiemReport : Migration
	{
		public override void Up()
		{
			if (Database.TableExists("[CRM].[PerDiemReport]"))
			{
				if (!Database.ColumnExists("[CRM].[PerDiemReport]", "ResponsibleUser"))
				{
					Database.AddColumn("[CRM].[PerDiemReport]", new Column("ResponsibleUser", DbType.String));

					Database.ExecuteNonQuery("UPDATE [CRM].[PerDiemReport] SET ResponsibleUser = CreateUser");
				}
			}
		}
	}
}
