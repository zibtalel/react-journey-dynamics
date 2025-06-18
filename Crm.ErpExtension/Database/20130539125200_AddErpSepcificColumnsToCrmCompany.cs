namespace Crm.ErpExtension.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20130539125200)]
	public class AddErpSepcificColumnsToCrmCompany : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[Company]", "ErpPaymentTerms"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpPaymentTerms", DbType.String, 64, ColumnProperty.Null);
			}

			if (!Database.ColumnExists("[CRM].[Company]", "ErpPaymentTermsKey"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpPaymentTermsKey", DbType.String, 20, ColumnProperty.Null);
			}

			if (!Database.ColumnExists("[CRM].[Company]", "ErpTermsOfDelivery"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpTermsOfDelivery", DbType.String, 64, ColumnProperty.Null);
			}

			if (!Database.ColumnExists("[CRM].[Company]", "ErpTermsOfDeliveryKey"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpTermsOfDeliveryKey", DbType.String, 20, ColumnProperty.Null);
			}

			if (!Database.ColumnExists("[CRM].[Company]", "ErpCurrency"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpCurrency", DbType.String, 20, ColumnProperty.Null);
			}

			if (!Database.ColumnExists("[CRM].[Company]", "ErpPartialDeliveryProhibited"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpPartialDeliveryProhibited", DbType.Boolean, ColumnProperty.Null);
			}

			if (!Database.ColumnExists("[CRM].[Company]", "ErpDeliveryProhibited"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpDeliveryProhibited", DbType.Boolean, ColumnProperty.Null);
			}

			if (!Database.ColumnExists("[CRM].[Company]", "ErpAccountGroup"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpAccountGroup", DbType.Int32, ColumnProperty.Null);
			}

			if (!Database.ColumnExists("[CRM].[Company]", "ErpCreditLimit"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpCreditLimit", DbType.String, 256, ColumnProperty.Null);
			}

			if (!Database.ColumnExists("[CRM].[Company]", "ErpDiscount"))
			{
				Database.AddColumn("[CRM].[Company]", "ErpDiscount", DbType.String, 256, ColumnProperty.Null);
			}
		}

		public override void Down()
		{
		}
	}
}