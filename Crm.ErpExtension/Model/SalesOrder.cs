namespace Crm.ErpExtension.Model
{
	using System;

	public class SalesOrder : ErpDocumentHead<SalesOrderPosition>
	{
		public virtual string OrderConfirmationNo { get; set; }
		public virtual DateTime? OrderConfirmationDate { get; set; }
	}
}