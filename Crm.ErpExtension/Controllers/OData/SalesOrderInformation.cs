namespace Crm.ErpExtension.Controllers.OData
{
	using System;

	public class SalesOrderInformation
	{
		public DateTime FirstOrder { get; set; }
		public DateTime LastOrder { get; set; }
		public int TotalOrders { get; set; }
	}
}