namespace Crm.ErpExtension.BusinessObjects
{
	public class InforSalesISvc : InforObject
	{
		//Properties
		public int MakerID { get; set; }
		public int? SOType { get; set; }
		public int? MainStatus { get; set; }
		public int? StatusA { get; set; }
		public string DestinationCAMID { get; set; }
		public string SLAID { get; set; }
		public string ShortText { get; set; }
	}
}
