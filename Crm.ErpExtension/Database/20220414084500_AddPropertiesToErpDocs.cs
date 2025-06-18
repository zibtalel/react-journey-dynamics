namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;
	using Crm.Library.Data.MigratorDotNet.Migrator.Extensions;

	[Migration(20220414084500)]
	public class AddParentToErpDocumentPosition : Migration
	{
		public override void Up()
		{
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("OrderType", DbType.String, 256, ColumnProperty.Null));
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("OrderDate", DbType.DateTime, ColumnProperty.Null));
			
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("ParentKey", DbType.Guid, ColumnProperty.Null));
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("DiscountPercentage", DbType.Decimal, ColumnProperty.Null));
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("PricePerUnit", DbType.Decimal, ColumnProperty.Null));
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("VATLevel", DbType.Decimal, ColumnProperty.Null));
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("PaymentTerms", DbType.String, 256, ColumnProperty.Null));
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("PaymentMethod", DbType.String, 256, ColumnProperty.Null));
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("TermsOfDelivery", DbType.String, 256, ColumnProperty.Null));
			Database.AddColumnIfNotExisting("[Crm].[ERPDocument]", new Column("DeliveryMethod", DbType.String, 256, ColumnProperty.Null));
		}
	}
}
