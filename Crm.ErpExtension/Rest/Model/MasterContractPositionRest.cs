namespace Crm.ErpExtension.Rest.Model
{
	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(MasterContractPosition))]
	public class MasterContractPositionRest : ErpDocumentPositionRest<MasterContractRest>
	{
		public decimal? QuantityShipped { get; set; }
	}
}
