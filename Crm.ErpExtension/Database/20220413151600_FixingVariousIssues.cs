namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20220413151600)]
	public class CorrectDiscriminatorColumn : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery("Update [Crm].[ERPDocument] Set DocumentType = 'Quote' WHERE DocumentType = 'Quotes'");
			Database.ExecuteNonQuery("Update [Crm].[ERPDocument] Set DocumentType = Concat(DocumentType, 'Position') WHERE RecordType = 'Position'");

			Database.ExecuteNonQuery("Alter Table Crm.ErpDocument ALTER COLUMN LegacyId nvarchar(50) null ");
			Database.ExecuteNonQuery("Alter Table Crm.ErpDocument ALTER COLUMN LegacyId nvarchar(50) null ");
			Database.ExecuteNonQuery("Alter Table Crm.ErpDocument ALTER COLUMN StatusKey nvarchar(50) null ");
			Database.ExecuteNonQuery("Alter Table Crm.ErpDocument ALTER COLUMN CompanyNo nvarchar(50) null ");
			Database.ExecuteNonQuery("Alter Table Crm.ErpDocument ALTER COLUMN OrderNo nvarchar(50) null ");
			Database.ExecuteNonQuery("Alter Table Crm.ErpDocument ALTER COLUMN PositionNo nvarchar(50) null ");
			
			Database.AddColumnIfNotExisting("Crm.ErpDocument", new Column("ArticleKey", DbType.Guid, ColumnProperty.Null));
		}
	}
}
