namespace Crm.ErpExtension.BusinessObjects
{
	using System;

	public class InforSMAIB : InforObject
	{
		//Properties
		public string CAMID { get; set; }
		public string SMANo { get; set; }
		public int IPos { get; set; }
		public int SMAIBStatus { get; set; }
		public string SMAIBStatusStr { get; set; }
		public DateTime? CtrBeginDate { get; set; }
		public DateTime? CtrExpDate { get; set; }

		public DateTime? LastServiceDt { get; set; }
		public DateTime? NextServiceDt { get; set; }
		public DateTime? LastSOClosingDt { get; set; }

		public InforSalesISvc SalesISvc { get; private set; }
		public InforFb Fb { get; private set; }

		// Constructor
		public InforSMAIB()
		{
			SalesISvc = new InforSalesISvc();
			Fb = new InforFb();
		}
	}
}
