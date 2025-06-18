namespace Crm.ErpExtension.BusinessObjects
{
	public class InforZTNum : InforObject
	{
		//Properties
		public int ZTKey { get; set; }
		public string KTxt { get; set; }
		public string TabName { get; set; }
		public string Sprache { get; set; }
	}

	public class InforZTISvcCAMStatus : InforZTNum
	{}
}
