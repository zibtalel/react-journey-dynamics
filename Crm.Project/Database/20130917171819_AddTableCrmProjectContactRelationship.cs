namespace Crm.Project.Database
{
	using System;
	using System.Data;
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130917171819)]
	public class AddTableCrmProjectContactRelationship : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("[CRM].[ProjectContactRelationship]"))
			{
				Database.AddTable("CRM.ProjectContactRelationship",
					new Column("ProjectContactRelationshipId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
					new Column("ProjectKey", DbType.Int32, ColumnProperty.NotNull),
					new Column("ContactKey", DbType.Int32, ColumnProperty.NotNull),
					new Column("RelationshipType", DbType.String, 50, ColumnProperty.NotNull),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull),
					new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("Information", DbType.String, Int32.MaxValue, ColumnProperty.Null),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true),
					new Column("TenantKey", DbType.Int32, ColumnProperty.Null));

				var migrate = new StringBuilder();
				migrate.AppendLine("INSERT INTO [CRM].[ProjectContactRelationship]");
				migrate.AppendLine("(ProjectKey, ContactKey, RelationshipType, CreateDate, CreateUser, ModifyDate, ModifyUser, Information, IsActive, TenantKey)");
				migrate.AppendLine("SELECT [ProjectKey], [ContactKey], 'Other', GETUTCDATE(), 'Setup', GETUTCDATE(), 'Setup', NULL, 1, [TenantKey]");
				migrate.AppendLine("FROM [CRM].[ContactProjects]");
				Database.ExecuteNonQuery(migrate.ToString());
				
				Database.RemoveTable("[CRM].[ContactProjects]");
			}
		}
		public override void Down()
		{
			throw new NotImplementedException();
		}
	}
}