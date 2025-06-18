namespace Crm.Configurator.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160816141500)]
	public class AddTableLuConfigurationRuleType : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("LU.ConfigurationRuleType"))
			{
				Database.AddTable("LU.ConfigurationRuleType",
					new Column("ConfigurationRuleTypeId", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
					new Column("Value", DbType.String, 20, ColumnProperty.NotNull),
					new Column("Name", DbType.String, ColumnProperty.NotNull),
					new Column("Language", DbType.String, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
					new Column("SortOrder", DbType.Int32, ColumnProperty.NotNull, 0),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true),
					new Column("TenantKey", DbType.Int32, ColumnProperty.Null));
				InsertLookup("Restriction", "Ausschluss", "de", false, 0);
				InsertLookup("Restriction", "Restriction", "en", false, 0);
				InsertLookup("Inclusion", "Einschluss", "de", false, 0);
				InsertLookup("Inclusion", "Inclusion", "en", false, 0);
				InsertLookup("Hint", "Hinweis", "de", false, 0);
				InsertLookup("Hint", "Hint", "en", false, 0);
			}
		}

		private void InsertLookup(string value, string name, string language, bool favorite, int sortorder)
		{
			Database.ExecuteNonQuery($"INSERT INTO [LU].[ConfigurationRuleType] ([Value], [Name], [Language], [Favorite], [SortOrder], [CreateDate], [ModifyDate], [CreateUser], [ModifyUser], [IsActive]) VALUES ('{value}', '{name}', '{language}', {(favorite ? 1 : 0)}, {sortorder}, GETUTCDATE(), GETUTCDATE(), 'Setup', 'Setup', 1)");
		}
	}
}