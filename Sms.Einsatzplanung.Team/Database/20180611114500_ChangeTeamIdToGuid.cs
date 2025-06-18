namespace Sms.Einsatzplanung.Team.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20180611114500)]
	public class ChangeTeamIdToGuid : Migration
	{
		public override void Up()
		{
			if (Database.GetColumnDataType("RPL", "Dispatch", "TeamId") == "int")
			{
				Database.ExecuteNonQuery("EXEC sp_rename 'RPL.Dispatch.TeamId', 'TeamIdOld', 'COLUMN'");
				Database.ExecuteNonQuery("ALTER TABLE RPL.Dispatch ADD TeamId UNIQUEIDENTIFIER NULL");
				Database.ExecuteNonQuery(@"
					UPDATE d
					SET d.TeamId = ug.UsergroupId
					FROM RPL.Dispatch d
					JOIN CRM.Usergroup ug ON UserGroupIdOld = TeamIdOld");
			}
		}
	}
}