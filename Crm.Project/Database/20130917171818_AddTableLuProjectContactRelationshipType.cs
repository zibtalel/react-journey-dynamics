namespace Crm.Project.Database
{
	using System;
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130917171818)]
	public class AddTableLuProjectContactRelationshipType : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[LU].[ProjectContactRelationshipType]"))
			{
				Database.AddTable("LU.ProjectContactRelationshipType",
					new Column("ProjectContactRelationshipTypeId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
					new Column("Value", DbType.String, 50, ColumnProperty.NotNull),
					new Column("Name", DbType.String, ColumnProperty.NotNull),
					new Column("Language", DbType.String, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull),
					new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull),
					new Column("TenantKey", DbType.Int32, ColumnProperty.Null));
				Database.ExecuteNonQuery("INSERT INTO LU.ProjectContactRelationshipType " +
				                         "([Value], [Name], [Language], [Favorite], [SortOrder], [TenantKey]) " +
				                         "VALUES ('Other', 'Other', 'en', 1, 0, NULL)");
				Database.ExecuteNonQuery("INSERT INTO LU.ProjectContactRelationshipType " +
				                         "([Value], [Name], [Language], [Favorite], [SortOrder], [TenantKey]) " +
				                         "VALUES ('Other', 'Sonstiges', 'de', 1, 0, NULL)");
			}
		}
		public override void Down()
		{
			throw new NotImplementedException();
		}
	}
}