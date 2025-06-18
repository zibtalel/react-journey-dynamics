namespace Crm.ErpExtension.Model
{
	using System;

	public class DeliveryNote : ErpDocumentHead<DeliveryNotePosition>
	{
		public virtual string DeliveryNoteNo { get; set; }
		public virtual DateTime? DeliveryNoteDate { get; set; }
	}
}