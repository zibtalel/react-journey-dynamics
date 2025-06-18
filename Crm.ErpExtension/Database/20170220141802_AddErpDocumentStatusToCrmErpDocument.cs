namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170220141802)]
	public class AddErpDocumentStatusToCrmErpDocument : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "StatusKey"))
			{
				Database.AddColumn("[CRM].[ERPDocument]", new Column("StatusKey", DbType.String, 50, ColumnProperty.Null));
				Database.ExecuteNonQuery("UPDATE [CRM].[ERPDocument] SET [StatusKey] = 'Open' WHERE [State] <> 6 OR [State] IS NULL");
				Database.ExecuteNonQuery("UPDATE [CRM].[ERPDocument] SET [StatusKey] = 'Closed' WHERE [State] = 6");
				Database.ChangeColumn("[CRM].[ERPDocument]", new Column("StatusKey", DbType.String, 50, ColumnProperty.NotNull));
				Database.RemoveColumn("[CRM].[ERPDocument]", "DocumentState");
				Database.RemoveColumn("[CRM].[ERPDocument]", "State");
			}
		}
	}
}