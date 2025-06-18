namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20221702150000)]
	public class CreateDeliveryAndPaymentMethodLookups : Migration
	{
		public override void Up()
		{
			if (!Database.TableExists("LU.ErpDeliveryMethod"))
			{
				Database.AddTable("LU.ErpDeliveryMethod", 
					new Column("ErpDeliveryMethodId", DbType.Int16, ColumnProperty.Identity),
					new Column("Name", DbType.String, 256, ColumnProperty.NotNull),
					new Column("Language", DbType.StringFixedLength, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
					new Column("SortOrder", DbType.Int16, ColumnProperty.Null),
					new Column("Value", DbType.String, 256, ColumnProperty.NotNull),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true),
					new Column("TenantKey", DbType.Int16, ColumnProperty.Null));
			}
			if (!Database.TableExists("LU.ErpPaymentMethod"))
			{
				Database.AddTable("LU.ErpPaymentMethod", 
					new Column("ErpPaymentMethodId", DbType.Int16, ColumnProperty.Identity),
					new Column("Name", DbType.String, 256, ColumnProperty.NotNull),
					new Column("Language", DbType.StringFixedLength, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
					new Column("SortOrder", DbType.Int16, ColumnProperty.Null),
					new Column("Value", DbType.String, 256, ColumnProperty.NotNull),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true),
					new Column("TenantKey", DbType.Int16, ColumnProperty.Null));
			}

			if (!Database.ColumnExists("Crm.Company", "ErpPaymentMethod"))
			{
				Database.AddColumn("Crm.Company", "ErpPaymentMethod", DbType.String, ColumnProperty.Null);
			}
			
			if (!Database.ColumnExists("Crm.Company", "ErpDeliveryMethod"))
			{
				Database.AddColumn("Crm.Company", "ErpDeliveryMethod", DbType.String, ColumnProperty.Null);
			}
			
			if (!Database.TableExists("LU.ErpDeliveryProhibitedReason"))
			{
				Database.AddTable("LU.ErpDeliveryProhibitedReason", 
					new Column("ErpDeliveryProhibitedReasonId", DbType.Int16, ColumnProperty.Identity),
					new Column("Name", DbType.String, 256, ColumnProperty.NotNull),
					new Column("Language", DbType.StringFixedLength, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
					new Column("SortOrder", DbType.Int16, ColumnProperty.Null),
					new Column("Value", DbType.String, 256, ColumnProperty.NotNull),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true),
					new Column("TenantKey", DbType.Int16, ColumnProperty.Null));
			}
			if (!Database.TableExists("LU.ErpPartialDeliveryProhibitedReason"))
			{
				Database.AddTable("LU.ErpPartialDeliveryProhibitedReason", 
					new Column("ErpPartialDeliveryProhibitedReasonId", DbType.Int16, ColumnProperty.Identity),
					new Column("Name", DbType.String, 256, ColumnProperty.NotNull),
					new Column("Language", DbType.StringFixedLength, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
					new Column("SortOrder", DbType.Int16, ColumnProperty.Null),
					new Column("Value", DbType.String, 256, ColumnProperty.NotNull),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true),
					new Column("TenantKey", DbType.Int16, ColumnProperty.Null));
			}

			if (!Database.ColumnExists("Crm.Company", "ErpPartialDeliveryProhibitedReason"))
			{
				Database.AddColumn("Crm.Company", "ErpPartialDeliveryProhibitedReason", DbType.String, ColumnProperty.Null);
			}
			
			if (!Database.ColumnExists("Crm.Company", "ErpDeliveryProhibitedReason"))
			{
				Database.AddColumn("Crm.Company", "ErpDeliveryProhibitedReason", DbType.String, ColumnProperty.Null);
			}

			
			if (!Database.TableExists("LU.ErpTermsOfDelivery"))
			{
				Database.AddTable("LU.ErpTermsOfDelivery", 
					new Column("ErpTermsOfDeliveryId", DbType.Int16, ColumnProperty.Identity),
					new Column("Name", DbType.String, 256, ColumnProperty.NotNull),
					new Column("Language", DbType.StringFixedLength, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
					new Column("SortOrder", DbType.Int16, ColumnProperty.Null),
					new Column("Value", DbType.String, 256, ColumnProperty.NotNull),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true),
					new Column("TenantKey", DbType.Int16, ColumnProperty.Null));
			}
			
			if (!Database.TableExists("LU.ErpPaymentTerms"))
			{
				Database.AddTable("LU.ErpPaymentTerms", 
					new Column("ErpPaymentTermsId", DbType.Int16, ColumnProperty.Identity),
					new Column("Name", DbType.String, 256, ColumnProperty.NotNull),
					new Column("Language", DbType.StringFixedLength, 2, ColumnProperty.NotNull),
					new Column("Favorite", DbType.Boolean, ColumnProperty.NotNull, false),
					new Column("SortOrder", DbType.Int16, ColumnProperty.Null),
					new Column("Value", DbType.String, 256, ColumnProperty.NotNull),
					new Column("CreateDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("ModifyDate", DbType.DateTime, ColumnProperty.NotNull, "GETUTCDATE()"),
					new Column("CreateUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("ModifyUser", DbType.String, 256, ColumnProperty.NotNull),
					new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true),
					new Column("TenantKey", DbType.Int16, ColumnProperty.Null));
			}
			
			if (!Database.ColumnExists("Crm.Company", "VATIdentificationNumber"))
			{
				Database.AddColumn("Crm.Company", "VATIdentificationNumber", DbType.String, ColumnProperty.Null);
			}
		}
	}
}
