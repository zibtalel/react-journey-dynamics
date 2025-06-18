namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20220315120000)]
	public class ChangeFloatsAndDoublesToDecimal : Migration
	{
		public override void Up()
		{
			foreach (var column in Columns)
			{
				Database.ExecuteNonQuery($"ALTER TABLE {column.Item1} ALTER COLUMN {column.Item2} decimal(19,4);");
			}
		}
		private readonly (string, string)[] Columns =	
		{
			//ErpDocs
			("CRM.ErpDocument","Total"),
			("CRM.ErpDocument","[Total wo Taxes]"),
			("CRM.ErpDocument","Quantity"),
			("CRM.ErpDocument","QuantityShipped"),
			("CRM.ErpDocument","RemainingQuantity"),
			//ErpTurnover
			("CRM.Turnover", "Month1"),
			("CRM.Turnover", "Month2"),
			("CRM.Turnover", "Month3"),
			("CRM.Turnover", "Month4"),
			("CRM.Turnover", "Month5"),
			("CRM.Turnover", "Month6"),
			("CRM.Turnover", "Month7"),
			("CRM.Turnover", "Month8"),
			("CRM.Turnover", "Month9"),
			("CRM.Turnover", "Month10"),
			("CRM.Turnover", "Month11"),
			("CRM.Turnover", "Month12"),
			("CRM.Turnover", "Total"),
			//Company,
			("CRM.Company", "ErpCreditLimit"),
			("CRM.Company", "ErpOpenItemsTotal"),
			("CRM.Company", "ErpOutstandingOrderValue"),
			("CRM.Company", "ErpOutstandingInvoiceValue"),
			("CRM.Company", "ErpOpenItemsDue"),
		};
	}
}
