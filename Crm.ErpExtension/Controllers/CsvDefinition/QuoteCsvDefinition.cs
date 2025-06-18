namespace Crm.ErpExtension.Controllers.CsvDefinition
{
	using Crm.ErpExtension.Model;
	using Crm.Library.EntityConfiguration;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Services.Interfaces;

	public class QuoteCsvDefinition : CsvDefinition<Quote>
	{
		public QuoteCsvDefinition(IUserService userService, IResourceManager resourceManager)
			: base(resourceManager)
		{
			Property("Id", x => x.Id);
			Property("CompanyNo", x => x.CompanyNo);
			Property("CompanyName", x => x.CompanyName);
			Property("OrderNo", x => x.OrderNo);
			Property("QuoteNo", x => x.QuoteNo);
			Property("QuoteDate", x => x.QuoteDate);
			Property("Total", x => x.Total);
		}
	}
}