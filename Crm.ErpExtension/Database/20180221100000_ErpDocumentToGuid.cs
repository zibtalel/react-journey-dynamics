namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20180221100000)]
	public class ErpDocumentToGuid : Migration
	{
		public override void Up()
		{
			if (Database.ColumnExists("[CRM].[ERPDocument]", "RecordId") && Database.GetColumnDataType("Crm", "ERPDocument", "RecordId") == "nvarchar")
			{
				Database.ExecuteNonQuery(@"ALTER TABLE CRM.ERPDocument DROP CONSTRAINT PK_RecordId");
				Database.ExecuteNonQuery(@"sp_rename 'CRM.ERPDocument.RecordId', 'LegacyId', 'COLUMN'");
				Database.ExecuteNonQuery(@"
					ALTER TABLE CRM.ERPDocument
					ADD ErpDocumentId UNIQUEIDENTIFIER
					CONSTRAINT DF_ErpDocument_ErpDocumentId DEFAULT(NEWSEQUENTIALID())
					CONSTRAINT PK_ErpDocument PRIMARY KEY");
				Database.ExecuteNonQuery(@"
					UPDATE CRM.ERPDocument
					SET ModifyDate = GETUTCDATE()
						,ModifyUser = 'Migration_20180221100000'");
			}
		}
	}
}