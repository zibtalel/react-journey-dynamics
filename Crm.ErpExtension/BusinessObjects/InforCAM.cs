namespace Crm.ErpExtension.BusinessObjects
{
	using System;
	using System.Collections.Generic;

	public class InforCAM : InforObject
	{
		//Properties
		public string CAMID { get; set; }
		public string Description { get; set; }
		public string CustomerNr { get; set; }
		public DateTime? WarrantyFrom { get; set; }
		public DateTime? WarrantyUntil { get; set; }
		public DateTime? Betriebsdatum1 { get; set; }
		public int? Betriebsstunden1 { get; set; }
		public string Commission { get; set; }
		public string SerialNo { get; set; }
		public int? CAMType { get; set; }
		public int? Status { get; set; }
		public DateTime? BuildingDate { get; set; }
	
		public string CAMTypeStr { get; set; }
		public string StatusStr { get; set; }

		public List<InforSMAIB> SMAIBList { get; private set; }

		//Constructor
		public InforCAM()
		{
			SMAIBList = new List<InforSMAIB>();
		}
	}
}
