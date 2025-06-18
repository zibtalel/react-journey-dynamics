namespace Crm.ErpExtension.Database
{
	using System.Text;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20131015143200)]
	public class AlterColumnsForRecordId : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("[CRM].[ERPDocument]", "RecordId"))
			{
				var sql = new StringBuilder();

				sql.AppendLine("DECLARE @TableName SYSNAME = 'CRM.ERPDocument'");
				sql.AppendLine("DECLARE @PrimaryKeyName sysname = (");
				sql.AppendLine("select name");
				sql.AppendLine("from sys.key_constraints");
				sql.AppendLine("where type = 'PK' and parent_object_id = object_id(@TableName))");
				sql.AppendLine("EXECUTE ('alter table ' + @TableName + ' drop constraint ' + @PrimaryKeyName) ");
				sql.AppendLine("ALTER TABLE Crm.ErpDocument ALTER COLUMN RecordId NVARCHAR(50) NOT NULL");
				sql.AppendLine("ALTER TABLE Crm.ErpDocument ADD CONSTRAINT PK_RecordId PRIMARY KEY (RecordId)");

				Database.ExecuteNonQuery(sql.ToString());
			}
		}
		public override void Down()
		{
		}
	}
}