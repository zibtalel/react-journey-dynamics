namespace Crm.ErpExtension.Rest.Model
{
	using Crm.ErpExtension.Model;
	using Crm.Library.Rest;

	[RestTypeFor(DomainType = typeof(CreditNotePosition))]
	public class CreditNotePositionRest : ErpDocumentPositionRest<CreditNoteRest>
	{
	}
}
