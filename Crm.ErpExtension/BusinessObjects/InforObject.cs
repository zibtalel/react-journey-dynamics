namespace Crm.ErpExtension.BusinessObjects
{
	using System;

	public class InforObject
	{
		//Properties
		public long? Ik { get; set; }
		public DateTime? CreateDate { get; set; }
		public string CreateUser { get; set; }
		public DateTime? ModifyDate { get; set; }
		public string ModifyUser { get; set; }
		public double? Cid { get; set; }
	}
}
