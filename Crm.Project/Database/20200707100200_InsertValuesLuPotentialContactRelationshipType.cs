namespace Crm.Project.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20200707100200)]
	public class InsertValuesLuPotentialContactRelationshipType : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("INSERT INTO LU.PotentialContactRelationshipType " +
			                         "([Value], [Name], [Language], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) " +
															 "VALUES ('Other', 'Other', 'en', 1, 0, GETUTCDATE(),GETUTCDATE(),N'Migration_20200707100200', N'Migration_20200707100200',1)");
			Database.ExecuteNonQuery("INSERT INTO LU.PotentialContactRelationshipType " +
															 "([Value], [Name], [Language], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) " +
															 "VALUES ('Other', 'Sonstiges', 'de', 1, 0, GETUTCDATE(),GETUTCDATE(),N'Migration_20200707100200', N'Migration_20200707100200',1)");
			Database.ExecuteNonQuery("INSERT INTO LU.PotentialContactRelationshipType " +
															 "([Value], [Name], [Language], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) " +
															 "VALUES ('Competitor', 'Competitor', 'en', 1, 0, GETUTCDATE(),GETUTCDATE(),N'Migration_20200707100200', N'Migration_20200707100200',1)");
			Database.ExecuteNonQuery("INSERT INTO LU.PotentialContactRelationshipType " +
															 "([Value], [Name], [Language], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) " +
															 "VALUES ('Competitor', 'Wettbewerber', 'de', 1, 0, GETUTCDATE(),GETUTCDATE(),N'Migration_20200707100200', N'Migration_20200707100200',1)");
		}
	}
}