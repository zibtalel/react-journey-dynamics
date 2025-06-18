namespace Crm.ErpExtension.Rest.Model
{
	using System;

	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(SalesOrder))]
	public class SalesOrderRest : ErpDocumentHeadRest<SalesOrderPositionRest>
	{
		public string OrderConfirmationNo { get; set; }
		public DateTime? OrderConfirmationDate { get; set; }
	}
}
