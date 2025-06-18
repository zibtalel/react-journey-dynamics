namespace Crm.ErpExtension.Model
{
	public class MasterContractPosition : ErpDocumentPosition<MasterContract>
	{
		public virtual decimal? QuantityShipped { get; set; }
	}
}
