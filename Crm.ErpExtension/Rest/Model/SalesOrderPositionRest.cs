namespace Crm.ErpExtension.Rest.Model
{
	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(SalesOrderPosition))]
	public class SalesOrderPositionRest : ErpDocumentPositionRest<SalesOrderRest>
	{
	}
}
