namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;
	using Crm.Library.Extensions;

	[Migration(20170724120004)]
	public class AddDefaultValuesToLookups : Migration
	{
		public override void Up()
		{
			new[]
			{
				new { Schema = "LU", Table = "ProjectCategory" },
				new { Schema = "LU", Table = "ProjectCategoryGroups" },
				new { Schema = "LU", Table = "ProjectContactRelationshipType" },
				new { Schema = "LU", Table = "ProjectLostReasonCategory" },
				new { Schema = "LU", Table = "ProjectStatus" }
			}
			.ForEach(x => Database.AddEntityBaseDefaultContraints(x.Schema, x.Table));
		}
	}
}