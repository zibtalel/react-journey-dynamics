namespace Crm.ErpExtension.Rest.Model
{
	using System;

	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(DeliveryNote))]
	public class DeliveryNoteRest : ErpDocumentHeadRest<DeliveryNotePositionRest>
	{
		public string DeliveryNoteNo { get; set; }
		public DateTime? DeliveryNoteDate { get; set; }
	}
}
