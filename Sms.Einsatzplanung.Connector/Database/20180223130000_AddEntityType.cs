namespace Sms.Einsatzplanung.Connector.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Helper;

	using Sms.Einsatzplanung.Connector.Model;

	[Migration(20180223130000)]
	public class AddEntityType : Migration
	{
		public override void Up()
		{
			var helper = new UnicoreMigrationHelper(Database);

			helper.AddEntityTypeAndAuthDataColumnIfNeeded<RplServiceOrderDispatch>("RPL", "Dispatch");
			helper.AddEntityType<RplTimePosting>();

			Database.ExecuteNonQuery("INSERT INTO EntityType ([Name]) VALUES ('Einsatzplanung.Absence.Model.AbsenceDispatch')");
			Database.ExecuteNonQuery("INSERT INTO EntityType ([Name]) VALUES ('Einsatzplanung.Absence.Model.GeneratedAbsenceDispatch')");
			Database.ExecuteNonQuery("INSERT INTO EntityType ([Name]) VALUES ('Einsatzplanung.InforTimes.Model.InforTimesDispatch')");
			Database.ExecuteNonQuery("INSERT INTO EntityType ([Name]) VALUES ('Einsatzplanung.Team.Model.TeamDispatch')");
			Database.ExecuteNonQuery("INSERT INTO EntityType ([Name]) VALUES ('Einsatzplanung.Core.Model.Profile')");

			if (Database.TableExists("dbo.EntityAuthData"))
			{
				helper.AddEntityAuthDataColumn("RPL", "Profile");
			}

			if (Database.ColumnExists("RPL.Profile", "InternalId") == false)
			{
				Database.ExecuteNonQuery(@"
					ALTER TABLE [RPL].[Profile]
					ADD InternalId UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWSEQUENTIALID())
				");
			}
		}
	}
}