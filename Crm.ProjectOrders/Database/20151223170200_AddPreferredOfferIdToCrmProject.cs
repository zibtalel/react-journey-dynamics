namespace Crm.ProjectOrders.Database
{
	using System.Data;

	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20151223170200)]
	public class AddPreferredOfferIdToCrmProject : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[Project]", "PreferredOfferId"))
			{
				var orderIdIsGuid = (int)Database.ExecuteScalar(@"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Order' AND COLUMN_NAME='OrderId' AND DATA_TYPE = 'uniqueidentifier'") == 1;
				Database.AddColumn("[CRM].[Project]", new Column("PreferredOfferId", orderIdIsGuid ? DbType.Guid : DbType.Int64, ColumnProperty.Null));
				Database.AddForeignKey("FK_Project_PreferredOfferId", "[CRM].[Project]", "PreferredOfferId", "[CRM].[Order]", "OrderId");
			}
		}
	}
}