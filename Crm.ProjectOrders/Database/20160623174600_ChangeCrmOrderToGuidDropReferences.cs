namespace Crm.ProjectOrders.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20160623174600)]
	public class ChangeCrmOrderToGuidDropReferences : Migration
	{
		public override void Up()
		{
			Database.ExecuteNonQuery(@"IF EXISTS (SELECT * FROM sys.objects WHERE NAME = 'FK_Project_PreferredOfferId') AND EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='CRM' AND TABLE_NAME='Project' AND COLUMN_NAME='PreferredOfferId' AND DATA_TYPE like '%int')
					BEGIN
						ALTER TABLE [CRM].[Project] DROP CONSTRAINT [FK_Project_PreferredOfferId]
					END");
		}
	}
}