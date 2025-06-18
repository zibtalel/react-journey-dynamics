namespace Crm.ErpExtension.Rest.Model
{
	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(QuotePosition))]
	public class QuotePositionRest : ErpDocumentPositionRest<QuoteRest>
	{
	}
}
