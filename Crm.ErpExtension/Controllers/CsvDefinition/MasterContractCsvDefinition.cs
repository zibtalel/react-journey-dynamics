namespace Crm.ErpExtension.Controllers.CsvDefinition
{
	using Crm.ErpExtension.Model;
	using Crm.Library.EntityConfiguration;
	using Crm.Library.Globalization.Resource;
	using Crm.Library.Services.Interfaces;

	public class MasterContractCsvDefinition : CsvDefinition<MasterContract>
	{
		public MasterContractCsvDefinition(IUserService userService, IResourceManager resourceManager)
			: base(resourceManager)
		{
			Property("Id", x => x.Id);
			Property("CompanyNo", x => x.CompanyNo);
			Property("CompanyName", x => x.CompanyName);
			Property("OrderNo", x => x.OrderNo);
			Property("OrderConfirmationDate", x => x.OrderConfirmationDate);
			Property("Total", x => x.Total);
			Property("QuantityShipped", x => x.QuantityShipped);
			Property("DueDate", x => x.DueDate);
		}
	}
}