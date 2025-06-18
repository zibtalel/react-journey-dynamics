namespace Crm.ErpExtension.Database
{
	using Crm.Library.Data.MigratorDotNet.Framework;

	[Migration(20170220141803)]
	public class RemoveObsoleteColumnsFromErpDocument : Migration
	{
		public override void Up()
		{
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "DocumentState"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "DocumentState");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "State"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "State");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "FilterElement"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "FilterElement");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "DocumentServiceType"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "DocumentServiceType");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "DocumentServiceState"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "DocumentServiceState");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "DeviceNo"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "DeviceNo");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "ReferenceB"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "ReferenceB");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "DeliveryTrackingUrl"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "DeliveryTrackingUrl");
			}
			if (!Database.ColumnExists("[CRM].[ERPDocument]", "DmsLegacyId"))
			{
				Database.RemoveColumn("[CRM].[ERPDocument]", "DmsLegacyId");
			}
		}
	}
}
