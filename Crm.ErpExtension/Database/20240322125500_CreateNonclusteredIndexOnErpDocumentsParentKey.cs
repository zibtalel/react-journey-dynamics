namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20240322125500)]
	public class CreateNonclusteredIndexOnErpDocumentsParentKey : Migration 
	{
		public override void Up()
		{
			if (Database.TableExists("[CRM].[ERPDocument]"))
			{
				Database.ExecuteNonQuery(@"CREATE NONCLUSTERED INDEX IX_ERPDocument_ParentKey ON [CRM].[ERPDocument] ([ParentKey])");
			}
		}
	}
}
